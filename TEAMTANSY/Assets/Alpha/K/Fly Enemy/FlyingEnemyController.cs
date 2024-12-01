using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    //hpポイント
    public float hp = 10;
    public float tickInterval = 2f;    // ダメージを与える間隔（秒）
    public float duration = 10f;       // 持続時間（秒）

    //+++ サウンド再生追加 +++
    public AudioClip encon;    //敵がやられたとき

    private bool isTakingDamage = false; // 持続ダメージを受けているかどうか
    private fireBullet FB;
    // Start is called before the first frame update
    public float patrolSpeed = 2f; // パトロール速度
    public float chaseSpeed = 3f; // 追跡速度
    public float patrolRange = 3f; // パトロール範囲
    public float detectionRange = 5f; // プレイヤー検知範囲
    public Transform player; // プレイヤーのTransform

    private Vector2 startPos; // 初期位置
    private int patrolDirection = -1; // 初期移動方向を左に設定
    public bool isChasing = false; // 追跡状態フラグ
    private bool isFacingRight = false; // 初期向きを左に設定

    void Start()
    {
        FB = FindObjectOfType<fireBullet>();
        startPos = transform.position; // 初期位置を記録
    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer(); // プレイヤーを追跡
        }
        else
        {
            Patrol(); // パトロール
        }
    }

    void Patrol()
    {
        // パトロール移動
        transform.position += Vector3.right * patrolSpeed * patrolDirection * Time.deltaTime;

        // 範囲を超えたら方向を反転
        if (Mathf.Abs(transform.position.x - startPos.x) > patrolRange)
        {
            patrolDirection *= -1; // 移動方向を反転
            UpdateSpriteDirection(patrolDirection);
        }
    }

    void ChasePlayer()
    {
        // プレイヤーの方向を計算
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // 移動
        transform.position += (Vector3)directionToPlayer * chaseSpeed * Time.deltaTime;

        // スプライトの向きを更新
        UpdateSpriteDirection(directionToPlayer.x > 0 ? 1 : -1);
    }

    void UpdateSpriteDirection(int direction)
    {
        // 右を向く場合
        if (direction > 0 && !isFacingRight)
        {
            FlipSprite();
        }
        // 左を向く場合
        else if (direction < 0 && isFacingRight)
        {
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        // スプライトを反転
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // X軸を反転
        transform.localScale = scale;
    }

    public void StartDamageOverTime()
    {
        if (!isTakingDamage) // すでにダメージを受けていない場合のみ開始
        {
            StartCoroutine(ApplyDamageOverTime());
        }
    }
    private IEnumerator ApplyDamageOverTime()
    {
        isTakingDamage = true;

        float elapsed = 0f; // 持続時間を追跡する変数

        float firedamage = 0f;

        if (FB.fireBaff == false)
        {
            firedamage = 5f;
            Debug.Log("a");
        }
        else if (FB.fireBaff == true)
        {
            firedamage = 10f;

            FB.fireBaff = false;
            Debug.Log("i");
        }

        while (elapsed < duration)
        {
            // ダメージを与える処理
            TakeDamage(firedamage);

            // 次のダメージまで待機
            yield return new WaitForSeconds(tickInterval);

            // 経過時間を更新
            elapsed += tickInterval;
        }
        isTakingDamage = false; // ダメージ完了
    }
    //接触
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            TakeDamage(10f);
        }
        if (collision.collider.CompareTag("FireBullet"))
        {
            StartDamageOverTime();

        }
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);

    }
}

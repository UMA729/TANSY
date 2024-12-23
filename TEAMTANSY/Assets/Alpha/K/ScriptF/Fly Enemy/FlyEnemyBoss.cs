using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyBoss : MonoBehaviour
{
    
    Rigidbody2D rb;                     
    public float hp = 10f;               // HP
    public float speed = 5.0f;           // 徘徊時の動く速度
    public float Dirtime = 3.0f;         // 徘徊方向を切り替える間隔（秒）
    public float tickInterval = 2f;      // ダメージを与える間隔（秒）
    public float patrolSpeed = 2f;       // パトロール速度
    public float chaseSpeed = 3f;        // 追跡速度
    public Transform player;             // プレイヤーのTransform
    public bool isChasing = false;       // 追跡状態フラグ
                                         
    private float DamDur = 0.0f;         // 火ダメージを与え続ける時間を計測する
    private float DirDur = 0.0f;         // 徘徊方向を切り替える時間を計測
    private bool isFacingRight = false;  // 初期向きを左に設定
    private bool Direction = true;       // 
    private bool isTakingDamage = false; //

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    { 
        if (isChasing)
        {
            ChasePlayer();  //プレイヤー範囲内
        }
        else
        {
            MoveEnemy();    //プレイヤー範囲外中
        }
    }
    private void FixedUpdate()
    {
        if (Direction)
        {
            transform.localScale = new Vector3(4, 4, 1); 
        }
        else if (!Direction)
        {
            transform.localScale = new Vector3(-4, 4, 1);
        }

        rb.velocity = new Vector2(speed, rb.velocity.y);
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
        fireBullet FB;                          //fireBulletの呼び出し
        FB = FindAnyObjectByType<fireBullet>(); //fireBulletのオブジェクト読み込み

    　　isTakingDamage = true;                  // 火弾丸を受けた
                                                
        float elapsed = 0f;                     // 持続時間を追跡する変数

        float firedamage = 0f;                  // 火ダメージを決める変数

        if (FB.fireBaff == false)               //バフを未取得時
        {
            firedamage = 5f;
        }
        else if (FB.fireBaff == true)           //バフを取得時
        {
            firedamage = 10f;

            FB.fireBaff = false;                //バフを切る
        }

        while (elapsed < DamDur)
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

    void ChasePlayer()//プレイヤーの場所へ移動
    {
        // プレイヤーの方向を計算
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // 移動
        Vector2 targetPosition = rb.position + directionToPlayer * chaseSpeed * Time.deltaTime;

        rb.MovePosition(targetPosition);

        // スプライトの向きを更新
        UpdateSpriteDirection(directionToPlayer.x > 0 ? 1 : -1);
    }

    void UpdateSpriteDirection(int direction)//追跡時の顔方向を決める
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
    void MoveEnemy() //
    {
        DirDur += Time.deltaTime;
        if (Dirtime <= DirDur)
        {
            if (Direction)
            {
                Direction = false;  //右向き時左向きに反転
            }
            else if (!Direction)
            {
                Direction = true;   //左向き時右向きに反転
            }
            speed *= -1.0f;         //方向切り替え時間がくると移動方向を反転
            DirDur = 0.0f;          //方向切り替え時間の初期化
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float Damage = 10.0f;

        if (collision.collider.CompareTag("Bullet"))        // 通常弾丸にあたると
        {
            TakeDamage(Damage);                             // 通常弾丸ダメージ
        }
        if (collision.collider.CompareTag("FireBullet"))    // 火弾丸にあたると
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

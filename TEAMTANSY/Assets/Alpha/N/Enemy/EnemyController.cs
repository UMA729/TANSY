using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //hpポイント
    public float hp = 10;
    public float speed = 2.0f;//敵の移動速度
    public float revTime = 0f;//反転時間
    public float Xscale = 1.0f;
    public float Yscale = 1.0f;
    public float tickInterval = 2f;    // ダメージを与える間隔（秒）
    public float duration = 10f;       // 持続時間（秒）

    public bool isToRight = false;//true=右向き　false=左向き
    public LayerMask groundLayer;//地面レイヤー

    //+++ サウンド再生追加 +++
    public AudioClip encon;    //敵がやられたとき

    private Rigidbody2D rb;
    private bool isTakingDamage = false; // 持続ダメージを受けているかどうか
    private fireBullet FB;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        FB = FindObjectOfType<fireBullet>();
        rb = this.GetComponent<Rigidbody2D>();
        if (isToRight)
        {
            transform.localScale = new Vector2(-Xscale, Yscale);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (revTime > 0)
        {
            time += Time.deltaTime;
            if (time >= revTime)
            {
                isToRight = !isToRight;
                time = 0;
                if (isToRight)
                {
                    transform.localScale = new Vector2(-Xscale, Yscale);
                }
                else
                {
                    transform.localScale = new Vector2(Xscale, Yscale);
                }
            }
        }
    }

    void FixedUpdate()
    {
        //地上判定
        bool onGuound = Physics2D.CircleCast(transform.position,
                                            0.5f,
                                            Vector2.down,
                                            0.5f,
                                            groundLayer);
        if (onGuound)
        {
            //速度更新
            if (isToRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isToRight = !isToRight;
        time = 0;
        if (isToRight)
        {
            transform.localScale = new Vector2(-Xscale, Yscale);
        }
        else
        {
            transform.localScale = new Vector2(Xscale, Yscale);
        }
    }

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


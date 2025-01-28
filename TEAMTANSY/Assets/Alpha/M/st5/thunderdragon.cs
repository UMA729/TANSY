using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunderdragon : MonoBehaviour
{
    // 既存の変数
    public float hp = 10;
    public float speed = 2.0f;
    public float revTime = 0f;
    public float Xscale = 1.0f;
    public float Yscale = 1.0f;
    public float tickInterval = 2f;
    public float duration = 10f;
    public bool isToRight = false;
    public LayerMask groundLayer;
    public float jumpForce = 10f;
    public float jumpIntervalMin = 10f;
    public float jumpIntervalMax = 30f;
    private float nextJumpTime = 0f;
    private float nextThanderTime = 0f;
    private float Thanderduration = 3f;
    public AudioClip encon;

    public GameObject break_wall;

    // 追加する変数
    public GameObject bulletPrefab;  // 弾のプレハブ
    public Transform firePoint;      // 弾を発射する位置
    public float bulletSpeed = 5f;   // 弾のスピード
    private Transform playerTransform; // プレイヤーのTransform

    private Rigidbody2D rb;
    private bool isTakingDamage = false;
    private fireBullet FB;
    private float time = 0;

    //エフェクトプログラム
    public GameObject gameObject1;

    void Start()
    {
        gameObject1.SetActive(false); // gameObjectを非アクティブ化
        FB = FindObjectOfType<fireBullet>();
        rb = this.GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // プレイヤーをタグで検索
        if (isToRight)
        {
            transform.localScale = new Vector2(-2, 2);
        }
    }

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

        // プレイヤーに向かって弾を発射

        nextThanderTime += Time.deltaTime;

        if (nextThanderTime >= Thanderduration)
        {
            FireBulletAtPlayer();
            nextThanderTime = 0;
        }
    }

    void FixedUpdate()
    {
        // 地面判定
        bool onGround = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 0.5f, groundLayer);

        if (onGround)
        {
            // 速度更新
            Rigidbody2D rbody = GetComponent<Rigidbody2D>();
            if (isToRight)
            {
                rbody.velocity = new Vector2(speed, rbody.velocity.y);
            }
            else
            {
                rbody.velocity = new Vector2(-speed, rbody.velocity.y);
            }

            // ランダムジャンプ
            if (Time.time >= nextJumpTime)
            {
                Jump();
                nextJumpTime = Time.time + Random.Range(jumpIntervalMin, jumpIntervalMax);
            }
        }
    }

    // ジャンプ
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // ジャンプ
    }

    // プレイヤーに向かって弾を発射する処理
    private void FireBulletAtPlayer()
    {
        if (playerTransform == null) return;

        // プレイヤーの位置を取得
        Vector2 direction = (playerTransform.position - firePoint.position).normalized;

        // 弾を発射
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // 弾に速度を与える
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;
    }

    // 持続ダメージ
    public void StartDamageOverTime()
    {
        if (!isTakingDamage)
        {
            StartCoroutine(ApplyDamageOverTime());
        }
    }

    private IEnumerator ApplyDamageOverTime()
    {
        isTakingDamage = true;
        float elapsed = 0f;
        float firedamage = 0f;

        if (FB.fireBaff == false)
        {
            firedamage = 5f;
        }
        else if (FB.fireBaff == true)
        {
            firedamage = 10f;
            FB.fireBaff = false;
        }

        while (elapsed < duration)
        {
            TakeDamage(firedamage);
            yield return new WaitForSeconds(tickInterval);
            elapsed += tickInterval;
        }
        isTakingDamage = false;
    }

    // 衝突処理
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
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10f);

            gameObject1.SetActive(true); // gameObjectを非アクティブ化

            Invoke("Test1", 0.5f); // 関数Test1を3秒後に実行 　//エフェクトを消す
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.CompareTag("FireBullet"))
        {
            StartDamageOverTime();
        }

        // 壁に衝突した場合の反転
        if (collision.collider.CompareTag("Wall"))
        {
            ReverseDirection();
        }

        
    }

    // 壁に衝突した場合の処理
    private void ReverseDirection()
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

    // ダメージ処理
    public void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Die();
        }
    }

    // 死亡処理
    private void Die()
    {
        Destroy(gameObject);
        Destroy(break_wall);
    }

    void Test1()
    {
        gameObject1.SetActive(false); // gameObjectを非アクティブ化
    }

}

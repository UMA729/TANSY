using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyBoss : MonoBehaviour
{
    
    Rigidbody2D  rb;                     
    public float hp = 10f;               // HP
    public float speed = 5.0f;           // 徘徊時の動く速度
    public float Dirtime = 3.0f;         // 徘徊方向を切り替える間隔（秒）
    public float Ticktime = 2f;          // ダメージを与える間隔（秒）
    public float Atacktime = 1.0f;       // 攻撃をする感覚（秒）
    public float patrolSpeed = 2f;       // パトロール速度
    public float chaseSpeed = 3f;        // 追跡速度
    public float RayLen;
    public Transform player;             // プレイヤーの位置やスケールなど
    public bool isChasing = false;       // 追跡状態フラグ
    public GameObject EffectObj;
    public Transform EffectPos;
    public Transform Effectmam;
    public LayerMask MyLayer;
    private float DamDur = 0.0f;         // 火弾丸ダメージを与え続ける時間を計測する
    private float DirDur = 0.0f;         // 徘徊方向を切り替える時間を計測
    private bool isFacingRight = true;   // オブジェクトの向きフラグ
    private bool Direction = true;       // 
    private bool isTakingDamage = false; // 火弾丸をすでに受けているかのフラグ

    //敵攻撃
    //public Transform firePoint;          // 敵攻撃の発射位置
    //public GameObject firePrefab;        // 敵攻撃のプレハブ
    //public float fireSpeed = 2.0f;       // 敵攻撃の速度
    //private float AtaDur = 0.0f;         // 敵攻撃の間隔
    //private bool isAtackAria = false;    // 攻撃を開始する範囲

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

                            //敵攻撃間隔
                            //if (isAtackAria)                 
                            //{                                 //
                            //    AtaDur += Time.deltaTime;     //
                            //    if (Atacktime <= AtaDur)      //
                            //    {                             //
                            //        fireAtack();              //
                            //        AtaDur = 0.0f;            //
                            //    }                             //
                            //}                                 //
        }
    }
    private void FixedUpdate()
    {
       
        if (!isChasing)
        {
            float XScale = 6.0f; //
                                 // オブジェクトと同じスケール値に
            float YScale = 6.0f; //

            if (Direction)
            {
                transform.localScale = new Vector3(XScale, YScale, 1);
            }
            else if (!Direction)
            {
                transform.localScale = new Vector3(-XScale, YScale, 1);
            }

            rb.velocity = new Vector2(speed, rb.velocity.y);
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
        fireBullet FB;                          //fireBulletの呼び出し
        FB = FindAnyObjectByType<fireBullet>(); //fireBulletのオブジェクト読み込み

    　　isTakingDamage = true;          // 火弾丸を受けた
                                                
        float elapsed = 5f;             // 持続時間を追跡する変数

        float firedamage = 0f;          // 火ダメージを決める変数

        GameObject EfeObj = Instantiate(EffectObj, EffectPos.position, Quaternion.identity, Effectmam); //エフェクトを表示

        Destroy(EfeObj);

        if (FB.fireBaff == false)       //バフを未取得時
        {
            firedamage = 5f;
        }
        else if (FB.fireBaff == true)   //バフを取得時
        {
            firedamage = 10f;
        }

        while (elapsed < DamDur)
        {
            // ダメージを与える処理
            TakeDamage(firedamage);


            // 次のダメージまで待機
            yield return new WaitForSeconds(Ticktime);

            // 経過時間を更新
            elapsed += Ticktime;
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
        if (direction > 0 && isFacingRight)
        {
            FlipSprite();
        }
        // 左を向く場合
        else if (direction < 0 && !isFacingRight)
        {
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        // オブジェクトを反転
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // X軸を反転
        transform.localScale = scale;
    }
    void MoveEnemy() 
    {
        var hit = Physics2D.Raycast(transform.position, (player.position - transform.position).normalized * RayLen, RayLen, MyLayer);

        if (hit.collider != null)
        {
            Debug.Log("範囲内に入りました。");
        }

        Debug.DrawRay(transform.position, (player.position - transform.position).normalized * RayLen, Color.red);

        if (hit.collider != null && !isChasing)
        {
            isChasing = true;
        }
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

    //敵攻撃の開始
    //void fireAtack()
    //{                                                                                       //
    //    Vector2 direction = (player.position - firePoint.position).normalized;              //
    //
    //    GameObject fire = Instantiate(firePrefab, firePoint.position, Quaternion.identity); //
    //
    //    Rigidbody2D fireRb = fire.GetComponent<Rigidbody2D>();                              //
    //    fireRb.velocity = direction * fireSpeed;                                            //
    //}                                                                                       //

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float Damage = 10.0f;

        Debug.Log("このコライダーをもつオブジェクトネームは" + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Bullet"))        // 通常弾丸にあたると
        {
            TakeDamage(Damage);                             // 通常弾丸ダメージ
        }
        if (collision.gameObject.CompareTag("FireBullet"))    // 火弾丸にあたると
        {
            StartDamageOverTime();
        }
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;         //HPを引数からの数値で引く
        Debug.Log("ダメージを与えました。" + hp);
        if (hp <= 0)
        {
            Die();            //HPが0になったら入る
        }
    }

    private void Die()
    {
        Destroy(gameObject); //敵オブジェクトを削除
    }
}

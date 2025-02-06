using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    //定数
    Rigidbody2D rb;
    public float hp = 10f;               // HP
    public float speed = 5.0f;           // 徘徊時の動く速度
    public float Dirtime = 3.0f;         // 徘徊方向を切り替える間隔（秒）
    public float Ticktime = 2f;          // ダメージを与える間隔（秒）

    //エフェクトを出すために必要
    public GameObject EffectObj;         //エフェクトオブジェクト
    public Transform EffectPos;          //エフェクトを出す位置
    public Transform Effectmam;          //エフェクトの親オブジェクト

    private float DirDur = 0.0f;         // 徘徊方向を切り替える時間を計測
    private bool isFacingRight = true;   // オブジェクトの向きフラグ
    private bool isTakingDamage = false; // 火弾丸をすでに受けているかのフラグ


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        MoveEnemy();   //プレイヤー範囲外中
        
    }
    private void FixedUpdate()
    {

        float XScale = 4.0f; //
                             // オブジェクトと同じスケール値に
        float YScale = 4.0f; //

        if (isFacingRight)
        {
            transform.localScale = new Vector3(XScale, YScale, 1);
        }
        else if (!isFacingRight)
        {
            transform.localScale = new Vector3(-XScale, YScale, 1);
        }

        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    public void StartDamageOverTime()
    {
        if (!isTakingDamage) // すでにダメージを受けていない場合のみ開始
        {
            Debug.Log("入りました。");
            StartCoroutine(ApplyDamageOverTime());
        }
    }
    private IEnumerator ApplyDamageOverTime()
    {

        fireBullet FB;                          //fireBulletの呼び出し
        FB = FindAnyObjectByType<fireBullet>(); //fireBulletのオブジェクト読み込み

        isTakingDamage = true;          // 火弾丸を受けた

        float duration = 5.0f;
        float elapsed = 0.0f;             // 持続時間を追跡する変数
        float firedamage = 0f;          // 火ダメージを決める変数

        GameObject EfeObj = Instantiate(EffectObj, EffectPos.position, Quaternion.identity, Effectmam); //エフェクトを表示


        if (FB.fireBaff == false)       //バフを未取得時
        {
            firedamage = 5f;
        }
        else if (FB.fireBaff == true)   //バフを取得時
        {
            firedamage = 10f;
        }

        Debug.Log("火攻撃を開始します。");

        while (elapsed < duration)
        {
            // ダメージを与える処理
            TakeDamage(firedamage);


            // 次のダメージまで待機
            yield return new WaitForSeconds(Ticktime);

            // 経過時間を更新
            elapsed += Ticktime;
        }

        Destroy(EfeObj);
        isTakingDamage = false; // ダメージ完了

    }

    void MoveEnemy()
    {
        //パトロール時方向切り替え
        DirDur += Time.deltaTime;
        if (Dirtime <= DirDur)
        {
            if (isFacingRight)
            {
                isFacingRight = false;  //右向き時左向きに反転
            }
            else if (!isFacingRight)
            {
                isFacingRight = true;   //左向き時右向きに反転
            }
            speed *= -1.0f;         //方向切り替え時間がくると移動方向を反転
            DirDur = 0.0f;          //方向切り替え時間の初期化
        }
    }

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
            Debug.Log("当たりました。");
            StartDamageOverTime();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("needle"))
        {
            float Needam = 5;
            TakeDamage(Needam);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCommtller : MonoBehaviour
{
    public GameObject ballPrefab; // 発射する球のプレハブ
    public Transform shootingPoint; // 弾の発射位置
    public Transform player;  // プレイヤーのTransformをアサイン
    public float moveSpeed = 3f;
    public float jump = 6f;
    public GameObject bullet;
    public LayerMask groundLayer;
    public Transform groundCheck;     // 地面判定用のオブジェクト
    public float hp = 1000;
    public float Lenght = 5f;//プレイヤーが近づく距離の範囲
    public int deleteTime;//消す時間
    public bool isDelete = false;

    private Rigidbody2D rb;//Rigidbody2dコンポーネント
    private bool isGrounded;//地面にいるかどうか判定
    private float nexttime = 3f;//次のアクション
    private bool playerRange = false;//プレイヤーが範囲内に入っているのか
    public float actionInterval = 1f;  // ランダムな数を取得してスイッチ文を動かす間隔（秒）

    // アニメーション対応
    Animator animator; // アニメーション
    public string BossStopAnime = "BossStop";
    public string BossMoveAnime = "BossMove";
    public string wazastopAnime = "wazaStop";
    public string wazaAnime = "wazaMove";
    string nowAnime = "";
    string oldAnime = "";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        nowAnime = BossStopAnime;
        oldAnime = BossMoveAnime;

    }
    void Update()
    {
        if (player != null)
        {
            // プレイヤーの方向に向かって移動
            Vector2 direction = (player.position - transform.position).normalized;

            // ボスをプレイヤーの方向に移動
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        // 地面に接しているかを判定
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        Debug.Log("取ってるよｗ");
        if (isGrounded)
        {
            nowAnime = BossStopAnime;//停止中
        }
        else
        {
            nowAnime = BossMoveAnime;//移動
        }

        // プレイヤーが近づいているか確認
        if (Vector2.Distance(transform.position, player.position) < Lenght)
        {
            Debug.Log("来てるよ!!");
            if (!playerRange)
            {
                playerRange = true;
                Debug.Log("範囲内侵入!");
                
            }

            if(Time.time >= nexttime)
            {
                nexttime = Time.time + actionInterval;
                TriggerRandomEvent();  // プレイヤーが範囲内に入ったらランダムな処理を実行
            }
        }
        else
        {
            if(playerRange)
            {
                playerRange = false;
                Debug.Log("出ていきやがった");
            }
           
        }
    }

    void TriggerRandomEvent()
    {
        
            int rad = Random.Range(1, 6);//1~10の範囲内の数が取得される
            Debug.Log("rad:" + rad);
        switch (rad)
        {
            case 1:
                Debug.Log("ジャンプ");
                if (isGrounded)  //ジャンプ
                {
                    Jump();
                    Debug.Log("");
                }
                break;
            case 2:
                if (isGrounded)
                    Debug.Log("闇技");
                {
                   
                    // 弾丸を生成して発射位置に配置
                    GameObject bullet = Instantiate(ballPrefab, shootingPoint.position, Quaternion.identity);
                    if (!playerRange)
                    {
                        nowAnime = wazastopAnime;
                        Debug.Log("動いとります");
                    }
                    else
                    {
                        oldAnime = wazaAnime;
                        Debug.Log("こそ");

                    }
                    
                }
                
                break;
            case 3:
                Debug.Log("だああああンがん");
                if (player != null)
                {
                    Bullet();
                }
                break;
            case 4:

                Debug.Log("果然");
                break;
            case 5:

                Debug.Log("ヨシ");
                break;
            default:

                Debug.Log("デフォの処理");
                break;

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            hp -= 10;
            if (hp <= 0)
            {
                Die();
            }
        }
    }
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jump);  // ジャンプ力を加える
    }
    void Bullet()
    {

    }
    private void Die()
    {
        Destroy(gameObject);
    }
}

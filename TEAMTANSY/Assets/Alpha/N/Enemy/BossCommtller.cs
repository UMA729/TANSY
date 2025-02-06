using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UIを使うときに書きます。

/// <summary>
/// <see cref="player">playerリンク</see>
/// </summary>

public class BossCommtller : MonoBehaviour
{
    //public
    public int MaxHp = 300;     //最大HPと現在のHP。
    public float Hp;            //ボスのHP
    public Transform Player;  // プレイヤーのTransformをアサイン
    public float MoveSpeed = 3f;    //ボスの移動速度
    public float jump = 6f;         //ジャンプ
    public LayerMask GroundLayer;   //床を取るレイヤー
    public Transform GroundCheck;     // 地面判定用のオブジェクト
    public float Lenght = 5f;//プレイヤーが近づく距離の範囲
    public float DeleteTime = 2.0f;//消す時間
    public GameObject WallObject;
    public float ActionInterval = 1f;  // ランダムな数を取得してスイッチ文を動かす間隔（秒）

    //Slider
    public Slider Slider;   //ボスのHP用のスライダー

    //private
    private Rigidbody2D rb;//Rigidbody2dコンポーネント
    private bool IsGrounded;//地面にいるかどうか判定
    private float NextTime = 3f;//次のアクション
    private bool PlayerRange = false;//プレイヤーが範囲内に入っているのか
    private SpriteRenderer SpriteRenderer;//ボスのするスプライトレンダラー
    private EnemyBullet EB1;
    private EnemyBullet EB2;
    private Bossthunder BT;
    private GameManager GameManager;
    private bool isTouchingPlayer = false;
    private int count;

    // アニメーション対応
    Animator animator; // アニメーション
    public string BossStopAnime = "BossStop";
    public string BossMoveAnime = "BossMove";
    public string BossDeadAnime = "BossDead";
    public string wazastopAnime = "wazaStop";
    public string wazaAnime = "wazaMove";
    public string dangerAnime = "danger";
    public string RecoveryAnime = "BossHPRecovery";
    string nowAnime = "";
    string oldAnime = "";

    //+++ サウンド再生追加 +++
    public AudioClip jum;    
    public AudioClip thunder;    
    public AudioClip Hit;    //ダメージが当たった時   

    /// <summary>
    /// <seealso cref="EnemyBullet">近距離技</seealso>のリンク
    /// <seealso cref="Bossthunder">遠距離技</seealso>のリンク
    /// </summary>
    private void Start()
    {
        Slider.interactable = false;
        //Sliderを最大にする。
        Slider.value = Hp;
        //HPを最大HPと同じ値に。
        Hp = MaxHp;
        nowAnime = BossStopAnime;
        oldAnime = BossMoveAnime;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        GameManager = GetComponent<GameManager>();
        EB1 = GameObject.Find("AttackTechnique1").GetComponent<EnemyBullet>();  //攻撃枠にスクリプトをコンポーネントする
        EB2 = GameObject.Find("AttackTechnique2").GetComponent<EnemyBullet>();  //攻撃枠にスクリプトをコンポーネントする
        BT = GameObject.Find("Thunder").GetComponent<Bossthunder>();        //遠距離攻撃技にスクリプトをコンポーネント
    }

    /// <summary>
    /// アップデート関数の内容説明
    /// </summary>
    void Update()
    {
        if (Player != null)
        {
            // プレイヤーの方向に向かって移動
            Vector2 direction = (Player.position - transform.position).normalized;
            // ボスをプレイヤーの方向に移動
            transform.position = Vector2.MoveTowards(transform.position, Player.position, MoveSpeed * Time.deltaTime);
            if (Player.position.x > transform.position.x)
            {
                SpriteRenderer.flipX = true;
            }
            else
            {
                SpriteRenderer.flipX = false;
            }
        }
        // 地面に接しているかを判定
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, GroundLayer);
        if (IsGrounded)
        {
            nowAnime = BossStopAnime;//停止中
        }
        else
        {
            nowAnime = BossMoveAnime;//移動
        }

        // プレイヤーが近づいているか確認
        if (Vector2.Distance(transform.position, Player.position) < Lenght)
        {
            if (!PlayerRange)
            {
                PlayerRange = true;
            }

            if (Time.time >= NextTime)
            {
                NextTime = Time.time + ActionInterval;
                TriggerRandomEvent();  // プレイヤーが範囲内に入ったらランダムな処理を実行
            }
        }
        else
        {
            if (PlayerRange)
            {
                PlayerRange = false;
            }
        }

        if(isTouchingPlayer)
        {
            //プレイヤーが触れたら移動を止める
            rb.velocity = Vector2.zero;
        }
        else
        {
            Vector2 direction = (Player.position - transform.position).normalized;
        }

    }

    /// <summary>
    /// ターゲットランダムイベント関数の内容
    /// <see cref="Bossthunder">遠距離攻撃のリンク</see>
    /// <see cref="EnemyBullet">近距離のリンク</see>
    /// </summary>
    void TriggerRandomEvent()
    {
        
        int rad = Random.Range(1, 4);//1~10の範囲内の数が取得される
        count++;
        Debug.Log("rad:" + rad);
        Debug.Log("count:" + count);
        switch (rad)
        {
            case 1:
                if (IsGrounded != false)  //ジャンプ
                {
                    Jump();
                    //+++ サウンド再生追加 +++
                    //サウンド再生
                    AudioSource soundPlayer = GetComponent<AudioSource>();
                    if (soundPlayer != null)
                    {
                        //BGM停止
                        soundPlayer.Stop();
                        soundPlayer.PlayOneShot(jum);
                    }
                }
                break;
            case 2:
                //近距離攻
                if (IsGrounded != false)
                {
                    if (PlayerRange)
                    {
                        nowAnime = wazaAnime;   //攻撃のアニメーション
                        EB1.LaunchBall();
                        EB2.LaunchBall();
                    }
                }
                break;
            case 3:
                if(count >= 50)
                {
                    BT.FireBulletAtPlayer();
                    //+++ サウンド再生追加 +++
                    //サウンド再生
                    AudioSource soundPlayer = GetComponent<AudioSource>();
                    if (soundPlayer != null)
                    {
                        //BGM停止
                        soundPlayer.Stop();
                        soundPlayer.PlayOneShot(thunder);
                    }
                }
                break;
            case 4:
                if(Hp <= 10)
                {
                    nowAnime = RecoveryAnime;
                    Debug.Log("アニメーション動いてるよ！");
                    Hp += 5;
                    //HPをSliderに反映。
                    Slider.value = (float)Hp;
                }
                break;
        }
    }

    /// <summary>
    /// ターゲット関数
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //タグ"Bullet"に当たったらHPが減るように
        if(collision.gameObject.tag == "Bullet")
        {
            Hp -= 10;
            //HPをSliderに反映。
            Slider.value = (float)Hp;
            //+++ サウンド再生追加 +++
            //サウンド再生
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM停止
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(Hit);
            }
            if (Hp <= 0)
            {
                Die();
            }
        }
    }

    /// <summary>
    /// 触れたら変数を動かす
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
        }
    }


    /// <summary>
    /// ジャンプ関数
    /// </summary>
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jump);  // ジャンプ力を加える
    }

    /// <summary>
    /// デス関数
    /// </summary>
    private void Die()
    {
        animator.Play(BossDeadAnime);
        GetComponent<CapsuleCollider2D>().enabled = false;
        Destroy(gameObject);//ボスの削除
        Destroy(WallObject);//閉じてる壁の削除
    }

    /// <summary>
    /// ゲームストップ関数
    /// </summary>
    public void GameStop()
    {
        //Rigidbody2Dを取ってくる
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        //速度を0にして強制停止
        rbody.velocity = new Vector2(0, 0);

    }
}

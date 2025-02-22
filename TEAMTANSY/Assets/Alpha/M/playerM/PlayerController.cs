using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    Rigidbody2D rbody;          // Rigidbody2D 型の変数
    public float axisH = 0.0f;          //　入力
    public float speed = 3.0f; //移動
    public float jump = 9.0f;       // ジャンプ力
    public float duration = 5f;     //継続時間
    public float thunderdown = 0f;  //サンダーダメージを受ける
    public LayerMask groundLayer;   //　着地できるレイヤー
    bool goJump = false;            //　ジャンプ開始フラグ
    bool thunderhit = false;
    // アニメーション対応
    Animator animator; // アニメーション
    public string stopAnime = "PlayerStop"; //ストップ
    public string moveAnime = "PlayerMove"; //移動
    public string JumpAnime = "PlayerJump"; //ジャンプ
    public string goalAnime = "PlayerGoal"; //ゴール
    public string deadAnime = "PlayerOver"; //ゲームオーバー
    string nowAnime = "";
    string oldAnime = "";

    public AudioClip jumpvoice;

    public static string gameState = "playing"; // ゲームの状態

    private PlayerRopeSwing PRS;

    //スタート関数
    //説明
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ItemKeeper.hasMagicBook);
        thunderhit = false;
        PRS = FindObjectOfType<PlayerRopeSwing>();
        // Rigidbody2Dを取ってくる
        rbody = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        //　Animator を取ってくる
        nowAnime = stopAnime;                       //　停止後から開始する
        oldAnime = stopAnime;                       //　停止後から開始する

        gameState = "playing";  // ゲーム中にする
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState !="playing")
        {
            return;
        }

        if (!PRS.isSwinging)
        {
            PlayerAndGrappleDirection();

            // キャラクターをジャンプさせる
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        if (thunderhit == true) //サンダーヒット
        {
            thunderdown += Time.deltaTime;  //時間
            
            if (thunderdown >= duration)    //継続時間
            {
                Debug.Log("速度が戻りました");  //サンダーヒットで遅くなった速度を元に戻す　元の速度は5
                speed += 2.0f;
                thunderhit = false;
                thunderdown = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (gameState !="playing")  //ゲームの状態がこれ以外ならこの以降のスクリプトを無視
        {
            return;
        }

        // 地上判定
        bool onGround = Physics2D.CircleCast(transform.position,                // 発射位置
                                                                 0.2f,          // 円の半径
                                                                 Vector2.down,  //発射方向
                                                                 0.0f,          //発射距離
                                                                 groundLayer);  //検出レイヤー
        if (onGround || axisH != 0 && !PRS.isSwinging)
        {
            //地面の上でジャンプスキーが押された
            // 速度を更新する
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
        if (onGround && goJump)
        {
            // 地面の上でジャンプが押された
            // ジャンプさせる
            Vector2 jumpPw = new Vector2(0, jump);  // ジャンプさせるベクトルを作る
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //　瞬間的な力を加える
            goJump = false; //　ジャンプフラグを下ろす


            //+++ サウンド再生追加 +++
            //サウンド再生
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM停止
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(jumpvoice);

            }
        }


        // アニメーション更新
        if (onGround)
        {
            // 地面の上
            if (axisH == 0)
            {
                nowAnime = stopAnime;   //停止中
            }
            else
            {
                nowAnime = moveAnime;   // 移動
            }
        }
        else
        {
            // 空中
            nowAnime = JumpAnime;
        }
        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);    // アニメーション再生
        }   

    }
    // ジャンプ
    public void Jump()
    {
        goJump = true; //ジャンプフラグを立てる
    }
    // 接触開始
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "thunder" )
        {
            Debug.Log("遅くなりました");
            if (thunderhit == false)
            {
                speed -= 2.0f; //移動速度低下
                thunderhit = true;

            }
        }

        if (collision.gameObject.tag == "Goal")
        {
            Goal();     //ゴール！！
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver(); //  ゲームオーバー 
        }
    }


    //ゴール
    public void Goal()
    {
        animator.Play(goalAnime);

        gameState = "gameclear";
        GameStop();// ゲーム停止
    }
    //ゲームオーバー
    void GameOver()
    {
        Debug.Log("ゲームオーバー");
        animator.Play(deadAnime);

        gameState = "gameover";
        GameStop();//ゲーム停止
        //ゲーム演出
        //プレイヤー当たり判定を消す
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        //プレイヤーを上に少し上げる演出
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }
    //ゲーム停止
    public void GameStop()
    {
        //Rigidbody2Dを取ってくる
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        //速度を0にして強制停止
        rbody.velocity = new Vector2(0,0);

    }

    private void PlayerAndGrappleDirection()
    {
        // 水平方向の入力をチェックする
        axisH = Input.GetAxisRaw("Horizontal");

        float Angleright   = 115;
        float Angleleft    = 70;
        float Anglechange = 45;

        // フック発射時と主人公の向きの調整
        if (axisH > 0.0f)
        {
            //右移動
            transform.localScale = new Vector2(0.5f, 0.5f);

            if (PRS.launchAngle == Angleright)
            {
                PRS.launchAngle -= Anglechange;
            }
        }
        else if (axisH < 0.0f)
        {
            // 左移動
            transform.localScale = new Vector2(-0.5f, 0.5f); //左右反転させる
            if (PRS.launchAngle == Angleleft)
            {
                PRS.launchAngle += Anglechange;
            }
        }
    }
 }


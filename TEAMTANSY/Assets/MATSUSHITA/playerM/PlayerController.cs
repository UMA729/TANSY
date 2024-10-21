using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;          // Rigidbody2D 型の変数
    float axisH = 0.0f;          //　入力
    public float speed = 3.0f; //移動

    public float jump = 9.0f;       // ジャンプ力
    public LayerMask groundLayer;   //　着地できるレイヤー
    bool goJump = false;            //　ジャンプ開始フラグ

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2Dを取ってくる
        rbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 水平方向の入力をチェックする
        axisH = Input.GetAxisRaw("Horizontal");
        // 向きの調整
        if (axisH > 0.0f)
        {
            //右移動
            Debug.Log("右移動");
            transform.localScale = new Vector2(0.5f, 0.5f);
        }
        else if (axisH < 0.0f)
        {
            // 左移動
            Debug.Log("左移動");
            transform.localScale = new  Vector2(-0.5f, 0.5f); //左右反転させる
        }

        // キャラクターをジャンプさせる
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // 地上判定
        bool onGround = Physics2D.CircleCast(transform.position,                // 発射位置
                                                                 1.5f,          // 円の半径
                                                                 Vector2.down,  //発射方向
                                                                 0.0f,          //発射距離
                                                                 groundLayer);  //検出レイヤー
        if (onGround || axisH != 0)
        {
        //地面の上でジャンプスキーが押された
        // 速度を更新する
        rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
        if(onGround && goJump)
        {
            // 地面の上でジャンプが押された
            // ジャンプさせる
            Vector2 jumpPw = new Vector2(0, jump);  // ジャンプさせるベクトルを作る
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //　瞬間的な力を加える
            goJump = false; //　ジャンプフラグを下ろす
        }
    }
    // ジャンプ
    public void Jump()
    {
        goJump = true; //ジャンプフラグを立てる
    }
}

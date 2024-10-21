using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 5.0f;

    public float jump = 9.0f;
    public LayerMask groundLayer;
    bool goJump = false;

    public static string gameState = "playing";
    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();

        gameState = "playing";  //ゲーム中にする
    }

    // Update is called once per frame
    void Update()
    {

        if(gameState != "playing")
        {
            return;
        }
        axisH = Input.GetAxisRaw("Horizontal");
        if(axisH > 0.0f)
        {
            transform.localScale = new Vector2(5, 5);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector2(-5, 5);
        }

        //ジャンプ
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if(gameState != "playing")
        {
            return;
        }
        //地上判定
        bool onGround = Physics2D.CircleCast(transform.position,
                                                0.2f,
                                                Vector2.down,
                                                0.0f,
                                                groundLayer);
        if(onGround || axisH != 0)
        {
            //地面の上 or 速度が０ではない
            //速度を更新
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
        if(onGround && goJump)
        {
            //ジャンプキーが押された
            //じゃんぷさせる
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;//ジャンプを下す
        }
    }
    //ジャンプ
    public void Jump()
    {
        goJump = true;//ジャンプフラグを立てる
    }
   
}

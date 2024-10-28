using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    public float jumpForce = 10f; // ジャンプ力
    private bool isGrounded; // 地面にいるかどうかを確認するフラグ
    private Rigidbody2D rb;

    public int maxHP = 100;
    private int currentHP;
    public int maxMP = 100;
    private int crrrentMP;
    private Animator animator;

    public static string gameState = "playing";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        gameState = "playing";//ゲーム中にする
    }

    void Update()
    {
        Move();
        Jump();

        if(gameState != "playing")
        {
            return;
        }
    }

    void FixedUpdate()
    {
        if(gameState != "playing")
        {
            return;
        }
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Graund"))
        {
            isGrounded = true; // 地面に接触したらフラグを立てる
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Graund"))
        {
            isGrounded = false; // 地面から離れたらフラグを下げる
        }
    }

    public void GameOver()
    {
        gameState = "gameover";
        GameStop();
    }
    //ゲーム停止
    void GameStop()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(0, 0);
    }
   
}
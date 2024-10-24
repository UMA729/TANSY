using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 5.0f;

    public float jump = 9.0f;
    public LayerMask groundLayer;
    bool goJump = false;

    public int maxHP = 100;
    private int currentHP;
    private Animator animator;


    public static string gameState = "playing";
    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();

        gameState = "playing";

        currentHP = maxHP;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != "palying")
        {
            return;
        }

        axisH = Input.GetAxisRaw("Horizontal");
        if (axisH > 0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (gameState != "palying")
        {
            return;
        }

        bool onGround = Physics2D.CircleCast(transform.position,
                                            0.2f,
                                            Vector2.down,
                                            0.0f,
                                            groundLayer);
        if (onGround || axisH != 0)
        {
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
        if (onGround && goJump)
        {
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;
        }
    }
    public void Jump()
    {
        goJump = true;
    }

    public void GameOver()
    {
        gameState = "gameover";
        GameStop();

        GetComponent<CapsuleCollider2D>().enabled = false;
    }
    void GameStop()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();

    }

    public void TakeDamege(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);

        if (damage > 0)
        {
            animator.SetTrigger("TakeDamage");
        }
    }
}


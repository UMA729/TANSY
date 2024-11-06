using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed = 5f; // �ړ����x
    public float jumpForce = 10f; // �W�����v��
    private bool isGrounded; // �n�ʂɂ��邩�ǂ������m�F����t���O
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

        gameState = "playing";//�Q�[�����ɂ���
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
            isGrounded = true; // �n�ʂɐڐG������t���O�𗧂Ă�
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Graund"))
        {
            isGrounded = false; // �n�ʂ��痣�ꂽ��t���O��������
        }
    }

    public void GameOver()
    {
        gameState = "gameover";
        GameStop();
    }
    //�Q�[����~
    void GameStop()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(0, 0);
    }
   
}
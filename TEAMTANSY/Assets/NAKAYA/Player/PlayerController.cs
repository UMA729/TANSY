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

        gameState = "playing";  //�Q�[�����ɂ���
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

        //�W�����v
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
        //�n�㔻��
        bool onGround = Physics2D.CircleCast(transform.position,
                                                0.2f,
                                                Vector2.down,
                                                0.0f,
                                                groundLayer);
        if(onGround || axisH != 0)
        {
            //�n�ʂ̏� or ���x���O�ł͂Ȃ�
            //���x���X�V
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
        if(onGround && goJump)
        {
            //�W�����v�L�[�������ꂽ
            //�����Ղ�����
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;//�W�����v������
        }
    }
    //�W�����v
    public void Jump()
    {
        goJump = true;//�W�����v�t���O�𗧂Ă�
    }
   
}

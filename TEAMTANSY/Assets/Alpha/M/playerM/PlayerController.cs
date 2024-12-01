using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    Rigidbody2D rbody;          // Rigidbody2D �^�̕ϐ�
    public float axisH = 0.0f;          //�@����
    public float speed = 3.0f; //�ړ�
    public float jump = 9.0f;       // �W�����v��
    public float duration = 5f;
    public float thunderdown = 0f;
    public LayerMask groundLayer;   //�@���n�ł��郌�C���[
    bool goJump = false;            //�@�W�����v�J�n�t���O
    bool thunderhit = false;
    // �A�j���[�V�����Ή�
    Animator animator; // �A�j���[�V����
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string JumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";

    public static string gameState = "playing"; // �Q�[���̏��

    private PlayerRopeSwing PRS;
    // Start is called before the first frame update
    void Start()
    {
        thunderhit = false;
        PRS = FindObjectOfType<PlayerRopeSwing>();
        // Rigidbody2D������Ă���
        rbody = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        //�@Animator ������Ă���
        nowAnime = stopAnime;                       //�@��~�ォ��J�n����
        oldAnime = stopAnime;                       //�@��~�ォ��J�n����

        gameState = "playing";  // �Q�[�����ɂ���
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
            // ���������̓��͂��`�F�b�N����
            axisH = Input.GetAxisRaw("Horizontal");
            // �����̒���
            if (axisH > 0.0f)
            {
                //�E�ړ�
                transform.localScale = new Vector2(0.5f, 0.5f);

                if (PRS.launchAngle == 115)
                {
                    PRS.launchAngle -= 45;
                }
            }
            else if (axisH < 0.0f)
            {
                // ���ړ�
                transform.localScale = new Vector2(-0.5f, 0.5f); //���E���]������
                if (PRS.launchAngle == 70)
                {
                    PRS.launchAngle += 45;
                }
            }


            // �L�����N�^�[���W�����v������
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        if (thunderhit == true)
        {
            thunderdown += Time.deltaTime;
            
            if (thunderdown >= duration)
            {
                Debug.Log("���x���߂�܂���");
                speed += 2.0f;
                thunderhit = false;
                thunderdown = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (gameState !="playing")
        {
            return;
        }

        // �n�㔻��
        bool onGround = Physics2D.CircleCast(transform.position,                // ���ˈʒu
                                                                 0.2f,          // �~�̔��a
                                                                 Vector2.down,  //���˕���
                                                                 0.0f,          //���ˋ���
                                                                 groundLayer);  //���o���C���[
        if (onGround || axisH != 0 && !PRS.isSwinging)
        {
            //�n�ʂ̏�ŃW�����v�X�L�[�������ꂽ
            // ���x���X�V����
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
        if (onGround && goJump)
        {
            // �n�ʂ̏�ŃW�����v�������ꂽ
            // �W�����v������
            Vector2 jumpPw = new Vector2(0, jump);  // �W�����v������x�N�g�������
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //�@�u�ԓI�ȗ͂�������
            goJump = false; //�@�W�����v�t���O�����낷
        }


        // �A�j���[�V�����X�V
        if (onGround)
        {
            // �n�ʂ̏�
            if (axisH == 0)
            {
                nowAnime = stopAnime;   //��~��
            }
            else
            {
                nowAnime = moveAnime;   // �ړ�
            }
        }
        else
        {
            // ��
            nowAnime = JumpAnime;
        }
        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);    // �A�j���[�V�����Đ�
        }   

    }
    // �W�����v
    public void Jump()
    {
        goJump = true; //�W�����v�t���O�𗧂Ă�
    }
    // �ڐG�J�n
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "thunder" )
        {
            Debug.Log("�x���Ȃ�܂���");
            if (thunderhit == false)
            {
                speed -= 2.0f; //�ړ�
                thunderhit = true;

            }
        }

        if (collision.gameObject.tag == "Goal")
        {
            Goal();     //�S�[���I�I
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver(); //  �Q�[���I�[�o�[ 
        }
    }


    //�S�[��
    public void Goal()
    {
        animator.Play(goalAnime);

        gameState = "gameclear";
        GameStop();// �Q�[����~
    }
    //�Q�[���I�[�o�[
    void GameOver()
    {
        Debug.Log("�Q�[���I�[�o�[");
        animator.Play(deadAnime);

        gameState = "gameover";
        GameStop();//�Q�[����~
        //======================
        //�Q�[�����o
        //=======================
        //�v���C���[�����������
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        //�v���C���[����ɏ����グ�鉉�o
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }
    //�Q�[����~
    public void GameStop()
    {
        //Rigidbody2D������Ă���
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        //���x��0�ɂ��ċ�����~
        rbody.velocity = new Vector2(0,0);

    }

 }
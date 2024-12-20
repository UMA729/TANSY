using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCommtller : MonoBehaviour
{ 
    public Transform player;  // �v���C���[��Transform���A�T�C��
    public float moveSpeed = 3f;    //�{�X�̈ړ����x
    public float jump = 6f;         //�W�����v
    public LayerMask groundLayer;   //������郌�C���[
    public Transform groundCheck;     // �n�ʔ���p�̃I�u�W�F�N�g
    public float hp = 1000;             //�{�X��HP
    public float Lenght = 5f;//�v���C���[���߂Â������͈̔�
    public float deleteTime = 2.0f;//��������
    public bool isDelete = false;
    public GameObject WallObject;

    private Rigidbody2D rb;//Rigidbody2d�R���|�[�l���g
    private bool isGrounded;//�n�ʂɂ��邩�ǂ�������
    private float nexttime = 3f;//���̃A�N�V����
    private bool playerRange = false;//�v���C���[���͈͓��ɓ����Ă���̂�
    private SpriteRenderer spriteRenderer;//�{�X�̂���X�v���C�g�����_���[
    public float actionInterval = 1f;  // �����_���Ȑ����擾���ăX�C�b�`���𓮂����Ԋu�i�b�j
    private EnemyBullet EB;
    private EnemyBullet EB2;
    private GameManager GameManager;


    // �A�j���[�V�����Ή�
    Animator animator; // �A�j���[�V����
    public string BossStopAnime = "BossStop";
    public string BossMoveAnime = "BossMove";
    public string wazastopAnime = "wazaStop";
    public string wazaAnime = "wazaMove";
    string nowAnime = "";
    string oldAnime = "";

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip BSS;    //�e����
    public AudioClip jum;    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        nowAnime = BossStopAnime;
        oldAnime = BossMoveAnime;
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager = GetComponent<GameManager>();
        EB = GameObject.Find("waza1").GetComponent<EnemyBullet>();
        EB2 = GameObject.Find("waza2").GetComponent<EnemyBullet>();
    }
    void Update()
    {
        if (player != null)
        {
            // �v���C���[�̕����Ɍ������Ĉړ�
            Vector2 direction = (player.position - transform.position).normalized;
            // �{�X���v���C���[�̕����Ɉړ�
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            if(player.position.x > transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        // �n�ʂɐڂ��Ă��邩�𔻒�
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        Debug.Log("����Ă�悗");
        if (isGrounded)
        {
            nowAnime = BossStopAnime;//��~��
        }
        else
        {
            nowAnime = BossMoveAnime;//�ړ�
        }

        // �v���C���[���߂Â��Ă��邩�m�F
        if (Vector2.Distance(transform.position, player.position) < Lenght)
        {
            Debug.Log("���Ă��!!");
            if (!playerRange)
            {
                playerRange = true;
                Debug.Log("�͈͓��N��!");

            }

            if (Time.time >= nexttime)
            {
                nexttime = Time.time + actionInterval;
                TriggerRandomEvent();  // �v���C���[���͈͓��ɓ������烉���_���ȏ��������s
            }
        }
        else
        {
            if (playerRange)
            {
                playerRange = false;
                Debug.Log("�o�Ă����₪����");
            }

        }

    }

    void TriggerRandomEvent()
    {
        
            int rad = Random.Range(1, 3);//1~10�͈͓̔��̐����擾�����
            Debug.Log("rad:" + rad);
        switch (rad)
        {
            case 1:
                Debug.Log("�W�����v");
                if (isGrounded)  //�W�����v
                {
                    Jump();
                    //+++ �T�E���h�Đ��ǉ� +++
                    //�T�E���h�Đ�
                    AudioSource soundPlayer = GetComponent<AudioSource>();
                    if (soundPlayer != null)
                    {
                        //BGM��~
                        soundPlayer.Stop();
                        soundPlayer.PlayOneShot(jum);
                    }
                    Debug.Log("�Ȃ�܂���");
                    Debug.Log("");
                }
                break;
            case 2:
                
                if (isGrounded == true)
                {
                    Debug.Log("�ŋZ");
                    if (playerRange)
                    {
                        Debug.Log("�����Ƃ�܂�");
                        nowAnime = wazaAnime;
                        EB.LaunchBall();
                        EB2.LaunchBall();
                    }
                }
                
                break;
            case 3:
                Debug.Log("����������������");
                if (player)
                {
                    
                }
                break;
            case 4:
                Debug.Log("�ʎ�");
               


                break;
            case 5:

                Debug.Log("���V");
                break;
            default:

                Debug.Log("�f�t�H�̏���");
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
        rb.velocity = new Vector2(rb.velocity.x, jump);  // �W�����v�͂�������
    }
    private void Die()
    {
        Destroy(gameObject);//�{�X������
        Destroy(WallObject);//���Ă�ǂ̍폜
    }

    public void GameStop()
    {
        //Rigidbody2D������Ă���
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        //���x��0�ɂ��ċ�����~
        rbody.velocity = new Vector2(0, 0);

    }
}

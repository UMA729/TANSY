using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCommtller : MonoBehaviour
{
    public GameObject ballPrefab; // ���˂��鋅�̃v���n�u
    public Transform shootingPoint; // �e�̔��ˈʒu
    public Transform player;  // �v���C���[��Transform���A�T�C��
    public float moveSpeed = 3f;
    public float jump = 6f;
    public GameObject bullet;
    public LayerMask groundLayer;
    public Transform groundCheck;     // �n�ʔ���p�̃I�u�W�F�N�g
    public float hp = 1000;
    public float Lenght = 5f;//�v���C���[���߂Â������͈̔�
    public int deleteTime;//��������
    public bool isDelete = false;

    private Rigidbody2D rb;//Rigidbody2d�R���|�[�l���g
    private bool isGrounded;//�n�ʂɂ��邩�ǂ�������
    private float nexttime = 3f;//���̃A�N�V����
    private bool playerRange = false;//�v���C���[���͈͓��ɓ����Ă���̂�
    public float actionInterval = 1f;  // �����_���Ȑ����擾���ăX�C�b�`���𓮂����Ԋu�i�b�j

    // �A�j���[�V�����Ή�
    Animator animator; // �A�j���[�V����
    public string BossStopAnime = "BossStop";
    public string BossMoveAnime = "BossMove";
    public string wazastopAnime = "wazaStop";
    public string wazaAnime = "wazaMove";
    string nowAnime = "";
    string oldAnime = "";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        nowAnime = BossStopAnime;
        oldAnime = BossMoveAnime;

    }
    void Update()
    {
        if (player != null)
        {
            // �v���C���[�̕����Ɍ������Ĉړ�
            Vector2 direction = (player.position - transform.position).normalized;

            // �{�X���v���C���[�̕����Ɉړ�
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
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

            if(Time.time >= nexttime)
            {
                nexttime = Time.time + actionInterval;
                TriggerRandomEvent();  // �v���C���[���͈͓��ɓ������烉���_���ȏ��������s
            }
        }
        else
        {
            if(playerRange)
            {
                playerRange = false;
                Debug.Log("�o�Ă����₪����");
            }
           
        }
    }

    void TriggerRandomEvent()
    {
        
            int rad = Random.Range(1, 6);//1~10�͈͓̔��̐����擾�����
            Debug.Log("rad:" + rad);
        switch (rad)
        {
            case 1:
                Debug.Log("�W�����v");
                if (isGrounded)  //�W�����v
                {
                    Jump();
                    Debug.Log("");
                }
                break;
            case 2:
                if (isGrounded)
                    Debug.Log("�ŋZ");
                {
                   
                    // �e�ۂ𐶐����Ĕ��ˈʒu�ɔz�u
                    GameObject bullet = Instantiate(ballPrefab, shootingPoint.position, Quaternion.identity);
                    if (!playerRange)
                    {
                        nowAnime = wazastopAnime;
                        Debug.Log("�����Ƃ�܂�");
                    }
                    else
                    {
                        oldAnime = wazaAnime;
                        Debug.Log("����");

                    }
                    
                }
                
                break;
            case 3:
                Debug.Log("����������������");
                if (player != null)
                {
                    Bullet();
                }
                break;
            case 4:

                Debug.Log("�ʑR");
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
    void Bullet()
    {

    }
    private void Die()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI���g���Ƃ��ɏ����܂��B

/// <summary>
/// <see cref="player">player�����N</see>
/// </summary>

public class BossCommtller : MonoBehaviour
{
    //public
    public int MaxHp = 300;     //�ő�HP�ƌ��݂�HP�B
    public float Hp;            //�{�X��HP
    public Transform Player;  // �v���C���[��Transform���A�T�C��
    public float MoveSpeed = 3f;    //�{�X�̈ړ����x
    public float jump = 6f;         //�W�����v
    public LayerMask GroundLayer;   //������郌�C���[
    public Transform GroundCheck;     // �n�ʔ���p�̃I�u�W�F�N�g
    public float Lenght = 5f;//�v���C���[���߂Â������͈̔�
    public float DeleteTime = 2.0f;//��������
    public GameObject WallObject;
    public float ActionInterval = 1f;  // �����_���Ȑ����擾���ăX�C�b�`���𓮂����Ԋu�i�b�j

    //Slider
    public Slider Slider;   //�{�X��HP�p�̃X���C�_�[

    //private
    private Rigidbody2D rb;//Rigidbody2d�R���|�[�l���g
    private bool IsGrounded;//�n�ʂɂ��邩�ǂ�������
    private float NextTime = 3f;//���̃A�N�V����
    private bool PlayerRange = false;//�v���C���[���͈͓��ɓ����Ă���̂�
    private SpriteRenderer SpriteRenderer;//�{�X�̂���X�v���C�g�����_���[
    private EnemyBullet EB1;
    private EnemyBullet EB2;
    private Bossthunder BT;
    private GameManager GameManager;
    private bool isTouchingPlayer = false;
    private int count;

    // �A�j���[�V�����Ή�
    Animator animator; // �A�j���[�V����
    public string BossStopAnime = "BossStop";
    public string BossMoveAnime = "BossMove";
    public string BossDeadAnime = "BossDead";
    public string wazastopAnime = "wazaStop";
    public string wazaAnime = "wazaMove";
    public string dangerAnime = "danger";
    public string RecoveryAnime = "BossHPRecovery";
    string nowAnime = "";
    string oldAnime = "";

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip jum;    
    public AudioClip thunder;    
    public AudioClip Hit;    //�_���[�W������������   

    /// <summary>
    /// <seealso cref="EnemyBullet">�ߋ����Z</seealso>�̃����N
    /// <seealso cref="Bossthunder">�������Z</seealso>�̃����N
    /// </summary>
    private void Start()
    {
        Slider.interactable = false;
        //Slider���ő�ɂ���B
        Slider.value = Hp;
        //HP���ő�HP�Ɠ����l�ɁB
        Hp = MaxHp;
        nowAnime = BossStopAnime;
        oldAnime = BossMoveAnime;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        GameManager = GetComponent<GameManager>();
        EB1 = GameObject.Find("AttackTechnique1").GetComponent<EnemyBullet>();  //�U���g�ɃX�N���v�g���R���|�[�l���g����
        EB2 = GameObject.Find("AttackTechnique2").GetComponent<EnemyBullet>();  //�U���g�ɃX�N���v�g���R���|�[�l���g����
        BT = GameObject.Find("Thunder").GetComponent<Bossthunder>();        //�������U���Z�ɃX�N���v�g���R���|�[�l���g
    }

    /// <summary>
    /// �A�b�v�f�[�g�֐��̓��e����
    /// </summary>
    void Update()
    {
        if (Player != null)
        {
            // �v���C���[�̕����Ɍ������Ĉړ�
            Vector2 direction = (Player.position - transform.position).normalized;
            // �{�X���v���C���[�̕����Ɉړ�
            transform.position = Vector2.MoveTowards(transform.position, Player.position, MoveSpeed * Time.deltaTime);
            if (Player.position.x > transform.position.x)
            {
                SpriteRenderer.flipX = true;
            }
            else
            {
                SpriteRenderer.flipX = false;
            }
        }
        // �n�ʂɐڂ��Ă��邩�𔻒�
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, GroundLayer);
        if (IsGrounded)
        {
            nowAnime = BossStopAnime;//��~��
        }
        else
        {
            nowAnime = BossMoveAnime;//�ړ�
        }

        // �v���C���[���߂Â��Ă��邩�m�F
        if (Vector2.Distance(transform.position, Player.position) < Lenght)
        {
            if (!PlayerRange)
            {
                PlayerRange = true;
            }

            if (Time.time >= NextTime)
            {
                NextTime = Time.time + ActionInterval;
                TriggerRandomEvent();  // �v���C���[���͈͓��ɓ������烉���_���ȏ��������s
            }
        }
        else
        {
            if (PlayerRange)
            {
                PlayerRange = false;
            }
        }

        if(isTouchingPlayer)
        {
            //�v���C���[���G�ꂽ��ړ����~�߂�
            rb.velocity = Vector2.zero;
        }
        else
        {
            Vector2 direction = (Player.position - transform.position).normalized;
        }

    }

    /// <summary>
    /// �^�[�Q�b�g�����_���C�x���g�֐��̓��e
    /// <see cref="Bossthunder">�������U���̃����N</see>
    /// <see cref="EnemyBullet">�ߋ����̃����N</see>
    /// </summary>
    void TriggerRandomEvent()
    {
        
        int rad = Random.Range(1, 4);//1~10�͈͓̔��̐����擾�����
        count++;
        Debug.Log("rad:" + rad);
        Debug.Log("count:" + count);
        switch (rad)
        {
            case 1:
                if (IsGrounded != false)  //�W�����v
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
                }
                break;
            case 2:
                //�ߋ����U
                if (IsGrounded != false)
                {
                    if (PlayerRange)
                    {
                        nowAnime = wazaAnime;   //�U���̃A�j���[�V����
                        EB1.LaunchBall();
                        EB2.LaunchBall();
                    }
                }
                break;
            case 3:
                if(count >= 50)
                {
                    BT.FireBulletAtPlayer();
                    //+++ �T�E���h�Đ��ǉ� +++
                    //�T�E���h�Đ�
                    AudioSource soundPlayer = GetComponent<AudioSource>();
                    if (soundPlayer != null)
                    {
                        //BGM��~
                        soundPlayer.Stop();
                        soundPlayer.PlayOneShot(thunder);
                    }
                }
                break;
            case 4:
                if(Hp <= 10)
                {
                    nowAnime = RecoveryAnime;
                    Debug.Log("�A�j���[�V���������Ă��I");
                    Hp += 5;
                    //HP��Slider�ɔ��f�B
                    Slider.value = (float)Hp;
                }
                break;
        }
    }

    /// <summary>
    /// �^�[�Q�b�g�֐�
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�^�O"Bullet"�ɓ���������HP������悤��
        if(collision.gameObject.tag == "Bullet")
        {
            Hp -= 10;
            //HP��Slider�ɔ��f�B
            Slider.value = (float)Hp;
            //+++ �T�E���h�Đ��ǉ� +++
            //�T�E���h�Đ�
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM��~
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(Hit);
            }
            if (Hp <= 0)
            {
                Die();
            }
        }
    }

    /// <summary>
    /// �G�ꂽ��ϐ��𓮂���
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
        }
    }


    /// <summary>
    /// �W�����v�֐�
    /// </summary>
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jump);  // �W�����v�͂�������
    }

    /// <summary>
    /// �f�X�֐�
    /// </summary>
    private void Die()
    {
        animator.Play(BossDeadAnime);
        GetComponent<CapsuleCollider2D>().enabled = false;
        Destroy(gameObject);//�{�X�̍폜
        Destroy(WallObject);//���Ă�ǂ̍폜
    }

    /// <summary>
    /// �Q�[���X�g�b�v�֐�
    /// </summary>
    public void GameStop()
    {
        //Rigidbody2D������Ă���
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        //���x��0�ɂ��ċ�����~
        rbody.velocity = new Vector2(0, 0);

    }
}

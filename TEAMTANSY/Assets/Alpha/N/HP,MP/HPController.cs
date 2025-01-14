using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI���g���Ƃ��ɏ����܂��B

public class HPController : MonoBehaviour
{
    //�ő�HP�ƌ��݂�HP�B
    int maxHp = 100;
    int Hp;
    private PlayerController player;
    public int recoveryAmout = 10;  //1��̉񕜗�
    public float recoveryInterval = 10f;    //�񕜂̊Ԋu
    public int consumptionAmount = 10;
    public bool lighthit = false;         //���̑��i����
    public bool Deth = false;             //���S���Ă��邩
    public bool nextDamage = false;       //��ԓG�̑��i�_���[�W����
    public float damageRevive = 5;        //���_���[�W�̕����܂ł̎���
    public float nextD = 0.5f;            //�G�ɐG�ꂽ�Ƃ����̃_���[�W����������܂ł̎���
    public float duration = 0;            //�o�ߎ���

    private bool onNeedle = false;        //�g�Q�̏�ɂ��邩�ǂ���
    private float Ntime = 0;

    Animator animator; // �A�j���[�V����
    public string deadAnime = "PlayerOver";
    public static string gameState = "playing"; // �Q�[���̏��
    //Slider
    public Slider slider;

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip meHP;    //�e����
    public AudioClip Nsound;  //�g�Q�_���[�W���ʉ�

    void Start()
    {
        slider.interactable = false;
        //Slider���ő�ɂ���B
        slider.value = 100;
        //HP���ő�HP�Ɠ����l�ɁB
        Hp = maxHp;
        if(Deth == false)
        StartCoroutine(RecoverHP());    //�ŏ��̉񕜎��Ԃ�ݒ�

        animator = GetComponent<Animator>();        //�@Animator ������Ă���
        gameState = "playing";  // �Q�[�����ɂ���
    }

    private void Update()
    {
        if (onNeedle)
        {
            Debug.Log("�g�Q�̏�ł�");
            NeedleDamage();
        }
        if (lighthit == true)
        {
            duration += Time.deltaTime;

            if (duration >= damageRevive)
            {
                duration = 0f;
                lighthit = false;
            }
        }
        if (nextDamage == true)
        {
            duration += Time.deltaTime;
            if (duration >= nextD)
            {
                duration = 0f;
                nextDamage = false;
            }
        }
        if (Hp <= 0)
        {
            Debug.Log("kieta");
            animator.Play(deadAnime);

            PlayerController.gameState = "gameover";
            gameState = "gameover";
            Deth = true;
            GameStop();
            //======================
            //�Q�[�����o
            //=======================
            //�v���C���[�����������
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            Deth = true;
            // �I�u�W�F�N�g��j�󂷂�
            //Destroy(transform.root.gameObject);
        }
        
    }

    private void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Enemy�^�O��ݒ肵�Ă���I�u�W�F�N�g�ɐڐG�����Ƃ�
        if (collision.gameObject.tag == "Enemy" && !nextDamage)
        {
            //HP����1������
            Hp = Hp - 10;

            nextDamage = true;

            //HP��Slider�ɔ��f�B
            slider.value = (float)Hp;
        }

        if(collision.gameObject.tag == "Boss")
        {
            //HP����1������
            Hp = Hp - 5;

            //HP��Slider�ɔ��f�B
            slider.value = (float)Hp;
        }
        if (collision.gameObject.tag == "needle")
        {
            AudioSource soundPlayer = GetComponent<AudioSource>();

            soundPlayer.PlayOneShot(Nsound);

            Hp -= 10;
            slider.value = (float)Hp;
            onNeedle = true;
        }

        if (Hp <= 30)
        {
            //+++ �T�E���h�Đ��ǉ� +++
            //�T�E���h�Đ�
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM��~
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(meHP);
            }
            Debug.Log("�Ȃ�܂���");
        }

        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "needle")
        {
            onNeedle = false;
            Ntime = 0.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //���_���[�W
        if (other.gameObject.tag == "lightning" && !lighthit)
        {
            Debug.Log("ataltutq");
            //HP����1������
            Hp = Hp - 20;
            lighthit = true;

            //HP��Slider�ɔ��f�B
            slider.value = (float)Hp;
        }

        //�h���S���̗��_���[�W
        if (other.gameObject.tag == "thunder")
        {
            Debug.Log("ataltutq");
            //HP����1������
            Hp = Hp - 5;

            //HP��Slider�ɔ��f�B
            slider.value = (float)Hp;
        }

        //�{�X�̋Z���󂯂��Ƃ�
        if(other.gameObject.tag == "waza")
        {
            Debug.Log("�Z��H�����");
            //hp������
            Hp = Hp - 20;
            //hp��Slider�ɔ��f
            slider.value = (float)Hp;
        }

        //
        if(other.gameObject.CompareTag("Apple"))
        {
            Debug.Log("����������`!!");
            //hp������
            Hp = Hp - 100;
            //hp��Slider�ɔ��f
            slider.value = (float)Hp;
        }
        //if (Hp <= 0)
        //{
        //    Debug.Log("kieta");
        //    animator.Play(deadAnime);

        //    gameState = "gameover";
        //    player.GameStop();
        //    //======================
        //    //�Q�[�����o
        //    //=======================
        //    //�v���C���[�����������
        //    GetComponent<CapsuleCollider2D>().enabled = false;
        //    GetComponent<BoxCollider2D>().enabled = false;
        //    Deth = true;
        //    // �I�u�W�F�N�g��j�󂷂�
        //    //Destroy(transform.root.gameObject);
           
        //}
    }

    private IEnumerator RecoverHP()
    {
        while (true)    //�������[�v�ŉ񕜂𑱂���
        {
            yield return new WaitForSeconds(recoveryInterval);  //�w�肵�����Ԃ�҂�

            if (Hp < maxHp)
            {
                Hp += recoveryAmout;
                if (Hp > maxHp)
                {
                    Hp = maxHp;
                }
                //HP��Slider�ɔ��f�B
                slider.value = (float)Hp;
                Debug.Log("Hp:" + Hp);
            }
        }
    }
    void GameStop()
    {
        //Rigidbody2D������Ă���
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        //���x��0�ɂ��ċ�����~
        rbody.velocity = new Vector2(0, 0);
    }
    private void NeedleDamage()
    {
        AudioSource soundPlayer = GetComponent<AudioSource>();
        
        float Ndamage = 1.0f;

        Ntime += Time.deltaTime;

        Debug.Log(Ntime);

        if (Ntime >= Ndamage)
        {

            soundPlayer.PlayOneShot(Nsound);
            Hp -= 10;

            slider.value = (float)Hp;
            Ntime = 0;
        }
    }
}
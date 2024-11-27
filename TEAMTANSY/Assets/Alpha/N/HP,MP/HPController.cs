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

    Animator animator; // �A�j���[�V����
    public string deadAnime = "PlayerOver";
    public static string gameState = "playing"; // �Q�[���̏��
    //Slider
    public Slider slider;

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip meHP;    //�e����

    void Start()
    {
        slider.interactable = false;
        //Slider���ő�ɂ���B
        slider.value = 100;
        //HP���ő�HP�Ɠ����l�ɁB
        Hp = maxHp;
        StartCoroutine(RecoverHP());    //�ŏ��̉񕜎��Ԃ�ݒ�

        animator = GetComponent<Animator>();        //�@Animator ������Ă���
        gameState = "playing";  // �Q�[�����ɂ���
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Enemy�^�O��ݒ肵�Ă���I�u�W�F�N�g�ɐڐG�����Ƃ�
        if (collision.gameObject.tag == "Enemy")
        {
            //HP����1������
            Hp = Hp - 10;

            //HP��Slider�ɔ��f�B
            slider.value = (float)Hp;
        }

        

        if (Hp == 30)
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

        if (Hp == 0)
        {
            Debug.Log("kieta");
            animator.Play(deadAnime);

            gameState = "gameover";
            //======================
            //�Q�[�����o
            //=======================
            //�v���C���[�����������
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            // �I�u�W�F�N�g��j�󂷂�
            Destroy(transform.root.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //���_���[�W
        if (other.gameObject.tag == "lightning")
        {
            Debug.Log("ataltutq");
            //HP����1������
            Hp = Hp - 35;

            //HP��Slider�ɔ��f�B
            slider.value = (float)Hp;
        }

        //�h���S���̗��_���[�W
        if (other.gameObject.tag == "thunder")
        {
            Debug.Log("ataltutq");
            //HP����1������
            Hp = Hp - 10;

            //HP��Slider�ɔ��f�B
            slider.value = (float)Hp;
        }

        if (Hp <= 0)
        {
            Debug.Log("kieta");
            animator.Play(deadAnime);

            gameState = "gameover";
            //======================
            //�Q�[�����o
            //=======================
            //�v���C���[�����������
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            // �I�u�W�F�N�g��j�󂷂�
            Destroy(transform.root.gameObject);
        }
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
}


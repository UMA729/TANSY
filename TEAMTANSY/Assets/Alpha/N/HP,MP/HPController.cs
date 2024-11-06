using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI���g���Ƃ��ɏ����܂��B

public class HPController : MonoBehaviour
{
    //�ő�HP�ƌ��݂�HP�B
    int maxHp = 100;
    int Hp;
    public int recoveryAmout = 10;  //1��̉񕜗�
    public float recoveryInterval = 10f;    //�񕜂̊Ԋu
    public int consumptionAmount = 10;

    //Slider
    public Slider slider;

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip meHP;    //�e����

    void Start()
    {
        //Slider���ő�ɂ���B
        slider.value = 100;
        //HP���ő�HP�Ɠ����l�ɁB
        Hp = maxHp;
        StartCoroutine(RecoverHP());    //�ŏ��̉񕜎��Ԃ�ݒ�
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

        if(Hp == 30)
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


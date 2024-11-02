using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI���g���Ƃ��ɏ����܂��B

public class HPController : MonoBehaviour
{
    //�ő�HP�ƌ��݂�HP�B
    int maxHp = 100;
    int Hp;
    //Slider
    public Slider slider;

    void Start()
    {
        //Slider���ő�ɂ���B
        slider.value = 100;
        //HP���ő�HP�Ɠ����l�ɁB
        Hp = maxHp;
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

        if (Hp == 0)
        {
            Debug.Log("kieta");
            // �I�u�W�F�N�g��j�󂷂�
            Destroy(transform.root.gameObject);
        }
    }

}

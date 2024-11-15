using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI���g���Ƃ��ɏ����܂��B

public class MPController : MonoBehaviour
{
    //�ő�HP�ƌ��݂�HP�B
    int maxMp= 100;
    public int Mp; 
    public int recoveryAmout = 10;  //1��̉񕜗�
    public float recoveryInterval = 10f;    //�񕜂̊Ԋu
    public int consumptionAmount = 10;

    //Slider
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.interactable = false;
        //Slider���ő�ɂ���B
        slider.value = 100;
        //HP���ő�HP�Ɠ����l�ɁB
        Mp = maxMp;
        StartCoroutine(RecoverMP());    //�ŏ��̉񕜎��Ԃ�ݒ�
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WindBullet")
        {
            if (Mp >= consumptionAmount)
            {
                Mp -= consumptionAmount;
                //HP��Slider�ɔ��f�B
                slider.value = (float)Mp;
                Debug.Log("������");
            }
            else
            {
                //HP��Slider�ɔ��f�B
                slider.value = (float)Mp;
                Debug.Log("Not enough to consume.");
            }
        }
    }

    private IEnumerator RecoverMP()
    {
        while (true)    //�������[�v�ŉ񕜂𑱂���
        {
            yield return new WaitForSeconds(recoveryInterval);  //�w�肵�����Ԃ�҂�

            if(Mp < maxMp)
            {
                Mp += recoveryAmout;
                if(Mp > maxMp)
                {
                    Mp = maxMp;
                }
                //HP��Slider�ɔ��f�B
                slider.value = (float)Mp;
                Debug.Log("Mp:" + Mp);
            }
        }
    }
}

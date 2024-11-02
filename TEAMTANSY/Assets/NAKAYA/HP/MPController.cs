using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI���g���Ƃ��ɏ����܂��B

public class MPController : MonoBehaviour
{
    //�ő�HP�ƌ��݂�HP�B
    int maxMp= 100;
    int Mp;
    //Slider
    public Slider slider;

    public int recoveryAmount = 10; // ���̉񕜗�
    public float fireRate = 1f;
    private float nextFireTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        //Slider���ő�ɂ���B
        slider.value = 100;
        //HP���ő�HP�Ɠ����l�ɁB
        Mp = maxMp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        while(Mp > 0)
        {
            if (collision.gameObject.tag == "WindBullet")
            {
                //Mp��10����
                Mp = Mp - 100;

                //HP��Slider�ɔ��f�B
                slider.value = (float)Mp;
            }
            if (Time.time >= nextFireTime)
            {
                Mp += recoveryAmount;

                //MP��Slider�ɔ��f�B
                slider.value = (float)Mp;

            }
        }
  
    }
}

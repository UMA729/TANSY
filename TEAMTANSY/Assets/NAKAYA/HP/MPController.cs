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
       
  
    }
}

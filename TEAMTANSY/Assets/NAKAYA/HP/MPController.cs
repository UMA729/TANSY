using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UIを使うときに書きます。

public class MPController : MonoBehaviour
{
    //最大HPと現在のHP。
    int maxMp= 100;
    int Mp;
    //Slider
    public Slider slider;

    public int recoveryAmount = 10; // 一回の回復量
    public float fireRate = 1f;
    private float nextFireTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        //Sliderを最大にする。
        slider.value = 100;
        //HPを最大HPと同じ値に。
        Mp = maxMp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        while(Mp > 0)
        {
            if (collision.gameObject.tag == "WindBullet")
            {
                //Mpが10減る
                Mp = Mp - 100;

                //HPをSliderに反映。
                slider.value = (float)Mp;
            }
            if (Time.time >= nextFireTime)
            {
                Mp += recoveryAmount;

                //MPをSliderに反映。
                slider.value = (float)Mp;

            }
        }
  
    }
}

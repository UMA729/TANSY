using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UIを使うときに書きます。

public class MPController : MonoBehaviour
{
    //最大HPと現在のHP。
    int maxMp= 100;
    public int Mp; 
    public int recoveryAmout = 10;  //1回の回復量
    public float recoveryInterval = 10f;    //回復の間隔
    public int consumptionAmount = 10;

    //Slider
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.interactable = false;
        //Sliderを最大にする。
        slider.value = 100;
        //HPを最大HPと同じ値に。
        Mp = maxMp;
        StartCoroutine(RecoverMP());    //最初の回復時間を設定
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WindBullet")
        {
            if (Mp >= consumptionAmount)
            {
                Mp -= consumptionAmount;
                //HPをSliderに反映。
                slider.value = (float)Mp;
                Debug.Log("減った");
            }
            else
            {
                //HPをSliderに反映。
                slider.value = (float)Mp;
                Debug.Log("Not enough to consume.");
            }
        }
    }

    private IEnumerator RecoverMP()
    {
        while (true)    //無限ループで回復を続ける
        {
            yield return new WaitForSeconds(recoveryInterval);  //指定した時間を待つ

            if(Mp < maxMp)
            {
                Mp += recoveryAmout;
                if(Mp > maxMp)
                {
                    Mp = maxMp;
                }
                //HPをSliderに反映。
                slider.value = (float)Mp;
                Debug.Log("Mp:" + Mp);
            }
        }
    }
}

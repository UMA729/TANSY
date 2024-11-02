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
       
  
    }
}

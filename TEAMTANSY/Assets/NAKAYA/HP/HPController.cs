using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UIを使うときに書きます。

public class HPController : MonoBehaviour
{
    //最大HPと現在のHP。
    int maxHp = 100;
    int Hp;
    //Slider
    public Slider slider;

    void Start()
    {
        //Sliderを最大にする。
        slider.value = 100;
        //HPを最大HPと同じ値に。
        Hp = maxHp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Enemyタグを設定しているオブジェクトに接触したとき
        if (collision.gameObject.tag == "Enemy")
        {
            //HPから1を引く
            Hp = Hp - 10;

            //HPをSliderに反映。
            slider.value = (float)Hp;
        }

        if (Hp == 0)
        {
            Debug.Log("kieta");
            // オブジェクトを破壊する
            Destroy(transform.root.gameObject);
        }
    }

}


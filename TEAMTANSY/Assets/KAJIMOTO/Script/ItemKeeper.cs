using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKeeper : MonoBehaviour
{
    public static int hasDoorKey = 0;    //ドアの鍵
    public static int hasMagicBook = 0;  //魔法所の数
    public static int hasHealBullet = 0; //回復弾の数

    // Start is called before the first frame update
    void Start()
    {
        //アイテム読み込み
        hasDoorKey = PlayerPrefs.GetInt("DoorKey");
        hasMagicBook = PlayerPrefs.GetInt("MagicBook");
        hasHealBullet = PlayerPrefs.GetInt("HealBullet");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

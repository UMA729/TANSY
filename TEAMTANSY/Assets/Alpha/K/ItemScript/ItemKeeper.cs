using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKeeper : MonoBehaviour
{
    public static int hasDoorKey = 0;    //�h�A�̌�
    public static int hasMagicBook = 0;  //���@���̐�
    public static int hasHealBullet = 0; //�񕜒e�̐�

    // Start is called before the first frame update
    void Start()
    {
        //�A�C�e���ǂݍ���
        hasDoorKey = PlayerPrefs.GetInt("DoorKey");
        hasMagicBook = PlayerPrefs.GetInt("MagicBook");
        hasHealBullet = PlayerPrefs.GetInt("HealBullet");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

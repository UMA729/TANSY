using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savetrigger : MonoBehaviour
{
    //触れたらプレイヤーの座標位置をセーブする
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            sevePoint.SavePlayerPosition(other.transform);
            Debug.Log("game Saved");
        }
    }

}

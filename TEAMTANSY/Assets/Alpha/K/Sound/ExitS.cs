using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitS : MonoBehaviour
{
    public AudioClip Esound;    //出口が開く音
    slidedoor SD;               //出口のスクリプト

    // Start is called before the first frame update
    void Start()
    {
        SD = FindObjectOfType<slidedoor>();//インスタンス化
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !SD.openTF)
        {
            AudioSource.PlayClipAtPoint(Esound, transform.position); //音が鳴る
        }
    }
}

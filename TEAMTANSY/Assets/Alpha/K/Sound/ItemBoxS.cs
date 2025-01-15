using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxS : MonoBehaviour
{
    public AudioClip IBsound; //宝箱効果音
    ItemBox IB;               //宝箱スクリプト

    // Start is called before the first frame update
    void Start()
    {
        IB = FindObjectOfType<ItemBox>(); //インスタンス化
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && IB.isClosed)
        {
            AudioSource.PlayClipAtPoint(IBsound, transform.position); //音が鳴る
        }
    }
}

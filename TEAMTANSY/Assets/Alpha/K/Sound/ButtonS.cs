using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonS : MonoBehaviour
{

    public AudioClip Bsound;        //ボタン音
    GimmickButton GB;               //ギミックボタンスクリプト
    // Start is called before the first frame update
    void Start()
    {
        GB = FindObjectOfType<GimmickButton>(); //インスタンス化
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GB.gimmickceiling)
        {
            AudioSource.PlayClipAtPoint(Bsound, transform.position); //音が鳴る
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornS : MonoBehaviour
{
    public AudioClip Tsound;        //いばら焼却音
    public AudioClip TNsound;       //焼けないときの音
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FireBullet")
        {
            AudioSource.PlayClipAtPoint(Tsound, transform.position);//その位置で音が鳴る
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornS : MonoBehaviour
{
    public AudioClip Tsound;        //‚¢‚Î‚çÄ‹p‰¹
    public AudioClip TNsound;       //Ä‚¯‚È‚¢‚Æ‚«‚Ì‰¹
    

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
            AudioSource.PlayClipAtPoint(Tsound, transform.position);//‚»‚ÌˆÊ’u‚Å‰¹‚ª–Â‚é
        }
    }
}

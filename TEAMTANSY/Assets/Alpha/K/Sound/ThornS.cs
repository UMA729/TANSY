using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornS : MonoBehaviour
{
    public AudioClip Tsound;        //ƒ{ƒ^ƒ“‰¹
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
            AudioSource playsound = GetComponent<AudioSource>();
            if (playsound != null)
            {
                playsound.Stop();
                playsound.PlayOneShot(Tsound);
            }
        }
    }
}

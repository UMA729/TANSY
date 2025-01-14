using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOXorEXITSound : MonoBehaviour
{
    public AudioClip sound;

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
        if (collision.gameObject.tag == "Player")
        {
            AudioSource soundplayer = GetComponent<AudioSource>();

            if (soundplayer != null)
            {
                soundplayer.Stop();
                soundplayer.PlayOneShot(sound);
            }
        }
    }
}

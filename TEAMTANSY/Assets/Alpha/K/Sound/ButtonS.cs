using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonS : MonoBehaviour
{

    public AudioClip Bsound;        //�{�^����
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
        AudioSource playsound = GetComponent<AudioSource>();
        if (playsound != null)
        {
            playsound.Stop();
            playsound.PlayOneShot(Bsound);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedWalls : MonoBehaviour
{
    [SerializeField] GameObject Prefab;
    //+++ サウンド再生追加 +++
    public AudioClip cage;

    // Start is called before the first frame update
    void Start()
    {
        Prefab.SetActive(false);
        Prefab.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Prefab.SetActive(true);
            Prefab.SetActive(true);
            //+++ サウンド再生追加 +++
            //サウンド再生
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM停止
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(cage);
            }
        }

        if(other.gameObject.CompareTag("Bullet"))
        {
            //+++ サウンド再生追加 +++
            //サウンド再生
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM停止
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(cage);
            }
        }
    }
}

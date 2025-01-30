using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedWalls : MonoBehaviour
{
    [SerializeField] GameObject Prefab;
    //+++ �T�E���h�Đ��ǉ� +++
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
            //+++ �T�E���h�Đ��ǉ� +++
            //�T�E���h�Đ�
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM��~
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(cage);
            }
        }

        if(other.gameObject.CompareTag("Bullet"))
        {
            //+++ �T�E���h�Đ��ǉ� +++
            //�T�E���h�Đ�
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM��~
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(cage);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  <see cref="BulletComtller">Bullet�ւ̃����N</see>
/// </summary>
public class HitDlete : MonoBehaviour
{
    public AudioClip Hit;
    public AudioClip attck;

    //�e���͈͓��ɓ�������������悤�ɂ���
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyZone"))//�^�O"DestoryZone"�����
        {
            Destroy(gameObject); 
        }
        if(collision.gameObject.tag == "Dead")
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Graund"))
        {
            Destroy(gameObject);
            //+++ �T�E���h�Đ��ǉ� +++
            //�T�E���h�Đ�
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM��~
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(attck);
            }

        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("FireBullet"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Door"))
        {
            Destroy(gameObject); 
            //+++ �T�E���h�Đ��ǉ� +++
            //�T�E���h�Đ�
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM��~
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(Hit);
            }
        }
        if (collision.gameObject.CompareTag("thorns"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
    //�e���F��ȃ^�O��gameobject�ɐG�ꂽ�������悤�ɂ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        
    }

    /*public void DestroyZone()
    {
        if (gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("WindBullet"))
        {
            Destroy(gameObject);

        }

        if (gameObject.CompareTag("EarthBullet"))
        {
            Destroy(gameObject);

        }

    }*/
}
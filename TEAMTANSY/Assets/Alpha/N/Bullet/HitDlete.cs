using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  <see cref="BulletComtller">Bulletへのリンク</see>
/// </summary>
public class HitDlete : MonoBehaviour
{
    public AudioClip Hit;
    public AudioClip attck;

    //弾が範囲内に入った時消えるようにする
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyZone"))//タグ"DestoryZone"を取る
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
            //+++ サウンド再生追加 +++
            //サウンド再生
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM停止
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
            //+++ サウンド再生追加 +++
            //サウンド再生
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM停止
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
    //弾が色んなタグのgameobjectに触れたら消えるようにする
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
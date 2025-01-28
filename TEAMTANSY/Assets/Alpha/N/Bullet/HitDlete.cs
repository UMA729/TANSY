using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDlete : MonoBehaviour
{
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

        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Door"))
        {
            Destroy(gameObject);
        }
    }
    //�e���F��ȃ^�O��gameobject�ɐG�ꂽ�������悤�ɂ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("thorns"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
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
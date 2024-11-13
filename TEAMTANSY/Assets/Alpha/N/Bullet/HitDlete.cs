using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDlete : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DestroyZone")
        {
            DestroyZone();
        }
        if(collision.gameObject.tag == "Dead")
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            DestroyZone();
        }

        if (collision.gameObject.tag == "Wall")
        {
            DestroyZone();

        }

        if (collision.gameObject.tag == "Graund")
        {
            DestroyZone();

        }

        if (collision.gameObject.tag == "Player")
        {
            DestroyZone();

        }
    }

    public void DestroyZone()
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

    }
}
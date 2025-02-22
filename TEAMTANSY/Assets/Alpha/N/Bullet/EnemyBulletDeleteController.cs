using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletDeleteController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))//壁に当たったら消えるようにする
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

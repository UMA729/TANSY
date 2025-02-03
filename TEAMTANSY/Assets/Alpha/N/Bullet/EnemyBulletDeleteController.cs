using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletDeleteController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))//ï«Ç…ìñÇΩÇ¡ÇΩÇÁè¡Ç¶ÇÈÇÊÇ§Ç…Ç∑ÇÈ
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

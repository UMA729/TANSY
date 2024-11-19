using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    public float bounceForce = 10f;  // バネの反発力

    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーがバネに触れたか確認
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // バネの反発力でプレイヤーを飛ばす
                playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
            }
        }
    }
}

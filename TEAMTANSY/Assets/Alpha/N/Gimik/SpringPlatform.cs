using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    public float bounceForce = 10f;  // �o�l�̔�����

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �v���C���[���o�l�ɐG�ꂽ���m�F
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // �o�l�̔����͂Ńv���C���[���΂�
                playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
            }
        }
    }
}

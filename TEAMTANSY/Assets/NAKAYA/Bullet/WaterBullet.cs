using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    public float speed = 10f; // 初期スピード
    public float slowDownFactor = 0.5f; // プレイヤーに当たったときのスピード低下率
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed; // 弾丸が発射される方向に移動
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーに当たった場合、スピードを減らす
            rb.velocity = rb.velocity * slowDownFactor;
        }

        // もし弾丸が他のものに当たった場合、弾丸を破壊するなどの処理を追加できます
        // Destroy(gameObject); // 弾丸が衝突したら破壊
    }
}

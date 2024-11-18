using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomBullet : MonoBehaviour
{
    // 弾丸プレハブの配列
    public GameObject[] bulletPrefabs;

    // 弾丸の発射位置
    public Transform firePoint;

    // 弾丸を発射する間隔
    public float fireRate = 0.5f;

    // 次に発射できる時刻
    private float nextFireTime = 0f;

    // Update is called once per frame
    void Update()
    {
        // Fireキーが押され、発射可能時間であれば弾丸を発射
        if (Input.GetMouseButtonDown(1) && Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    // 弾丸を発射する関数
    void FireBullet()
    {
        // ランダムで弾丸を選択
        int randomIndex = Random.Range(0, bulletPrefabs.Length);

        // 選ばれた弾丸を発射
        GameObject bullet = Instantiate(bulletPrefabs[randomIndex], firePoint.position, firePoint.rotation);

        // 弾丸に速度を加える（例えば、右方向に発射）
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = firePoint.right * 10f; // 右方向に10ユニット/秒で発射
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossthunder : MonoBehaviour
{
    public bool isToRight = false;
    public GameObject bulletPrefab;  // 弾のプレハブ
    public Transform firePoint;      // 弾を発射する位置
    public float bulletSpeed = 5f;   // 弾のスピード
    public float deleteTime = 5.0f;
    private Transform playerTransform; // プレイヤーのTransform

    private Rigidbody2D rb;
    private bool isTakingDamage = false;
    private BossCommtller BC;
    private float time = 0;
    private float nextThanderTime = 0f;
    private float Thanderduration = 3f;
    public AudioClip encon;

    void Start()
    {
        BC = FindAnyObjectByType<BossCommtller>();
        rb = this.GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // プレイヤーをタグで検索
        if (isToRight)
        {
            transform.localScale = new Vector2(-2, 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーに向かって弾を発射

        nextThanderTime += Time.deltaTime;

        if (nextThanderTime >= Thanderduration)
        {
            FireBulletAtPlayer();
            nextThanderTime = 0;
        }
    }
    
    public void FireBulletAtPlayer()
    {
        if (playerTransform == null) return;

        // プレイヤーの位置を取得
        Vector2 direction = (playerTransform.position - firePoint.position).normalized;

        // 弾を発射
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // 弾に速度を与える
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;
    }
}

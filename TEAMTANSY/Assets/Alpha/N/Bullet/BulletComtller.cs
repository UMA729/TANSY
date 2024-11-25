using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComtller : MonoBehaviour
{
    public GameObject ballPrefab; // 発射する球のプレハブ
    public float launchForce = 10f; // 球を打つ力
    public Transform shootingPoint; // 弾の発射位置
    public float fireRate = 1f; // 弾丸を発射するクールタイム


    public float nextFireTime = 0f;

    //+++ サウンド再生追加 +++
    public AudioClip meShoot;    //銃放つ

    private PlayerController PC;
    private Vector2 shootDirection;
    private void Start()
    {
        PC = FindAnyObjectByType<PlayerController>();
    }
    void Update()
    {
        if (PC.axisH > 0.0f)
        {
            shootDirection = Vector2.right;
        }
        if (PC.axisH < 0.0f)
        {
            shootDirection = Vector2.left;
        }
    }


    public void LaunchBall()
    {
        // 弾丸を生成して発射位置に配置
        GameObject bullet = Instantiate(ballPrefab, shootingPoint.position, Quaternion.identity);

        // 弾丸に進行方向と速度を設定
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * launchForce;

        // 弾丸がプレイヤーと同じ向きで進むように回転を設定（必要に応じて）
        bullet.transform.localScale = new Vector3(shootDirection.x, 1, 1);  // 左右反転
    }

}
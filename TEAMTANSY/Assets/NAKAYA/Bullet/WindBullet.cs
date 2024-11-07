using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBullet : MonoBehaviour
{
    public GameObject ballPrefab; // 発射する球のプレハブ
    private MPController mp;
    public float launchForce = 10f; // 球を打つ力
    public Transform shootingPoint; // 弾の発射位置
    public float fireRate = 5f;
    public float nextFireTime = 0f;

    //+++ サウンド再生追加 +++
    public AudioClip Bullet;    //銃放つ

    void Start()
    {
        // プレイヤーのレベル管理スクリプトを取得
        mp = GetComponent<MPController>();
    }

    void Update()
    {
       
    }

    public void LaunchBall()
    {
        // プレイヤーが右向きか左向きかを判定
        Vector2 shootDirection = (transform.localScale.x > 0) ? Vector2.right : Vector2.left;

        // 弾丸を生成して発射位置に配置
        GameObject bullet = Instantiate(ballPrefab, shootingPoint.position, Quaternion.identity);

        // 弾丸に進行方向と速度を設定
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * launchForce;

        // 弾丸がプレイヤーと同じ向きで進むように回転を設定（必要に応じて）
        bullet.transform.localScale = new Vector3(shootDirection.x, 1, 1);  // 左右反転
    }


   
}

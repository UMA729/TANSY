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

        // 弾の進行方向に合わせて回転させる
        Vector2 direction = GetComponent<Rigidbody2D>().velocity.normalized;  // 弾の進行方向
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  // 方向を角度に変換
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));  // 角度を適用
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

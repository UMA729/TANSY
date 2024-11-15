using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xplosion : MonoBehaviour
{
    public GameObject ballPrefab; // 発射する球のプレハブ
    private MPController mp;
    public float launchForce = 10f; // 球を打つ力
    public Transform shootingPoint; // 弾の発射位置
    public float fireRate = 5f;
    public float nextFireTime = 0f;
    //爆破を起こすための距離
    public float explosionDistance = 5f;
    //爆破エフェクト
    public GameObject explosionEffect;

    public Transform target;

    //+++ サウンド再生追加 +++
    public AudioClip Bullet;    //銃放つ

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーのレベル管理スクリプトを取得
        mp = GetComponent<MPController>();
    }

    // Update is called once per frame
    void Update()
    {
        // 玉とターゲットの距離を計算
        float distance = Vector2.Distance(transform.position, target.position);

        // 距離が指定した爆発距離以下になったら爆発
        if (distance <= explosionDistance)
        {
            Explode();
        }
        if (Input.GetMouseButtonDown(1))
        {
            LaunchBall();
        }
    }

    // 爆発処理
    void Explode()
    {
        // 爆発エフェクトを生成
        if (explosionEffect != null)
        {
            Debug.Log("あささささ");
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // 玉（オブジェクト）を非アクティブにするか削除
        Destroy(gameObject);
    }
    void LaunchBall()
    {
        // プレイヤーの向きに玉を飛ばす
        Vector2 shootDirection = transform.right;  // プレイヤーの右方向を取得（向きに依存）

        // 玉をインスタンス化
        GameObject bullet = Instantiate(ballPrefab, transform.position, Quaternion.identity);

        // 玉のRigidbody2Dに速度を与えて発射
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * launchForce;
    }
}


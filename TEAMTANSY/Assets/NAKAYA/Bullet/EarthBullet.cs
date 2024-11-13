using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBullet : MonoBehaviour
{
    public GameObject ballPrefab; // 発射する球のプレハブ
    private MPController mp;
    public float launchForce = 10f; // 球を打つ力
    public Transform shootingPoint; // 弾の発射位置
    public float fireRate = 5f;
    public float nextFireTime = 0f;
    public GameObject wallPrefab;  // 生成する壁のプレハブ
    public float wallHeight = 2f;  // 壁の高さ（地面との距離）
    public float wallLifeTime = 3f;  // 壁が存在する時間

    //+++ サウンド再生追加 +++
    public AudioClip Bullet;    //銃放つ

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーのレベル管理スクリプトを取得
        mp = GetComponent<MPController>();
    }

    private void Update()
    {
        // マウスボタンが押されたときに球を打つ
        if (Input.GetMouseButtonDown(1))
        {
            LaunchBall();
            Debug.Log("iiiiiiiii");
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("あささささ");
        if (collision.gameObject.CompareTag("Graund"))
        {

            Debug.Log("ああああああああああ");
        }

    }

}

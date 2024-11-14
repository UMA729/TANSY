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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("あささささ");
        if (collision.gameObject.CompareTag("Graund"))
        {
            GetComponent<Collider2D>();
            // 衝突地点を取得
            Vector3 collisionPoint = collision.contacts[0].point;

            // 壁を衝突地点に生成
            GameObject wall = Instantiate(wallPrefab, collisionPoint, Quaternion.identity);

            // 壁を指定時間後に消す
            Destroy(wall, 3f);  // 3秒後に壁を消去
            Debug.Log("ああああああああああ");
        }
        if (collision.gameObject.tag == "Graund")
        {
            Debug.Log("いやああああああああ");
        }


    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            LaunchBall();
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

    

}

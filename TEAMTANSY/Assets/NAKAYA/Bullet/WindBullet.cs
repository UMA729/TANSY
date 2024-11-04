using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBullet : MonoBehaviour
{
    public GameObject ballPrefab; // 発射する球のプレハブ
    int Mp;
    public float launchForce = 500f; // 球を打つ力
    public float fireRate = 5f;

    private float nextFireTime = 0f;

    //+++ サウンド再生追加 +++
    public AudioClip Bullet;    //銃放つ
    void Update()
    {
        // 右クリックを検出
        if (Input.GetMouseButtonDown(1) && Time.time >= nextFireTime) // 右クリック
        {
            LaunchBall();
            nextFireTime = Time.time + 5f / fireRate; // クールタイムを設定


            //+++ サウンド再生追加 +++
            //サウンド再生
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM停止
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(Bullet);
            }
        }
    }

    void LaunchBall()
    {
        // マウスの位置を取得
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z軸を0に設定（2D空間なので）

        // 球を生成
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);

        // 球のRigidbodyを取得
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        // 発射方向を計算
        Vector2 launchDirection = (mousePosition - transform.position).normalized;

        // 球に力を加える
        rb.AddForce(launchDirection * launchForce);
    }

   
}

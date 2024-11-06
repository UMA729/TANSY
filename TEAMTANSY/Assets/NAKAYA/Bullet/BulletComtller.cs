using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComtller : MonoBehaviour
{
    public GameObject ballPrefab; // 発射する球のプレハブ
    public float launchForce = 500f; // 球を打つ力
    public float fireRate = 1f; // 弾丸を発射するクールタイム

    private float nextFireTime = 0f;

    //+++ サウンド再生追加 +++
    public AudioClip meShoot;    //銃放つ

    void Update()
    {
        // マウスボタンが押されたときに球を打つ
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            LaunchBall();
            nextFireTime = Time.time + 1f / fireRate; // クールタイムを設定


            //+++ サウンド再生追加 +++
            //サウンド再生
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM停止
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(meShoot);
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

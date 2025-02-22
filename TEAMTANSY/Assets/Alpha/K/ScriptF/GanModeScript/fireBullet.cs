using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBullet : MonoBehaviour
{
    public GameObject ballPrefab; // 発射する球のプレハブ
    private MPController mp;
    public float launchForce = 10f; // 球を打つ力
    public Transform shootingPoint; // 弾の発射位置
    public float fireRate = 5f;
    public float nextFireTime = 0f;
    public AudioClip meShoot;    //銃放つ
    public bool fireBaff = false;

    private PlayerController PC;
    
    Vector2 shootDirection;
    void Start()
    {        
        PC = FindObjectOfType<PlayerController>();
        shootDirection = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーが右向きか左向きかを判定し弾の方法を設定
        if (PC.axisH > 0.0f)
        {
            shootDirection = Vector2.right;
        }
        else if (PC.axisH < 0.0f)
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

        if (shootDirection == Vector2.right)
            bullet.transform.localScale = new Vector3(3f, 3f, 1f); // 必要なスケールに変更
        if (shootDirection == Vector2.left)
            bullet.transform.localScale = new Vector3(-3f, 3f, 1f);
    }

    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyBullet : MonoBehaviour
{
    //public
    public GameObject ballPrefab; // 発射する球のプレハブ
    public float launchForce = 10f; // 球を打つ力
    public Transform shootingPoint; // 弾の発射位置
    public float fireRate = 1f; // 弾丸を発射するクールタイム
    public float deleteTime = 2.0f;//消す時間
    public Transform player;  // プレイヤーのTransformをアサイン

    //private
    private Vector2 shootDirection;
    private SpriteRenderer spriteRenderer;//ボスのするスプライトレンダラー
    private BossCommtller BC;

    //+++ サウンド再生追加 +++
    public AudioClip BSS;    //銃放つ
    // Start is called before the first frame update

    //スタート関数
    //説明
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        BC = FindAnyObjectByType<BossCommtller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    public void LaunchBall()
    {
        // 魔法を生成して発射位置に配置
        GameObject bullet = Instantiate(ballPrefab, shootingPoint.position, Quaternion.identity)as GameObject;
        //+++ サウンド再生追加 +++
        //サウンド再生
        AudioSource soundPlayer = GetComponent<AudioSource>();
        if (soundPlayer != null)
        {
            //BGM停止
            soundPlayer.Stop();
            soundPlayer.PlayOneShot(BSS);
        }
        Destroy(bullet, deleteTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyBullet : MonoBehaviour
{
    public GameObject ballPrefab; // 発射する球のプレハブ
    public float launchForce = 10f; // 球を打つ力
    public Transform shootingPoint; // 弾の発射位置
    public float fireRate = 1f; // 弾丸を発射するクールタイム
    public Transform player;  // プレイヤーのTransformをアサイン
    private Vector2 shootDirection;
    private SpriteRenderer spriteRenderer;//ボスのするスプライトレンダラー
    public float deleteTime = 2.0f;//消す時間
    private BossCommtller BC;
    //+++ サウンド再生追加 +++
    public AudioClip BSS;    //銃放つ
    // Start is called before the first frame update
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
            Debug.Log("バーかだよ");
        }
        else
        {
            spriteRenderer.flipX = false;
            Debug.Log("それは残像だ");
        }
    }
    public void LaunchBall()
    {
        Debug.Log("Wryyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
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
        Debug.Log("wwwwwww");
        Destroy(bullet, deleteTime);
    }
}

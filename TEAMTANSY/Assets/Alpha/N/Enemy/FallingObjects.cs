using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    public GameObject ballPrefab; // 発射する球のプレハブ
    public Transform shootingPoint; // 弾の発射位置
    private Vector2 shootDirection;
    private SpriteRenderer spriteRenderer;//ボスのするスプライトレンダラー
    public float deleteTime = 2.0f;//消す時間
    private BossCommtller BC;
    private float nextThanderTime = 0f;
    private float Thanderduration = 3f;
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
        // プレイヤーに向かって弾を発射

        nextThanderTime += Time.deltaTime;

        if (nextThanderTime >= Thanderduration)
        {
            FireBulletAtPlayer();
            nextThanderTime = 0;
        }
    }
    public void FireBulletAtPlayer()
    {
        Debug.Log("乗りなさい！シンジくん");
        //if (playerTransform == null) return;
        // 魔法を生成して発射位置に配置
        GameObject bullet = Instantiate(ballPrefab, shootingPoint.position, Quaternion.identity) as GameObject;
        //+++ サウンド再生追加 +++
        //サウンド再生
        AudioSource soundPlayer = GetComponent<AudioSource>();
        if (soundPlayer != null)
        {
            //BGM停止
            soundPlayer.Stop();
            soundPlayer.PlayOneShot(BSS);
        }
        Debug.Log("いやだってばああああああああああああああ");
        Destroy(bullet, deleteTime);
    }
}

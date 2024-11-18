using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBrock : MonoBehaviour
{
    // 足場が崩れるまでの待機時間
    public float crumbleDelay = 0.5f;

    // 足場が崩れた後のエフェクトやサウンドなど
    public GameObject crumbleEffect;

    private Collider2D platformCollider;  // 足場のコライダー

    void Start()
    {
        // 足場のコライダーを取得
        platformCollider = GetComponent<Collider2D>();
    }

    // プレイヤーが足場に触れた時の処理
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトがプレイヤーかどうかをチェック
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("触れた");
            // 足場が崩れるまでの遅延時間を開始
            StartCoroutine(CrumblePlatform());
        }
    }

    // 足場が崩れるコルーチン
    private IEnumerator CrumblePlatform()
    {
        // 足場が崩れる前に待機
        yield return new WaitForSeconds(crumbleDelay);

        // 足場が崩れる処理
        // 例えば、足場のコライダーを無効化して、足場が消えるようにする
        if (platformCollider != null)
        {
            platformCollider.enabled = false;  // コライダーを無効化
        }

        // 足場のエフェクトが設定されていれば表示する
        if (crumbleEffect != null)
        {
            Instantiate(crumbleEffect, transform.position, Quaternion.identity);
        }

        // 足場を削除（消す）または非表示にする
        Destroy(gameObject, 0.5f);  // 0.5秒後にオブジェクトを削除
    }
}

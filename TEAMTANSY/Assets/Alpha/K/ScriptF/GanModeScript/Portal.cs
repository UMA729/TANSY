using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal; // 接続先のポータル
    private bool isTeleporting; // テレポート中かどうかのフラグ

    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーが接触かつ接続先ポータルが設定されている場合
        if (other.CompareTag("Player") && linkedPortal != null && !isTeleporting)
        {
            StartCoroutine(Teleport(other)); // テレポート処理を開始
        }
    }

    private IEnumerator Teleport(Collider2D player)
    {
        isTeleporting = true;                 // テレポート中フラグを立てる
        linkedPortal.isTeleporting = true;    // リンク先のポータルも一時的に無効にする

        // 接続先ポータルの位置にプレイヤーを移動
        player.transform.position = linkedPortal.transform.position;

        // クールダウン時間を設けて連続テレポートを防ぐ
        yield return new WaitForSeconds(0.5f);

        isTeleporting = false;                 // 現在のポータルのテレポートフラグを解除
        linkedPortal.isTeleporting = false;    // リンク先のポータルのテレポートフラグも解除
    }
}

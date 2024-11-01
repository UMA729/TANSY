using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickTrigger : MonoBehaviour
{
    public float riseSpeed = 4.0f;  //上昇速さ
    public float riseDistance = 5f; // 上昇距離

    private bool isRising = false;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; // 初期位置を記録
    }

    // Update is called once per frame
    void Update()
    {
        if (isRising)
        {
            // 上昇処理
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;

            // 一定距離上昇したら停止
            if (transform.position.y >= startPosition.y + riseDistance)
            {
                isRising = false;
                // 元の位置に戻す場合は以下を有効にする
                transform.position = startPosition;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Bullet")) // プレイヤーのタグを確認
        {
            ActivateGimmick();
        }
    }

    private void ActivateGimmick()
    {
        Debug.Log("ギミックが発動しました！");
        isRising = true; // 上昇を開始
    }

}

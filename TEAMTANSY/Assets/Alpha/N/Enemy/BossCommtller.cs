using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCommtller : MonoBehaviour
{
    public Transform player;  // プレイヤーのTransformをアサイン

    public float speed = 0.0f;
    public float jump = 0.0f;
    public GameObject bullet;

    public float hp = 1000;
    public float Lenght = 5f;//プレイヤーが近づく距離の範囲
    private float nexttime = 3f;//次のアクション
    public bool isDelete = false;
    private bool playerRange = false;//プレイヤーが範囲内に入っているのか
    public float actionInterval = 1f;  // ランダムな数を取得してスイッチ文を動かす間隔（秒）
 
    void Update()
    {
        // プレイヤーが近づいているか確認
        if (Vector2.Distance(transform.position, player.position) < Lenght)
        {
            if (!playerRange)
            {
                playerRange = true;
                Debug.Log("範囲内に入りました!");
                
            }

            if(Time.time >= nexttime)
            {
                nexttime = Time.time + actionInterval;
                TriggerRandomEvent();  // プレイヤーが範囲内に入ったらランダムな処理を実行
            }
        }
        else
        {
            if(playerRange)
            {
                playerRange = false;
                Debug.Log("範囲内を離れました");
            }
           
        }
    }

    void TriggerRandomEvent()
    {
        
            int rad = Random.Range(1, 11);//1~10の範囲内の数が取得される
            Debug.Log("rad:" + rad);
        switch (rad)
        {
            case 1:
                Debug.Log("ジャンプ");
                break;
            case 2:
                Debug.Log("突進");
                break;
            case 3:
                Debug.Log("だああああンがん");
                break;
            case 4:
                Debug.Log("果然");
                break;
            case 5:
                Debug.Log("ヨシ");
                break;
            default:
                Debug.Log("デフォの処理");
                break;

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            hp -= 10;
            if (hp <= 0)
            {
                Die();
            }
        }
    }


    private void Die()
    {
        Destroy(gameObject);
    }
}

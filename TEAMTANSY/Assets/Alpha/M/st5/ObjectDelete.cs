using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDelete : MonoBehaviour
{
    public GameObject obj;
    public float deleteTime = 0.5f;

    // Startはオブジェクトが生成されたときに呼ばれる
    void Start()
    {
        // 指定した時間後に自動でオブジェクトを削除
        Destroy(gameObject, deleteTime);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        // "Graund"タグのオブジェクトと衝突したら削除する
        if (collision2D.gameObject.CompareTag("Graund"))
        {
            Destroy(gameObject, deleteTime);
        }
    }
}

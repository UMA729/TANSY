using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomGenerator : MonoBehaviour
{
    public GameObject obj;                 // 生成するオブジェクト
    [SerializeField] Transform pos;        // 生成位置
    [SerializeField] Transform pos2;       // 生成位置
    float minX, maxX, minY, maxY;         // 生成範囲

    float frame = 0f;
    public float elepsed = 3f;            // オブジェクト生成の間隔

    void Start()
    {
        // 初期化: 生成範囲を決める
        minX = Mathf.Min(pos.position.x, pos2.position.x);
        maxX = Mathf.Max(pos.position.x, pos2.position.x);
        minY = Mathf.Min(pos.position.y, pos2.position.y);
        maxY = Mathf.Max(pos.position.y, pos2.position.y);
    }

    void Update()
    {
        frame += Time.deltaTime;

        if (frame > elepsed)
        {
            frame = 0f;
            // ランダムで生成位置を決定
            float posX = Random.Range(minX, maxX);
            float posY = Random.Range(minY, maxY);

            // オブジェクトを生成
            GameObject newObj = Instantiate(obj, new Vector3(posX, posY, 0), Quaternion.identity);

            // ObjectDelete コンポーネントを追加して削除時間を設定
            ObjectDelete objectDelete = newObj.GetComponent<ObjectDelete>();
            if (objectDelete != null)
            {
                objectDelete.deleteTime = 0.5f; // 例えば0.5秒で消える
            }
        }
    }
}

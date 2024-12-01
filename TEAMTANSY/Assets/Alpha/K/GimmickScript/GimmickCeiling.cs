using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickCeiling : MonoBehaviour
{
    // 動かす範囲（上下）
    public float moveDistance = 5f; // 足場が動く最大の距離
    public float moveSpeed = 2f;    // 足場が移動する速度

    private Vector3 initialPosition; // 足場の初期位置
    GimmickButton GB;

    void Start()
    {
        initialPosition = transform.position; // 足場の初期位置を保存
        GB = FindObjectOfType<GimmickButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GB.gimmickceiling)
        {
            MoveCeiling();
        }
    }

    void MoveCeiling()
    {
        // 足場の現在の位置を取得
        Vector3 targetPosition = initialPosition;

        targetPosition.y -= moveDistance; // 下に移動

        // 足場を指定した速度で移動
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 目標位置に到達したら方向転換
        if (transform.position == targetPosition)
        {
            GB.gimmickceiling = false;
        }
    }
}

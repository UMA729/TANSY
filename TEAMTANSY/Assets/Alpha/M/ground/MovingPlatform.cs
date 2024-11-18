using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // 動かす範囲（上下）
    public float moveDistance = 5f; // 足場が動く最大の距離
    public float moveSpeed = 2f;    // 足場が移動する速度
    public bool moveUpwards = true; // 上下方向の初期設定

    private Vector3 initialPosition; // 足場の初期位置
    private bool movingUp = true;    // 足場が上に動いているかどうか

    void Start()
    {
        initialPosition = transform.position; // 足場の初期位置を保存
    }

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        // 足場の現在の位置を取得
        Vector3 targetPosition = initialPosition;

        // 上下方向に動かす
        if (movingUp)
        {
            targetPosition.y += moveDistance; // 上に移動
        }
        else
        {
            targetPosition.y -= moveDistance; // 下に移動
        }

        // 足場を指定した速度で移動
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 目標位置に到達したら方向転換
        if (transform.position == targetPosition)
        {
            movingUp = !movingUp;
        }
    }
}

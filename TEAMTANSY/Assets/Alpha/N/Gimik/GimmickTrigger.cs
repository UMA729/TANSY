using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickTrigger : MonoBehaviour
{
    public GameObject Prefab; // 動く壁や床のプレハブ
    // 床の元の位置と目標の位置
    public Vector3 originalPosition;
    public Vector3 targetPosition;

    // 上昇にかかる時間（秒）
    public float riseTime = 2f;

    // 元に戻る時間（秒）
    public float returnTime = 2f;

    // 現在の状態（上昇中か戻る中か）
    private enum FloorState { Idle, Rising, Returning }
    private FloorState currentState = FloorState.Idle;

    // 時間計測用の変数
    private float timer = 0f;

    void Start()
    {
        // 元の位置とターゲットの位置を設定
        originalPosition = transform.position;
        targetPosition = originalPosition + new Vector3(0, 5f, 0);  // 上に5ユニット移動させる例
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Debug.Log("お前は最後に");
            switch (currentState)
            {
                case FloorState.Idle:
                    // 初期状態、何もしない
                    break;

                case FloorState.Rising:
                    // 床が上昇中
                    Debug.Log("殺すと約束したんな");
                    timer += Time.deltaTime;
                    float riseProgress = Mathf.Clamp01(timer / riseTime);
                    transform.position = Vector3.Lerp(originalPosition, targetPosition, riseProgress);

                    if (riseProgress >= 1f)
                    {
                        // 上昇が完了したら戻る状態に
                        currentState = FloorState.Returning;
                        timer = 0f;
                    }
                    break;

                case FloorState.Returning:
                    // 床が元の位置に戻る
                    timer += Time.deltaTime;
                    float returnProgress = Mathf.Clamp01(timer / returnTime);
                    transform.position = Vector3.Lerp(targetPosition, originalPosition, returnProgress);

                    if (returnProgress >= 1f)
                    {
                        // 戻り終わったら元の状態に
                        currentState = FloorState.Idle;
                        timer = 0f;
                    }
                    break;
            }

        }
    }

    // 床を上昇させる
    public void TriggerFloorRise()
    {
        Debug.Log("あれは嘘だ");
        if (currentState == FloorState.Idle)
        {
            currentState = FloorState.Rising;
            timer = 0f; // タイマーリセット
        }
    }
}


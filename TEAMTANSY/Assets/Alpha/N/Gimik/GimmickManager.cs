using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickManager : MonoBehaviour
{
    public GameObject gimmickPrefab;  // ギミックのプレハブ
    public Vector2 respawnPosition = new Vector2(0f, 0f);

    private GameObject currentGimmick;

    void Start()
    {
        SpawnGimmick();
    }

    void Update()
    {
        if (IsObjectOutOfBounds())
        {
            RespawnGimmick();
        }
    }

    bool IsObjectOutOfBounds()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(currentGimmick.transform.position);
        return screenPos.x < 0f || screenPos.x > 1f || screenPos.y < 0f || screenPos.y > 1f;
    }

    void RespawnGimmick()
    {
        Destroy(currentGimmick);  // 現在のギミックを削除
        SpawnGimmick();  // 新しいギミックを復活
    }

    void SpawnGimmick()
    {
        currentGimmick = Instantiate(gimmickPrefab, respawnPosition, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeKajimoto: MonoBehaviour
{
    public Transform startPoint; // ロープの開始地点（キャラクターの手など）
    public Transform endPoint;   // ロープの終着地点（天井）

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineRenderer.SetPosition(0, startPoint.position);
        lineRenderer.SetPosition(1, endPoint.position);
    }
}

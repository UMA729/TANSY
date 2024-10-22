using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeKajimoto: MonoBehaviour
{
    public Transform startPoint; // ���[�v�̊J�n�n�_�i�L�����N�^�[�̎�Ȃǁj
    public Transform endPoint;   // ���[�v�̏I���n�_�i�V��j

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

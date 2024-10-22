using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeEnd : MonoBehaviour
{
    private bool attached = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ceiling") && !attached)
        {
            attached = true;
            rb.isKinematic = true; // ���[�v��V��ɌŒ�
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // �K�v�ɉ����āA���̋����i�X�C���O�Ȃǁj��ǉ�����
        }
    }
}
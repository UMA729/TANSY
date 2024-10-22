using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ropethrow : MonoBehaviour
{
    public GameObject ropePrefab;
    public Transform handPoint;  // �L�����N�^�[�̎�̈ʒu
    public float throwForce = 10f; // �������

    private GameObject currentRope;
    private bool ropeAttached = false;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !ropeAttached)
        {
            ThrowRope();
        }
    }

    void ThrowRope()
    {
        currentRope = Instantiate(ropePrefab, handPoint.position, Quaternion.identity);
        Rigidbody ropeRigidbody = currentRope.GetComponent<Rigidbody>();

        Vector3 throwDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - handPoint.position;
        throwDirection.z = 0;  // 2D�Q�[���̏ꍇ��Z�����Œ�
        throwDirection.Normalize();

        ropeRigidbody.AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }
}
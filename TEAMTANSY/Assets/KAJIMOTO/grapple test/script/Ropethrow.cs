using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ropethrow : MonoBehaviour
{
    public GameObject ropePrefab;
    public Transform handPoint;  // キャラクターの手の位置
    public float throwForce = 10f; // 投げる力

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
        throwDirection.z = 0;  // 2Dゲームの場合はZ軸を固定
        throwDirection.Normalize();

        ropeRigidbody.AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }
}
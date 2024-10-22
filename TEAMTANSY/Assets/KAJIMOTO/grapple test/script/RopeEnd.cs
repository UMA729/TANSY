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
            rb.isKinematic = true; // ロープを天井に固定
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // 必要に応じて、他の挙動（スイングなど）を追加する
        }
    }
}
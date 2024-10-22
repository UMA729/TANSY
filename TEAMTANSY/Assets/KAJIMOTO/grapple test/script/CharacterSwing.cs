using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwing : MonoBehaviour
{
    public GameObject ropeEnd; // ロープの終点

    private HingeJoint hingeJoint;

    void AttachToRope()
    {
        if (ropeEnd.GetComponent<RopeEnd>().attached)
        {
            hingeJoint = gameObject.AddComponent<HingeJoint>();
            hingeJoint.connectedBody = ropeEnd.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2")) // スイング開始のボタン
        {
            AttachToRope();
        }
    }
}
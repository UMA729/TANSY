using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwing : MonoBehaviour
{
    public GameObject ropeEnd; // ���[�v�̏I�_

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
        if (Input.GetButtonDown("Fire2")) // �X�C���O�J�n�̃{�^��
        {
            AttachToRope();
        }
    }
}
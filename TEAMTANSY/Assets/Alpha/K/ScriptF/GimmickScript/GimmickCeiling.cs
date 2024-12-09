using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickCeiling : MonoBehaviour
{
    // �������͈́i�㉺�j
    public float moveDistance = 5f; // ���ꂪ�����ő�̋���
    public float moveSpeed = 2f;    // ���ꂪ�ړ����鑬�x

    private Vector3 initialPosition; // ����̏����ʒu
    GimmickButton GB;

    void Start()
    {
        initialPosition = transform.position; // ����̏����ʒu��ۑ�
        GB = FindObjectOfType<GimmickButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GB.gimmickceiling)
        {
            MoveCeiling();
        }
    }

    void MoveCeiling()
    {
        // ����̌��݂̈ʒu���擾
        Vector3 targetPosition = initialPosition;

        targetPosition.y -= moveDistance; // ���Ɉړ�

        // ������w�肵�����x�ňړ�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // �ڕW�ʒu�ɓ��B����������]��
        if (transform.position == targetPosition)
        {
            GB.gimmickceiling = false;
        }
    }
}

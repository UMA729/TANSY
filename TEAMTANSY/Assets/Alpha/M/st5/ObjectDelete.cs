using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDelete : MonoBehaviour
{
    public GameObject obj;
    public float deleteTime = 0.5f;

    // Start�̓I�u�W�F�N�g���������ꂽ�Ƃ��ɌĂ΂��
    void Start()
    {
        // �w�肵�����Ԍ�Ɏ����ŃI�u�W�F�N�g���폜
        Destroy(gameObject, deleteTime);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        // "Graund"�^�O�̃I�u�W�F�N�g�ƏՓ˂�����폜����
        if (collision2D.gameObject.CompareTag("Graund"))
        {
            Destroy(gameObject, deleteTime);
        }
    }
}

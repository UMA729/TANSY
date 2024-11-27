using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDelete : MonoBehaviour
{
    public GameObject obj;
    public float deleteTime = 0.5f;
    HPController HPC;

    // Start�̓I�u�W�F�N�g���������ꂽ�Ƃ��ɌĂ΂��
    void Start()
    {
        HPC = FindObjectOfType<HPController>();
        // �w�肵�����Ԍ�Ɏ����ŃI�u�W�F�N�g���폜
        Destroy(gameObject, deleteTime);
        if (HPC.lighthit == true)
        {
            if (Time.deltaTime >= deleteTime)
            {
                HPC.lighthit = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        // "Graund"�^�O�̃I�u�W�F�N�g�ƏՓ˂�����폜����
        if (collision2D.gameObject.CompareTag("Graund"))
        {
            //HPC.lighthit = false;
            Destroy(gameObject, deleteTime);
        }
    }
}

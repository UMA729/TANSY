using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDelete : MonoBehaviour
{
    public GameObject obj;
    public int deleteTime;
    HPController HPC;

    int time=0;

    // Start�̓I�u�W�F�N�g���������ꂽ�Ƃ��ɌĂ΂��
    void Start()
    {
        HPC = FindObjectOfType<HPController>();
        // �w�肵�����Ԍ�Ɏ����ŃI�u�W�F�N�g���폜
        Destroy(gameObject, deleteTime);
        
    }

    //private void Update()
    //{
    //    if(time >= deleteTime)
    //    {
    //        Destroy(gameObject);
    //    }
    //    time++;
    //}

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        // "Graund"�^�O�̃I�u�W�F�N�g�ƏՓ˂�����폜����
        if (collision2D.gameObject.CompareTag("Graund"))
        {
            //HPC.lighthit = false;
            //Destroy(gameObject, deleteTime);
        }
    }
}

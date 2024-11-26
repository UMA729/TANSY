using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCommtller : MonoBehaviour
{
    public Transform player;  // �v���C���[��Transform���A�T�C��

    public float speed = 0.0f;
    public float jump = 0.0f;
    public GameObject bullet;

    public float hp = 1000;
    public float Lenght = 5f;//�v���C���[���߂Â������͈̔�
    private float nexttime = 3f;//���̃A�N�V����
    public bool isDelete = false;
    private bool playerRange = false;//�v���C���[���͈͓��ɓ����Ă���̂�
    public float actionInterval = 1f;  // �����_���Ȑ����擾���ăX�C�b�`���𓮂����Ԋu�i�b�j
 
    void Update()
    {
        // �v���C���[���߂Â��Ă��邩�m�F
        if (Vector2.Distance(transform.position, player.position) < Lenght)
        {
            if (!playerRange)
            {
                playerRange = true;
                Debug.Log("�͈͓��ɓ���܂���!");
                
            }

            if(Time.time >= nexttime)
            {
                nexttime = Time.time + actionInterval;
                TriggerRandomEvent();  // �v���C���[���͈͓��ɓ������烉���_���ȏ��������s
            }
        }
        else
        {
            if(playerRange)
            {
                playerRange = false;
                Debug.Log("�͈͓��𗣂�܂���");
            }
           
        }
    }

    void TriggerRandomEvent()
    {
        
            int rad = Random.Range(1, 11);//1~10�͈͓̔��̐����擾�����
            Debug.Log("rad:" + rad);
        switch (rad)
        {
            case 1:
                Debug.Log("�W�����v");
                break;
            case 2:
                Debug.Log("�ːi");
                break;
            case 3:
                Debug.Log("����������������");
                break;
            case 4:
                Debug.Log("�ʑR");
                break;
            case 5:
                Debug.Log("���V");
                break;
            default:
                Debug.Log("�f�t�H�̏���");
                break;

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            hp -= 10;
            if (hp <= 0)
            {
                Die();
            }
        }
    }


    private void Die()
    {
        Destroy(gameObject);
    }
}

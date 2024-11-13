using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    public float speed = 10f; // �����X�s�[�h
    public float slowDownFactor = 0.5f; // �v���C���[�ɓ��������Ƃ��̃X�s�[�h�ቺ��
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed; // �e�ۂ����˂��������Ɉړ�
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �v���C���[�ɓ��������ꍇ�A�X�s�[�h�����炷
            rb.velocity = rb.velocity * slowDownFactor;
        }

        // �����e�ۂ����̂��̂ɓ��������ꍇ�A�e�ۂ�j�󂷂�Ȃǂ̏�����ǉ��ł��܂�
        // Destroy(gameObject); // �e�ۂ��Փ˂�����j��
    }
}

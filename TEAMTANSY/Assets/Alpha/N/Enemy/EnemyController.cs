using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //hp�|�C���g
    public float hp = 10;
    public float speed = 2.0f;//�G�̈ړ����x
    public float revTime = 0f;//���]����
    public bool isToRight = false;//true=�E�����@false=������
    public LayerMask groundLayer;//�n�ʃ��C���[

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip encon;    //�G�����ꂽ�Ƃ�


    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (isToRight)
        {
            transform.localScale = new Vector2(-2, 2);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(revTime > 0)
        {
            time += Time.deltaTime;
            if(time >= revTime)
            {
                isToRight = !isToRight;
                time = 0;
                if(isToRight)
                {
                    transform.localScale = new Vector2(-0.5f, 0.5f);
                }
                else
                {
                    transform.localScale = new Vector2(0.5f, 0.5f);
                }
            }
        }
    }

    void FixedUpdate()
    {
        //�n�㔻��
        bool onGuound = Physics2D.CircleCast(transform.position,
                                            0.5f,
                                            Vector2.down,
                                            0.5f,
                                            groundLayer);
        if(onGuound)
        {
            //���x�X�V
            Rigidbody2D rbody = GetComponent<Rigidbody2D>();
            if(isToRight)
            {
                rbody.velocity = new Vector2(speed, rbody.velocity.y);
            }
            else
            {
                rbody.velocity = new Vector2(-speed, rbody.velocity.y);
            }
        }
    }

    //�ڐG
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isToRight = !isToRight;
        time = 0;
        if (isToRight)
        {
            transform.localScale = new Vector2(-0.5f, 0.5f);
        }
        else
        {
            transform.localScale = new Vector2(0.5f, 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Bullet"))
        {
            TakeDamage(10f);
            
        }
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        if(hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);

    }

}


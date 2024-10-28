using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2.0f;//�G�̈ړ����x
    public float revTime = 0f;//���]����
    public bool isToRight = false;//true=�E�����@false=������
    public LayerMask groundLayer;//�n�ʃ��C���[

    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (isToRight)
        {
            transform.localScale = new Vector2(-1, 1);
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
                    transform.localScale = new Vector2(-1, 1);
                }
                else
                {
                    transform.localScale = new Vector2(1, 1);
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
        if(isToRight)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
}

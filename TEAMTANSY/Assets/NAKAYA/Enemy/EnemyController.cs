using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2.0f;//敵の移動速度
    public float revTime = 0f;//反転時間
    public bool isToRight = false;//true=右向き　false=左向き
    public LayerMask groundLayer;//地面レイヤー

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
        //地上判定
        bool onGuound = Physics2D.CircleCast(transform.position,
                                            0.5f,
                                            Vector2.down,
                                            0.5f,
                                            groundLayer);
        if(onGuound)
        {
            //速度更新
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

    //接触
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

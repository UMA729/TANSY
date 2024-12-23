using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("âÊñ äOÇ≈Ç‡çsìÆÇ∑ÇÈ")] public bool nonVisible;
    public float speed = 5.0f;
    public SpriteRenderer SR = null;
    public  float Dirtime = 3.0f;

    private float Dur = 0.0f;
    private bool Direction = true;

    private void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Dur += Time.deltaTime;
        if (Dirtime <= Dur)
        {
            if (Direction)
            {
                Direction = false;
            }
            else if (!Direction)
            {
                Direction = true;
            }
            speed *= -1;
        }
    }

    private void FixedUpdate()
    {
        if(SR.isVisible || nonVisible)
        {
            if(Direction)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if(!Direction)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }


        }
    }

}

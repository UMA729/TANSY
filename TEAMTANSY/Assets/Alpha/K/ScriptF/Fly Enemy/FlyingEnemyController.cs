using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    Rigidbody2D rb;                    //Rigitbody変数
    public float hp = 10f;             //HP変数
    public float speed = 5.0f;         //敵速度変数
    public float Dirtime = 3.0f;
    public float tickInterval = 2f;    // ダメージを与える間隔（秒）
    public GameObject EffectObj;
    public Transform EffectPos;
    public Transform Effectmam;

    private float Dur = 0.0f;
    private fireBullet FB;
    private bool Direction = true;
    private bool isTakingDamage = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FB = FindAnyObjectByType<fireBullet>();
        //if (EFF == null)
        //{

        //    Debug.Log("effectスクリプトは存在しません");
        //}
        //else
        //{
        //    Debug.Log("effectスクリプトは存在はします");
        //}
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
            speed *= -1.0f;
            Dur = 0.0f;
        }

    }

    private void FixedUpdate()
    {
        if (Direction)
        {
            transform.localScale = new Vector3(4, 4, 1);
        }
        else if (!Direction)
        {
            transform.localScale = new Vector3(-4, 4, 1);
        }

        rb.velocity = new Vector2(speed,rb.velocity.y);
    }
    public void StartDamageOverTime()
    {
        if (!isTakingDamage) // すでにダメージを受けていない場合のみ開始
        {
            StartCoroutine(ApplyDamageOverTime());
        }
    }
    private IEnumerator ApplyDamageOverTime()
    {
        isTakingDamage = true;

        float duration   = 5.0f;
        float elapsed    =   0f; // 持続時間を追跡する変数
        float firedamage =   0f;

        if (FB.fireBaff == false)
        {
            firedamage = 5f;
        }
        else if (FB.fireBaff == true)
        {
            firedamage = 10f;

        }


        GameObject EfeObj = Instantiate(EffectObj, EffectPos.position, Quaternion.identity, Effectmam); //エフェクトを表示

        while (elapsed < duration)
        {
            // ダメージを与える処理
            TakeDamage(firedamage);

            // 次のダメージまで待機
            yield return new WaitForSeconds(tickInterval);

            // 経過時間を更新
            elapsed += tickInterval;
        }

        if(EffectPos != null)
        {
            Destroy(EfeObj);
        }

        isTakingDamage = false; // ダメージ完了
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10f);

        }
        if (collision.gameObject.CompareTag("FireBullet"))
        {
            StartDamageOverTime();
        }
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        Debug.Log(hp);
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //hp�|�C���g
    public float hp = 10;
    public float speed = 2.0f;//�G�̈ړ����x
    public float revTime = 0f;//���]����
    public float Xscale = 1.0f;
    public float Yscale = 1.0f;
    public float tickInterval = 2f;    // �_���[�W��^����Ԋu�i�b�j
    public float duration = 10f;       // �������ԁi�b�j

    public bool isToRight = false;//true=�E�����@false=������
    public LayerMask groundLayer;//�n�ʃ��C���[

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip encon;    //�G�����ꂽ�Ƃ�

    private Rigidbody2D rb;
    private bool isTakingDamage = false; // �����_���[�W���󂯂Ă��邩�ǂ���
    private fireBullet FB;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        FB = FindObjectOfType<fireBullet>();
        rb = this.GetComponent<Rigidbody2D>();
        if (isToRight)
        {
            transform.localScale = new Vector2(-Xscale, Yscale);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (revTime > 0)
        {
            time += Time.deltaTime;
            if (time >= revTime)
            {
                isToRight = !isToRight;
                time = 0;
                if (isToRight)
                {
                    transform.localScale = new Vector2(-Xscale, Yscale);
                }
                else
                {
                    transform.localScale = new Vector2(Xscale, Yscale);
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
        if (onGuound)
        {
            //���x�X�V
            if (isToRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
    }
    public void StartDamageOverTime()
    {
        if (!isTakingDamage) // ���łɃ_���[�W���󂯂Ă��Ȃ��ꍇ�̂݊J�n
        {
            StartCoroutine(ApplyDamageOverTime());
        }
    }
    private IEnumerator ApplyDamageOverTime()
    {
        isTakingDamage = true;

        float elapsed = 0f; // �������Ԃ�ǐՂ���ϐ�

        float firedamage = 0f;

        if (FB.fireBaff == false)
        {
            firedamage = 5f;
            Debug.Log("a");
        }
        else if (FB.fireBaff == true)
        {
            firedamage = 10f;

            FB.fireBaff = false;
            Debug.Log("i");
        }

        while (elapsed < duration)
        {
            // �_���[�W��^���鏈��
            TakeDamage(firedamage);

            // ���̃_���[�W�܂őҋ@
            yield return new WaitForSeconds(tickInterval);

            // �o�ߎ��Ԃ��X�V
            elapsed += tickInterval;
        }
        isTakingDamage = false; // �_���[�W����
    }
    //�ڐG
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isToRight = !isToRight;
        time = 0;
        if (isToRight)
        {
            transform.localScale = new Vector2(-Xscale, Yscale);
        }
        else
        {
            transform.localScale = new Vector2(Xscale, Yscale);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            TakeDamage(10f);
        }
        if (collision.collider.CompareTag("FireBullet"))
        {
            StartDamageOverTime();
        }
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
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


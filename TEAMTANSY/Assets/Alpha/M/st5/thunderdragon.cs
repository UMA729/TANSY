using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunderdragon : MonoBehaviour
{
    // �����̕ϐ�
    public float hp = 10;
    public float speed = 2.0f;
    public float revTime = 0f;
    public float Xscale = 1.0f;
    public float Yscale = 1.0f;
    public float tickInterval = 2f;
    public float duration = 10f;
    public bool isToRight = false;
    public LayerMask groundLayer;
    public float jumpForce = 10f;
    public float jumpIntervalMin = 10f;
    public float jumpIntervalMax = 30f;
    private float nextJumpTime = 0f;
    private float nextThanderTime = 0f;
    private float Thanderduration = 3f;
    public AudioClip encon;

    public GameObject break_wall;

    // �ǉ�����ϐ�
    public GameObject bulletPrefab;  // �e�̃v���n�u
    public Transform firePoint;      // �e�𔭎˂���ʒu
    public float bulletSpeed = 5f;   // �e�̃X�s�[�h
    private Transform playerTransform; // �v���C���[��Transform

    private Rigidbody2D rb;
    private bool isTakingDamage = false;
    private fireBullet FB;
    private float time = 0;

    //�G�t�F�N�g�v���O����
    public GameObject gameObject1;

    void Start()
    {
        gameObject1.SetActive(false); // gameObject���A�N�e�B�u��
        FB = FindObjectOfType<fireBullet>();
        rb = this.GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // �v���C���[���^�O�Ō���
        if (isToRight)
        {
            transform.localScale = new Vector2(-2, 2);
        }
    }

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

        // �v���C���[�Ɍ������Ēe�𔭎�

        nextThanderTime += Time.deltaTime;

        if (nextThanderTime >= Thanderduration)
        {
            FireBulletAtPlayer();
            nextThanderTime = 0;
        }
    }

    void FixedUpdate()
    {
        // �n�ʔ���
        bool onGround = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 0.5f, groundLayer);

        if (onGround)
        {
            // ���x�X�V
            Rigidbody2D rbody = GetComponent<Rigidbody2D>();
            if (isToRight)
            {
                rbody.velocity = new Vector2(speed, rbody.velocity.y);
            }
            else
            {
                rbody.velocity = new Vector2(-speed, rbody.velocity.y);
            }

            // �����_���W�����v
            if (Time.time >= nextJumpTime)
            {
                Jump();
                nextJumpTime = Time.time + Random.Range(jumpIntervalMin, jumpIntervalMax);
            }
        }
    }

    // �W�����v
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // �W�����v
    }

    // �v���C���[�Ɍ������Ēe�𔭎˂��鏈��
    private void FireBulletAtPlayer()
    {
        if (playerTransform == null) return;

        // �v���C���[�̈ʒu���擾
        Vector2 direction = (playerTransform.position - firePoint.position).normalized;

        // �e�𔭎�
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // �e�ɑ��x��^����
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;
    }

    // �����_���[�W
    public void StartDamageOverTime()
    {
        if (!isTakingDamage)
        {
            StartCoroutine(ApplyDamageOverTime());
        }
    }

    private IEnumerator ApplyDamageOverTime()
    {
        isTakingDamage = true;
        float elapsed = 0f;
        float firedamage = 0f;

        if (FB.fireBaff == false)
        {
            firedamage = 5f;
        }
        else if (FB.fireBaff == true)
        {
            firedamage = 10f;
            FB.fireBaff = false;
        }

        while (elapsed < duration)
        {
            TakeDamage(firedamage);
            yield return new WaitForSeconds(tickInterval);
            elapsed += tickInterval;
        }
        isTakingDamage = false;
    }

    // �Փˏ���
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
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10f);

            gameObject1.SetActive(true); // gameObject���A�N�e�B�u��

            Invoke("Test1", 0.5f); // �֐�Test1��3�b��Ɏ��s �@//�G�t�F�N�g������
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.CompareTag("FireBullet"))
        {
            StartDamageOverTime();
        }

        // �ǂɏՓ˂����ꍇ�̔��]
        if (collision.collider.CompareTag("Wall"))
        {
            ReverseDirection();
        }

        
    }

    // �ǂɏՓ˂����ꍇ�̏���
    private void ReverseDirection()
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

    // �_���[�W����
    public void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Die();
        }
    }

    // ���S����
    private void Die()
    {
        Destroy(gameObject);
        Destroy(break_wall);
    }

    void Test1()
    {
        gameObject1.SetActive(false); // gameObject���A�N�e�B�u��
    }

}

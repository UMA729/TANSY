using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyBoss : MonoBehaviour
{
    
    Rigidbody2D rb;                     
    public float hp = 10f;               // HP
    public float speed = 5.0f;           // �p�j���̓������x
    public float Dirtime = 3.0f;         // �p�j������؂�ւ���Ԋu�i�b�j
    public float tickInterval = 2f;      // �_���[�W��^����Ԋu�i�b�j
    public float patrolSpeed = 2f;       // �p�g���[�����x
    public float chaseSpeed = 3f;        // �ǐՑ��x
    public Transform player;             // �v���C���[��Transform
    public bool isChasing = false;       // �ǐՏ�ԃt���O
                                         
    private float DamDur = 0.0f;         // �΃_���[�W��^�������鎞�Ԃ��v������
    private float DirDur = 0.0f;         // �p�j������؂�ւ��鎞�Ԃ��v��
    private bool isFacingRight = false;  // �������������ɐݒ�
    private bool Direction = true;       // 
    private bool isTakingDamage = false; //

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    { 
        if (isChasing)
        {
            ChasePlayer();  //�v���C���[�͈͓�
        }
        else
        {
            MoveEnemy();    //�v���C���[�͈͊O��
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

        rb.velocity = new Vector2(speed, rb.velocity.y);
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
        fireBullet FB;                          //fireBullet�̌Ăяo��
        FB = FindAnyObjectByType<fireBullet>(); //fireBullet�̃I�u�W�F�N�g�ǂݍ���

    �@�@isTakingDamage = true;                  // �Βe�ۂ��󂯂�
                                                
        float elapsed = 0f;                     // �������Ԃ�ǐՂ���ϐ�

        float firedamage = 0f;                  // �΃_���[�W�����߂�ϐ�

        if (FB.fireBaff == false)               //�o�t�𖢎擾��
        {
            firedamage = 5f;
        }
        else if (FB.fireBaff == true)           //�o�t���擾��
        {
            firedamage = 10f;

            FB.fireBaff = false;                //�o�t��؂�
        }

        while (elapsed < DamDur)
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

    void ChasePlayer()//�v���C���[�̏ꏊ�ֈړ�
    {
        // �v���C���[�̕������v�Z
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // �ړ�
        Vector2 targetPosition = rb.position + directionToPlayer * chaseSpeed * Time.deltaTime;

        rb.MovePosition(targetPosition);

        // �X�v���C�g�̌������X�V
        UpdateSpriteDirection(directionToPlayer.x > 0 ? 1 : -1);
    }

    void UpdateSpriteDirection(int direction)//�ǐՎ��̊���������߂�
    {
        // �E�������ꍇ
        if (direction > 0 && !isFacingRight)
        {
            FlipSprite();
        }
        // ���������ꍇ
        else if (direction < 0 && isFacingRight)
        {
            FlipSprite();
        }
    }
    void FlipSprite()
    {
        // �X�v���C�g�𔽓]
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // X���𔽓]
        transform.localScale = scale;
    }
    void MoveEnemy() //
    {
        DirDur += Time.deltaTime;
        if (Dirtime <= DirDur)
        {
            if (Direction)
            {
                Direction = false;  //�E�������������ɔ��]
            }
            else if (!Direction)
            {
                Direction = true;   //���������E�����ɔ��]
            }
            speed *= -1.0f;         //�����؂�ւ����Ԃ�����ƈړ������𔽓]
            DirDur = 0.0f;          //�����؂�ւ����Ԃ̏�����
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float Damage = 10.0f;

        if (collision.collider.CompareTag("Bullet"))        // �ʏ�e�ۂɂ������
        {
            TakeDamage(Damage);                             // �ʏ�e�ۃ_���[�W
        }
        if (collision.collider.CompareTag("FireBullet"))    // �Βe�ۂɂ������
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

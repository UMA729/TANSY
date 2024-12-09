using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    //hp�|�C���g
    public float hp = 10;
    public float tickInterval = 2f;    // �_���[�W��^����Ԋu�i�b�j
    public float duration = 10f;       // �������ԁi�b�j

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip encon;    //�G�����ꂽ�Ƃ�

    private bool isTakingDamage = false; // �����_���[�W���󂯂Ă��邩�ǂ���
    private fireBullet FB;
    // Start is called before the first frame update
    public float patrolSpeed = 2f; // �p�g���[�����x
    public float chaseSpeed = 3f; // �ǐՑ��x
    public float patrolRange = 3f; // �p�g���[���͈�
    public float detectionRange = 5f; // �v���C���[���m�͈�
    public Transform player; // �v���C���[��Transform

    private Vector2 startPos; // �����ʒu
    private int patrolDirection = -1; // �����ړ����������ɐݒ�
    public bool isChasing = false; // �ǐՏ�ԃt���O
    private bool isFacingRight = false; // �������������ɐݒ�

    void Start()
    {
        FB = FindObjectOfType<fireBullet>();
        startPos = transform.position; // �����ʒu���L�^
    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer(); // �v���C���[��ǐ�
        }
        else
        {
            Patrol(); // �p�g���[��
        }
    }

    void Patrol()
    {
        // �p�g���[���ړ�
        transform.position += Vector3.right * patrolSpeed * patrolDirection * Time.deltaTime;

        // �͈͂𒴂���������𔽓]
        if (Mathf.Abs(transform.position.x - startPos.x) > patrolRange)
        {
            patrolDirection *= -1; // �ړ������𔽓]
            UpdateSpriteDirection(patrolDirection);
        }
    }

    void ChasePlayer()
    {
        // �v���C���[�̕������v�Z
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // �ړ�
        transform.position += (Vector3)directionToPlayer * chaseSpeed * Time.deltaTime;

        // �X�v���C�g�̌������X�V
        UpdateSpriteDirection(directionToPlayer.x > 0 ? 1 : -1);
    }

    void UpdateSpriteDirection(int direction)
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

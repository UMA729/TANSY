using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyBoss : MonoBehaviour
{
    
    Rigidbody2D  rb;                     
    public float hp = 10f;               // HP
    public float speed = 5.0f;           // �p�j���̓������x
    public float Dirtime = 3.0f;         // �p�j������؂�ւ���Ԋu�i�b�j
    public float Ticktime = 2f;          // �_���[�W��^����Ԋu�i�b�j
    public float Atacktime = 1.0f;       // �U�������銴�o�i�b�j
    public float patrolSpeed = 2f;       // �p�g���[�����x
    public float chaseSpeed = 3f;        // �ǐՑ��x
    public float RayLen;
    public Transform player;             // �v���C���[�̈ʒu��X�P�[���Ȃ�
    public bool isChasing = false;       // �ǐՏ�ԃt���O
    public GameObject EffectObj;
    public Transform EffectPos;
    public Transform Effectmam;
    public LayerMask MyLayer;
    private float DirDur = 0.0f;         // �p�j������؂�ւ��鎞�Ԃ��v��
    private bool isFacingRight = true;   // �I�u�W�F�N�g�̌����t���O
    private bool isTakingDamage = false; // �Βe�ۂ����łɎ󂯂Ă��邩�̃t���O

    //�G�U��
    //public Transform firePoint;          // �G�U���̔��ˈʒu
    //public GameObject firePrefab;        // �G�U���̃v���n�u
    //public float fireSpeed = 2.0f;       // �G�U���̑��x
    //private float AtaDur = 0.0f;         // �G�U���̊Ԋu
    //private bool isAtackAria = false;    // �U�����J�n����͈�

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

                            //�G�U���Ԋu
                            //if (isAtackAria)                 
                            //{                                 //
                            //    AtaDur += Time.deltaTime;     //
                            //    if (Atacktime <= AtaDur)      //
                            //    {                             //
                            //        fireAtack();              //
                            //        AtaDur = 0.0f;            //
                            //    }                             //
                            //}                                 //
        }
    }
    private void FixedUpdate()
    {
       
        if (!isChasing)
        {
            float XScale = 6.0f; //
                                 // �I�u�W�F�N�g�Ɠ����X�P�[���l��
            float YScale = 6.0f; //

            if (isFacingRight)
            {
                transform.localScale = new Vector3(XScale, YScale, 1);
            }
            else if (!isFacingRight)
            {
                transform.localScale = new Vector3(-XScale, YScale, 1);
            }

            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }
    public void StartDamageOverTime()
    {
        if (!isTakingDamage) // ���łɃ_���[�W���󂯂Ă��Ȃ��ꍇ�̂݊J�n
        {
            Debug.Log("����܂����B");
            StartCoroutine(ApplyDamageOverTime());
        }
    }
    private IEnumerator ApplyDamageOverTime()
    {
        
        fireBullet FB;                          //fireBullet�̌Ăяo��
        FB = FindAnyObjectByType<fireBullet>(); //fireBullet�̃I�u�W�F�N�g�ǂݍ���

    �@�@isTakingDamage = true;          // �Βe�ۂ��󂯂�

        float duration = 5.0f;
        float elapsed = 0.0f;             // �������Ԃ�ǐՂ���ϐ�
        float firedamage = 0f;          // �΃_���[�W�����߂�ϐ�

        GameObject EfeObj = Instantiate(EffectObj, EffectPos.position, Quaternion.identity, Effectmam); //�G�t�F�N�g��\��


        if (FB.fireBaff == false)       //�o�t�𖢎擾��
        {
            firedamage = 5f;
        }
        else if (FB.fireBaff == true)   //�o�t���擾��
        {
            firedamage = 10f;
        }

        Debug.Log("�΍U�����J�n���܂��B");
        
        while (elapsed < duration)
        {
            // �_���[�W��^���鏈��
            TakeDamage(firedamage);


            // ���̃_���[�W�܂őҋ@
            yield return new WaitForSeconds(Ticktime);

            // �o�ߎ��Ԃ��X�V
            elapsed += Ticktime;
        }

        Destroy(EfeObj);
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
        // �I�u�W�F�N�g�𔽓]
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // X���𔽓]
        transform.localScale = scale;
    }
    void MoveEnemy() 
    {
        //�v���C���[�Ƃ̋�����
        var hit = Physics2D.Raycast(transform.position, (player.position - transform.position).normalized * RayLen, RayLen, MyLayer);

        //�v���C���[��Raycast�ɐG�ꂽ��
        if (hit.collider != null && !isChasing)
        {
            isChasing = true;
            if (isFacingRight)
            {
                isFacingRight = !isFacingRight;
            }
        }
        DirDur += Time.deltaTime;
        if (Dirtime <= DirDur)
        {
            if (isFacingRight)
            {
                isFacingRight = false;  //�E�������������ɔ��]
            }
            else if (!isFacingRight)
            {
                isFacingRight = true;   //���������E�����ɔ��]
            }
            speed *= -1.0f;         //�����؂�ւ����Ԃ�����ƈړ������𔽓]
            DirDur = 0.0f;          //�����؂�ւ����Ԃ̏�����
        }
    }

    //�G�U���̊J�n
    //void fireAtack()
    //{                                                                                       //
    //    Vector2 direction = (player.position - firePoint.position).normalized;              //
    //
    //    GameObject fire = Instantiate(firePrefab, firePoint.position, Quaternion.identity); //
    //
    //    Rigidbody2D fireRb = fire.GetComponent<Rigidbody2D>();                              //
    //    fireRb.velocity = direction * fireSpeed;                                            //
    //}                                                                                       //

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float Damage = 10.0f;

        Debug.Log("���̃R���C�_�[�����I�u�W�F�N�g�l�[����" + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Bullet"))        // �ʏ�e�ۂɂ������
        {
            TakeDamage(Damage);                             // �ʏ�e�ۃ_���[�W
        }
        if (collision.gameObject.CompareTag("FireBullet"))    // �Βe�ۂɂ������
        {
            Debug.Log("������܂����B");
            StartDamageOverTime();
        }
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;         //HP����������̐��l�ň���
        Debug.Log("�_���[�W��^���܂����B" + hp);
        if (hp <= 0)
        {
            Die();            //HP��0�ɂȂ��������
        }
    }

    private void Die()
    {
        Destroy(gameObject); //�G�I�u�W�F�N�g���폜
    }
}

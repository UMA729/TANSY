using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    //�萔
    Rigidbody2D rb;
    public float hp = 10f;               // HP
    public float speed = 5.0f;           // �p�j���̓������x
    public float Dirtime = 3.0f;         // �p�j������؂�ւ���Ԋu�i�b�j
    public float Ticktime = 2f;          // �_���[�W��^����Ԋu�i�b�j

    //�G�t�F�N�g���o�����߂ɕK�v
    public GameObject EffectObj;         //�G�t�F�N�g�I�u�W�F�N�g
    public Transform EffectPos;          //�G�t�F�N�g���o���ʒu
    public Transform Effectmam;          //�G�t�F�N�g�̐e�I�u�W�F�N�g

    private float DirDur = 0.0f;         // �p�j������؂�ւ��鎞�Ԃ��v��
    private bool isFacingRight = true;   // �I�u�W�F�N�g�̌����t���O
    private bool isTakingDamage = false; // �Βe�ۂ����łɎ󂯂Ă��邩�̃t���O


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        MoveEnemy();   //�v���C���[�͈͊O��
        
    }
    private void FixedUpdate()
    {

        float XScale = 4.0f; //
                             // �I�u�W�F�N�g�Ɠ����X�P�[���l��
        float YScale = 4.0f; //

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

        isTakingDamage = true;          // �Βe�ۂ��󂯂�

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

    void MoveEnemy()
    {
        //�p�g���[���������؂�ւ�
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
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("needle"))
        {
            float Needam = 5;
            TakeDamage(Needam);
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

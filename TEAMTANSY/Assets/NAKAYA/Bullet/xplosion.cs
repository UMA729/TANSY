using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xplosion : MonoBehaviour
{
    public GameObject ballPrefab; // ���˂��鋅�̃v���n�u
    private MPController mp;
    public float launchForce = 10f; // ����ł�
    public Transform shootingPoint; // �e�̔��ˈʒu
    public float fireRate = 5f;
    public float nextFireTime = 0f;
    //���j���N�������߂̋���
    public float explosionDistance = 5f;
    //���j�G�t�F�N�g
    public GameObject explosionEffect;

    public Transform target;

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip Bullet;    //�e����

    // Start is called before the first frame update
    void Start()
    {
        // �v���C���[�̃��x���Ǘ��X�N���v�g���擾
        mp = GetComponent<MPController>();
    }

    // Update is called once per frame
    void Update()
    {
        // �ʂƃ^�[�Q�b�g�̋������v�Z
        float distance = Vector2.Distance(transform.position, target.position);

        // �������w�肵�����������ȉ��ɂȂ����甚��
        if (distance <= explosionDistance)
        {
            Explode();
        }
        if (Input.GetMouseButtonDown(1))
        {
            LaunchBall();
        }
    }

    // ��������
    void Explode()
    {
        // �����G�t�F�N�g�𐶐�
        if (explosionEffect != null)
        {
            Debug.Log("����������");
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // �ʁi�I�u�W�F�N�g�j���A�N�e�B�u�ɂ��邩�폜
        Destroy(gameObject);
    }
    void LaunchBall()
    {
        // �v���C���[�̌����ɋʂ��΂�
        Vector2 shootDirection = transform.right;  // �v���C���[�̉E�������擾�i�����Ɉˑ��j

        // �ʂ��C���X�^���X��
        GameObject bullet = Instantiate(ballPrefab, transform.position, Quaternion.identity);

        // �ʂ�Rigidbody2D�ɑ��x��^���Ĕ���
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * launchForce;
    }
}


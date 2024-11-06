using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBullet : MonoBehaviour
{
    public GameObject ballPrefab; // ���˂��鋅�̃v���n�u
    private MPController mp;
    public float launchForce = 10f; // ����ł�
    public Transform shootingPoint; // �e�̔��ˈʒu
    public float fireRate = 5f;
    public float nextFireTime = 0f;

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip Bullet;    //�e����

    void Start()
    {
        // �v���C���[�̃��x���Ǘ��X�N���v�g���擾
        mp = GetComponent<MPController>();

        // �e�̐i�s�����ɍ��킹�ĉ�]������
        Vector2 direction = GetComponent<Rigidbody2D>().velocity.normalized;  // �e�̐i�s����
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  // �������p�x�ɕϊ�
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));  // �p�x��K�p
    }

    void Update()
    {
       
    }

    public void LaunchBall()
    {
        // �v���C���[���E���������������𔻒�
        Vector2 shootDirection = (transform.localScale.x > 0) ? Vector2.right : Vector2.left;

        // �e�ۂ𐶐����Ĕ��ˈʒu�ɔz�u
        GameObject bullet = Instantiate(ballPrefab, shootingPoint.position, Quaternion.identity);

        // �e�ۂɐi�s�����Ƒ��x��ݒ�
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * launchForce;

        // �e�ۂ��v���C���[�Ɠ��������Ői�ނ悤�ɉ�]��ݒ�i�K�v�ɉ����āj
        bullet.transform.localScale = new Vector3(shootDirection.x, 1, 1);  // ���E���]
    }


   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComtller : MonoBehaviour
{
    public GameObject ballPrefab; // ���˂��鋅�̃v���n�u
    public float launchForce = 10f; // ����ł�
    public Transform shootingPoint; // �e�̔��ˈʒu
    public float fireRate = 1f; // �e�ۂ𔭎˂���N�[���^�C��


    public float nextFireTime = 0f;

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip meShoot;    //�e����

    private PlayerController PC;
    private Vector2 shootDirection;
    private void Start()
    {
        PC = FindAnyObjectByType<PlayerController>();
    }
    void Update()
    {
        if (PC.axisH > 0.0f)
        {
            shootDirection = Vector2.right;
        }
        if (PC.axisH < 0.0f)
        {
            shootDirection = Vector2.left;
        }
    }


    public void LaunchBall()
    {
        // �e�ۂ𐶐����Ĕ��ˈʒu�ɔz�u
        GameObject bullet = Instantiate(ballPrefab, shootingPoint.position, Quaternion.identity);

        // �e�ۂɐi�s�����Ƒ��x��ݒ�
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * launchForce;

        // �e�ۂ��v���C���[�Ɠ��������Ői�ނ悤�ɉ�]��ݒ�i�K�v�ɉ����āj
        bullet.transform.localScale = new Vector3(shootDirection.x, 1, 1);  // ���E���]
    }

}
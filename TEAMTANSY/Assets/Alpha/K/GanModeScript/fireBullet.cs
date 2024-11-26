using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBullet : MonoBehaviour
{
    public GameObject ballPrefab; // ���˂��鋅�̃v���n�u
    private MPController mp;
    public float launchForce = 10f; // ����ł�
    public Transform shootingPoint; // �e�̔��ˈʒu
    public float fireRate = 5f;
    public float nextFireTime = 0f;
    public bool fireBaff = false;
    public bool torchCharge = false;
    public AudioClip meShoot;    //�e����

    private PlayerController PC;

    Vector2 shootDirection;
    void Start()
    {
        PC = FindObjectOfType<PlayerController>();
        shootDirection = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (PC.axisH > 0.0f)
        {
            shootDirection = Vector2.right;
        }
        else if (PC.axisH < 0.0f)
        {
            shootDirection = Vector2.left;
        }
    }
    public void LaunchBall()
    {
        // �v���C���[���E���������������𔻒�

        // �e�ۂ𐶐����Ĕ��ˈʒu�ɔz�u
        GameObject bullet = Instantiate(ballPrefab, shootingPoint.position, Quaternion.identity);

        // �e�ۂɐi�s�����Ƒ��x��ݒ�
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * launchForce;

        // �e�ۂ��v���C���[�Ɠ��������Ői�ނ悤�ɉ�]��ݒ�i�K�v�ɉ����āj
        bullet.transform.localScale = new Vector3(shootDirection.x, 1, 1);  // ���E���]
    }

    public void Active()
    {
        fireBaff = true;
        torchCharge = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBullet : MonoBehaviour
{
    public GameObject ballPrefab; // ���˂��鋅�̃v���n�u
    int Mp;
    public float launchForce = 500f; // ����ł�
    public float fireRate = 5f;

    private float nextFireTime = 0f;

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip Bullet;    //�e����
    void Update()
    {
        // �E�N���b�N�����o
        if (Input.GetMouseButtonDown(1) && Time.time >= nextFireTime) // �E�N���b�N
        {
            LaunchBall();
            nextFireTime = Time.time + 5f / fireRate; // �N�[���^�C����ݒ�


            //+++ �T�E���h�Đ��ǉ� +++
            //�T�E���h�Đ�
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM��~
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(Bullet);
            }
        }
    }

    void LaunchBall()
    {
        // �}�E�X�̈ʒu���擾
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z����0�ɐݒ�i2D��ԂȂ̂Łj

        // ���𐶐�
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);

        // ����Rigidbody���擾
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        // ���˕������v�Z
        Vector2 launchDirection = (mousePosition - transform.position).normalized;

        // ���ɗ͂�������
        rb.AddForce(launchDirection * launchForce);
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomBullet : MonoBehaviour
{
    // �e�ۃv���n�u�̔z��
    public GameObject[] bulletPrefabs;

    // �e�ۂ̔��ˈʒu
    public Transform firePoint;

    // �e�ۂ𔭎˂���Ԋu
    public float fireRate = 0.5f;

    // ���ɔ��˂ł��鎞��
    private float nextFireTime = 0f;

    // Update is called once per frame
    void Update()
    {
        // Fire�L�[��������A���ˉ\���Ԃł���Βe�ۂ𔭎�
        if (Input.GetMouseButtonDown(1) && Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    // �e�ۂ𔭎˂���֐�
    void FireBullet()
    {
        // �����_���Œe�ۂ�I��
        int randomIndex = Random.Range(0, bulletPrefabs.Length);

        // �I�΂ꂽ�e�ۂ𔭎�
        GameObject bullet = Instantiate(bulletPrefabs[randomIndex], firePoint.position, firePoint.rotation);

        // �e�ۂɑ��x��������i�Ⴆ�΁A�E�����ɔ��ˁj
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = firePoint.right * 10f; // �E������10���j�b�g/�b�Ŕ���
        }
    }
}


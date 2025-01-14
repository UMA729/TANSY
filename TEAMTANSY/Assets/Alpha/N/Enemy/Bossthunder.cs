using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossthunder : MonoBehaviour
{
    public bool isToRight = false;
    public GameObject bulletPrefab;  // �e�̃v���n�u
    public Transform firePoint;      // �e�𔭎˂���ʒu
    public float bulletSpeed = 5f;   // �e�̃X�s�[�h
    public float deleteTime = 5.0f;
    private Transform playerTransform; // �v���C���[��Transform

    private Rigidbody2D rb;
    private bool isTakingDamage = false;
    private BossCommtller BC;
    private float time = 0;
    private float nextThanderTime = 0f;
    private float Thanderduration = 3f;
    public AudioClip encon;

    void Start()
    {
        BC = FindAnyObjectByType<BossCommtller>();
        rb = this.GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // �v���C���[���^�O�Ō���
        if (isToRight)
        {
            transform.localScale = new Vector2(-2, 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �v���C���[�Ɍ������Ēe�𔭎�

        nextThanderTime += Time.deltaTime;

        if (nextThanderTime >= Thanderduration)
        {
            FireBulletAtPlayer();
            nextThanderTime = 0;
        }
    }
    
    public void FireBulletAtPlayer()
    {
        if (playerTransform == null) return;

        // �v���C���[�̈ʒu���擾
        Vector2 direction = (playerTransform.position - firePoint.position).normalized;

        // �e�𔭎�
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // �e�ɑ��x��^����
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;
    }
}

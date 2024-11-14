using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBullet : MonoBehaviour
{
    public GameObject ballPrefab; // ���˂��鋅�̃v���n�u
    private MPController mp;
    public float launchForce = 10f; // ����ł�
    public Transform shootingPoint; // �e�̔��ˈʒu
    public float fireRate = 5f;
    public float nextFireTime = 0f;
    public GameObject wallPrefab;  // ��������ǂ̃v���n�u
    public float wallHeight = 2f;  // �ǂ̍����i�n�ʂƂ̋����j
    public float wallLifeTime = 3f;  // �ǂ����݂��鎞��

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip Bullet;    //�e����

    // Start is called before the first frame update
    void Start()
    {
        // �v���C���[�̃��x���Ǘ��X�N���v�g���擾
        mp = GetComponent<MPController>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("����������");
        if (collision.gameObject.CompareTag("Graund"))
        {
            GetComponent<Collider2D>();
            // �Փ˒n�_���擾
            Vector3 collisionPoint = collision.contacts[0].point;

            // �ǂ��Փ˒n�_�ɐ���
            GameObject wall = Instantiate(wallPrefab, collisionPoint, Quaternion.identity);

            // �ǂ��w�莞�Ԍ�ɏ���
            Destroy(wall, 3f);  // 3�b��ɕǂ�����
            Debug.Log("��������������������");
        }
        if (collision.gameObject.tag == "Graund")
        {
            Debug.Log("���₠��������������");
        }


    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            LaunchBall();
        }
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

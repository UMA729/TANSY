using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyBullet : MonoBehaviour
{
    //public
    public GameObject ballPrefab; // ���˂��鋅�̃v���n�u
    public float launchForce = 10f; // ����ł�
    public Transform shootingPoint; // �e�̔��ˈʒu
    public float fireRate = 1f; // �e�ۂ𔭎˂���N�[���^�C��
    public float deleteTime = 2.0f;//��������
    public Transform player;  // �v���C���[��Transform���A�T�C��

    //private
    private Vector2 shootDirection;
    private SpriteRenderer spriteRenderer;//�{�X�̂���X�v���C�g�����_���[
    private BossCommtller BC;

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip BSS;    //�e����
    // Start is called before the first frame update

    //�X�^�[�g�֐�
    //����
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        BC = FindAnyObjectByType<BossCommtller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    public void LaunchBall()
    {
        // ���@�𐶐����Ĕ��ˈʒu�ɔz�u
        GameObject bullet = Instantiate(ballPrefab, shootingPoint.position, Quaternion.identity)as GameObject;
        //+++ �T�E���h�Đ��ǉ� +++
        //�T�E���h�Đ�
        AudioSource soundPlayer = GetComponent<AudioSource>();
        if (soundPlayer != null)
        {
            //BGM��~
            soundPlayer.Stop();
            soundPlayer.PlayOneShot(BSS);
        }
        Destroy(bullet, deleteTime);
    }
}

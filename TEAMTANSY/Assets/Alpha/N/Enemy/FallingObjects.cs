using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    public GameObject ballPrefab; // ���˂��鋅�̃v���n�u
    public Transform shootingPoint; // �e�̔��ˈʒu
    private Vector2 shootDirection;
    private SpriteRenderer spriteRenderer;//�{�X�̂���X�v���C�g�����_���[
    public float deleteTime = 2.0f;//��������
    private BossCommtller BC;
    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip BSS;    //�e����
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        BC = FindAnyObjectByType<BossCommtller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LaunchBall()
    {
        Debug.Log("���Ȃ����I�V���W����");
        // ���@�𐶐����Ĕ��ˈʒu�ɔz�u
        GameObject bullet = Instantiate(ballPrefab, shootingPoint.position, Quaternion.identity) as GameObject;
        //+++ �T�E���h�Đ��ǉ� +++
        //�T�E���h�Đ�
        AudioSource soundPlayer = GetComponent<AudioSource>();
        if (soundPlayer != null)
        {
            //BGM��~
            soundPlayer.Stop();
            soundPlayer.PlayOneShot(BSS);
        }
        Debug.Log("���₾���Ă΂���������������������������");
        Destroy(bullet, deleteTime);
    }
}

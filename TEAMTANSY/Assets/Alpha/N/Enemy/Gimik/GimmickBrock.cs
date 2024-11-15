using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBrock : MonoBehaviour
{
    // ���ꂪ�����܂ł̑ҋ@����
    public float crumbleDelay = 0.5f;

    // ���ꂪ���ꂽ��̃G�t�F�N�g��T�E���h�Ȃ�
    public GameObject crumbleEffect;

    private Collider2D platformCollider;  // ����̃R���C�_�[

    void Start()
    {
        // ����̃R���C�_�[���擾
        platformCollider = GetComponent<Collider2D>();
    }

    // �v���C���[������ɐG�ꂽ���̏���
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փ˂����I�u�W�F�N�g���v���C���[���ǂ������`�F�b�N
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�G�ꂽ");
            // ���ꂪ�����܂ł̒x�����Ԃ��J�n
            StartCoroutine(CrumblePlatform());
        }
    }

    // ���ꂪ�����R���[�`��
    private IEnumerator CrumblePlatform()
    {
        // ���ꂪ�����O�ɑҋ@
        yield return new WaitForSeconds(crumbleDelay);

        // ���ꂪ����鏈��
        // �Ⴆ�΁A����̃R���C�_�[�𖳌������āA���ꂪ������悤�ɂ���
        if (platformCollider != null)
        {
            platformCollider.enabled = false;  // �R���C�_�[�𖳌���
        }

        // ����̃G�t�F�N�g���ݒ肳��Ă���Ε\������
        if (crumbleEffect != null)
        {
            Instantiate(crumbleEffect, transform.position, Quaternion.identity);
        }

        // ������폜�i�����j�܂��͔�\���ɂ���
        Destroy(gameObject, 0.5f);  // 0.5�b��ɃI�u�W�F�N�g���폜
    }
}

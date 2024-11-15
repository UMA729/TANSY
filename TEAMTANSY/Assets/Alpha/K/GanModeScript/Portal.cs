using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal; // �ڑ���̃|�[�^��
    private bool isTeleporting; // �e���|�[�g�����ǂ����̃t���O

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �v���C���[���ڐG���ڑ���|�[�^�����ݒ肳��Ă���ꍇ
        if (other.CompareTag("Player") && linkedPortal != null && !isTeleporting)
        {
            StartCoroutine(Teleport(other)); // �e���|�[�g�������J�n
        }
    }

    private IEnumerator Teleport(Collider2D player)
    {
        isTeleporting = true;                 // �e���|�[�g���t���O�𗧂Ă�
        linkedPortal.isTeleporting = true;    // �����N��̃|�[�^�����ꎞ�I�ɖ����ɂ���

        // �ڑ���|�[�^���̈ʒu�Ƀv���C���[���ړ�
        player.transform.position = linkedPortal.transform.position;

        // �N�[���_�E�����Ԃ�݂��ĘA���e���|�[�g��h��
        yield return new WaitForSeconds(0.5f);

        isTeleporting = false;                 // ���݂̃|�[�^���̃e���|�[�g�t���O������
        linkedPortal.isTeleporting = false;    // �����N��̃|�[�^���̃e���|�[�g�t���O������
    }
}

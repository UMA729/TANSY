using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public GameObject bluePortalPrefab;
    public GameObject orangePortalPrefab;
    public LayerMask wallLayer;
    public float rayDistance = 10f;   // Ray�̍ő勗��
    public bool shootRight = true;    // �E������Ray���΂����ifalse�ō������j

    private GameObject bluePortal;
    private GameObject orangePortal;
    private Vector2 direction;
    private PlayerController PC;
    private void Start()
    {
        PC = FindAnyObjectByType<PlayerController>();
    }
    void Update()
    {
      if (PC.axisH > 0.0f)
        {
            direction = Vector2.right;
        }
      if (PC.axisH < 0.0f)
        {
            direction = Vector2.left;
        }
    }

    public void CreatePortal()
    {
        // �v���C���[�̈ʒu��Ray�̎n�_�ɐݒ�
        Vector2 origin = transform.position;

        // Ray�̕�����ݒ�i�E�����܂��͍������j
       

        // �f�o�b�O�p��Ray�̉���
        Debug.DrawRay(origin, direction * rayDistance, Color.red, 1.0f);

        // Raycast�𔭎�
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayDistance, wallLayer);

        if (hit.collider != null)  // �ǂɓ��������ꍇ
        {
            Debug.Log("�ǂɃq�b�g���܂���: " + hit.collider.name);

            // �����̃|�[�^��������΍폜
            if (bluePortal != null) Destroy(bluePortal);
            if (orangePortal != null) Destroy(orangePortal);

            // �|�[�^�����q�b�g�ʒu�ɔz�u
            bluePortal = Instantiate(bluePortalPrefab, hit.point, Quaternion.identity);

            // �ǂ̔��Α��ɃI�����W�|�[�^����z�u
            Vector2 oppositePosition = hit.point + hit.normal * -2.0f;
            orangePortal = Instantiate(orangePortalPrefab, oppositePosition, Quaternion.identity);

            // �|�[�^�����m�������N
            var bluePortalScript = bluePortal.GetComponent<Portal>();
            var orangePortalScript = orangePortal.GetComponent<Portal>();
            if (bluePortalScript != null && orangePortalScript != null)
            {
                bluePortalScript.linkedPortal = orangePortalScript;
                orangePortalScript.linkedPortal = bluePortalScript;
            }
        }
        else
        {
            Debug.Log("Ray�����ɂ��q�b�g���܂���ł���");
        }
    }
}

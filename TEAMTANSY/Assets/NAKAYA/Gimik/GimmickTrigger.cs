using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickTrigger : MonoBehaviour
{
    public float riseSpeed = 4.0f;  //�㏸����
    public float riseDistance = 5f; // �㏸����

    private bool isRising = false;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; // �����ʒu���L�^
    }

    // Update is called once per frame
    void Update()
    {
        if (isRising)
        {
            // �㏸����
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;

            // ��苗���㏸�������~
            if (transform.position.y >= startPosition.y + riseDistance)
            {
                isRising = false;
                // ���̈ʒu�ɖ߂��ꍇ�͈ȉ���L���ɂ���
                transform.position = startPosition;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Bullet")) // �v���C���[�̃^�O���m�F
        {
            ActivateGimmick();
        }
    }

    private void ActivateGimmick()
    {
        Debug.Log("�M�~�b�N���������܂����I");
        isRising = true; // �㏸���J�n
    }

}

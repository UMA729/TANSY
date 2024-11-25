using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomGenerator : MonoBehaviour
{
    public GameObject obj;                 // ��������I�u�W�F�N�g
    [SerializeField] Transform pos;        // �����ʒu
    [SerializeField] Transform pos2;       // �����ʒu
    float minX, maxX, minY, maxY;         // �����͈�

    float frame = 0f;
    public float elepsed = 3f;            // �I�u�W�F�N�g�����̊Ԋu

    void Start()
    {
        // ������: �����͈͂����߂�
        minX = Mathf.Min(pos.position.x, pos2.position.x);
        maxX = Mathf.Max(pos.position.x, pos2.position.x);
        minY = Mathf.Min(pos.position.y, pos2.position.y);
        maxY = Mathf.Max(pos.position.y, pos2.position.y);
    }

    void Update()
    {
        frame += Time.deltaTime;

        if (frame > elepsed)
        {
            frame = 0f;
            // �����_���Ő����ʒu������
            float posX = Random.Range(minX, maxX);
            float posY = Random.Range(minY, maxY);

            // �I�u�W�F�N�g�𐶐�
            GameObject newObj = Instantiate(obj, new Vector3(posX, posY, 0), Quaternion.identity);

            // ObjectDelete �R���|�[�l���g��ǉ����č폜���Ԃ�ݒ�
            ObjectDelete objectDelete = newObj.GetComponent<ObjectDelete>();
            if (objectDelete != null)
            {
                objectDelete.deleteTime = 0.5f; // �Ⴆ��0.5�b�ŏ�����
            }
        }
    }
}

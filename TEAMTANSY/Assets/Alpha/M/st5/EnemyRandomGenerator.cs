using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomGenerator : MonoBehaviour
{
    // [SerializeField] List<GameObject> enemyList;    // �����I�u�W�F�N�g
    public GameObject obj;
    [SerializeField] Transform pos;                 // �����ʒu
    [SerializeField] Transform pos2;                // �����ʒu
    float minX, maxX, minY, maxY;                   // �����͈�

    float frame = 0f;
    public float elepsed = 3f;
    //[SerializeField] int generateFrame = 300;        // ��������Ԋu

    void Start()
    {
        //Destroy(obj, deleteTime);

        minX = Mathf.Min(pos.position.x, pos2.position.x);
        maxX = Mathf.Max(pos.position.x, pos2.position.x);
        minY = Mathf.Min(pos.position.y, pos2.position.y);
        maxY = Mathf.Max(pos.position.y, pos2.position.y);
    }

    void Update()
    {
        frame = frame + Time.deltaTime;

        Debug.Log(frame);

        if (frame > elepsed)
        {
            frame = 0;
            //Debug.Log("seisei");
            // �����_���Ŏ�ނƈʒu�����߂�
           //int index = Random.Range(0, obj);
            float posX = Random.Range(minX, maxX);
            float posY = Random.Range(minY, maxY);

            Instantiate(obj, new Vector3(posX, posY, 0), Quaternion.identity);

            //Destroy(obj, deleteTime);
        }
    }
}

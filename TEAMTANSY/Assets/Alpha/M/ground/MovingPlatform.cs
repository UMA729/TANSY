using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // �������͈́i�㉺�j
    public float moveDistance = 5f; // ���ꂪ�����ő�̋���
    public float moveSpeed = 2f;    // ���ꂪ�ړ����鑬�x
    public bool moveUpwards = true; // �㉺�����̏����ݒ�

    private Vector3 initialPosition; // ����̏����ʒu
    private bool movingUp = true;    // ���ꂪ��ɓ����Ă��邩�ǂ���

    void Start()
    {
        initialPosition = transform.position; // ����̏����ʒu��ۑ�
    }

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        // ����̌��݂̈ʒu���擾
        Vector3 targetPosition = initialPosition;

        // �㉺�����ɓ�����
        if (movingUp)
        {
            targetPosition.y += moveDistance; // ��Ɉړ�
        }
        else
        {
            targetPosition.y -= moveDistance; // ���Ɉړ�
        }

        // ������w�肵�����x�ňړ�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // �ڕW�ʒu�ɓ��B����������]��
        if (transform.position == targetPosition)
        {
            movingUp = !movingUp;
        }
    }
}

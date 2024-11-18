using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickTrigger : MonoBehaviour
{
    public GameObject Prefab; // �����ǂ⏰�̃v���n�u
    // ���̌��̈ʒu�ƖڕW�̈ʒu
    public Vector3 originalPosition;
    public Vector3 targetPosition;

    // �㏸�ɂ����鎞�ԁi�b�j
    public float riseTime = 2f;

    // ���ɖ߂鎞�ԁi�b�j
    public float returnTime = 2f;

    // ���݂̏�ԁi�㏸�����߂钆���j
    private enum FloorState { Idle, Rising, Returning }
    private FloorState currentState = FloorState.Idle;

    // ���Ԍv���p�̕ϐ�
    private float timer = 0f;

    void Start()
    {
        // ���̈ʒu�ƃ^�[�Q�b�g�̈ʒu��ݒ�
        originalPosition = transform.position;
        targetPosition = originalPosition + new Vector3(0, 5f, 0);  // ���5���j�b�g�ړ��������
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Debug.Log("���O�͍Ō��");
            switch (currentState)
            {
                case FloorState.Idle:
                    // ������ԁA�������Ȃ�
                    break;

                case FloorState.Rising:
                    // �����㏸��
                    Debug.Log("�E���Ɩ񑩂������");
                    timer += Time.deltaTime;
                    float riseProgress = Mathf.Clamp01(timer / riseTime);
                    transform.position = Vector3.Lerp(originalPosition, targetPosition, riseProgress);

                    if (riseProgress >= 1f)
                    {
                        // �㏸������������߂��Ԃ�
                        currentState = FloorState.Returning;
                        timer = 0f;
                    }
                    break;

                case FloorState.Returning:
                    // �������̈ʒu�ɖ߂�
                    timer += Time.deltaTime;
                    float returnProgress = Mathf.Clamp01(timer / returnTime);
                    transform.position = Vector3.Lerp(targetPosition, originalPosition, returnProgress);

                    if (returnProgress >= 1f)
                    {
                        // �߂�I������猳�̏�Ԃ�
                        currentState = FloorState.Idle;
                        timer = 0f;
                    }
                    break;
            }

        }
    }

    // �����㏸������
    public void TriggerFloorRise()
    {
        Debug.Log("����͉R��");
        if (currentState == FloorState.Idle)
        {
            currentState = FloorState.Rising;
            timer = 0f; // �^�C�}�[���Z�b�g
        }
    }
}


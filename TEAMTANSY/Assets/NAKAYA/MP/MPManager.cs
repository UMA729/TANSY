using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPManager : MonoBehaviour
{
    public Image mpBar; // MP�o�[��Image
    public float maxMP = 100f; // �ő�MP
    public float currentMP; // ���݂�MP
    public float recoveryRate = 5f; // �񕜑��x

    void Start()
    {
        currentMP = maxMP; // ����MP���ő�ɐݒ�
        UpdateMPBar(); // MP�o�[���X�V
    }

    void Update()
    {
        // MP�̉�
        if (currentMP < maxMP)
        {
            currentMP += recoveryRate * Time.deltaTime;
            currentMP = Mathf.Min(currentMP, maxMP); // �ő�MP�𒴂��Ȃ��悤��
            UpdateMPBar();
        }
    }

    public void Attack()
    {
        float attackCost = 20f; // �U���ɕK�v��MP
        if (currentMP >= attackCost)
        {
            currentMP -= attackCost; // MP������
            UpdateMPBar();
            // �U���̏����������ɒǉ�
        }
        else
        {
            Debug.Log("MP���s�����Ă��܂��I");
        }
    }

    private void UpdateMPBar()
    {
        mpBar.fillAmount = currentMP / maxMP; // MP�o�[�̕\�����X�V
    }
}
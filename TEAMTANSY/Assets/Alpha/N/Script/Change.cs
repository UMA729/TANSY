using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;      //�V�[���̐؂�ւ��ɕK�v

public class Change : MonoBehaviour
{
    // �{�^�����N���b�N���ꂽ�Ƃ��ɃV�[����ύX
    public void LoadSceneByButton()
    {
        SceneManager.LoadScene("stage1");
    }
}
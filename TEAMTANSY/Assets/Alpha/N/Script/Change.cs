using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;      //�V�[���̐؂�ւ��ɕK�v

public class Change : MonoBehaviour
{
    public string sceneName;
    float time = 0.0f;
    bool nextscene = false;

    // �{�^�����N���b�N���ꂽ�Ƃ��ɃV�[����ύX
    private void Update()
    {
        if(nextscene)
        LoadScene();
    }
    public void LoadScene()
    {
        float NSdur = 0.1f;

        time += Time.deltaTime;
        //�^�C�����v���ϐ��̒l�𒴂���Ǝ��̃V�[����
        if (time >= NSdur)
        {
            SceneManager.LoadScene(sceneName);
            Time.timeScale = 1;

            time = 0.0f;
            nextscene = false;
        }
    }
    // �{�^�����N���b�N���ꂽ�Ƃ�
    public void ClickButton()
    {
        //nextscene��false�łȂ��ꍇtrue�ɂ���
        if(!nextscene)
        nextscene = true;
    }
}
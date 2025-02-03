using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;      //�V�[���̐؂�ւ��ɕK�v

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    public GameObject restart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(restart.activeSelf)
        {
            //R�L�[���������Ƃ��Ɍ��݂̃V�[���ɖ߂�
            if (Input.GetKeyDown(KeyCode.R))
            {


                if (ItemKeeper.hasDoorKey == 1)
                    ItemKeeper.hasDoorKey -= 1;

                if (ItemKeeper.hasMagicBook == 1)
                    ItemKeeper.hasMagicBook -= 1;

                Time.timeScale = 1;


                SceneManager.LoadScene(sceneName);//�V�[���̖��O������
            }
        }
    }

    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}

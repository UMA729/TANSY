using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;      //シーンの切り替えに必要

public class Change : MonoBehaviour
{
    public string sceneName;
    // ボタンがクリックされたときにシーンを変更
    public void LoadSceneByButton()
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
}
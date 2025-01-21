using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;      //シーンの切り替えに必要

public class Change : MonoBehaviour
{
    public string sceneName;
    float time = 0.0f;
    bool nextscene = false;

    // ボタンがクリックされたときにシーンを変更
    private void Update()
    {
        if(nextscene)
        LoadScene();
    }
    public void LoadScene()
    {
        float NSdur = 0.1f;

        Debug.Log("入っています");

        time += Time.deltaTime;

        Debug.Log(time);
        //タイムが計測変数の値を超えると次のシーンへ
        if (time >= NSdur)
        {
            SceneManager.LoadScene(sceneName);
            Time.timeScale = 1;

            time = 0.0f;
            nextscene = false;
        }
    }

    public void PauseClickbyTitle()
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    // ボタンがクリックされたとき
    public void ClickButton()
    {
        //nextsceneがfalseでない場合trueにする
        if(!nextscene)
        nextscene = true;
    }
}
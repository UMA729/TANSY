using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;      //シーンの切り替えに必要

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //シーン読み込み
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}

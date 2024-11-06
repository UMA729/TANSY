using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;      //シーンの切り替えに必要

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
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }

    //シーン読み込み
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

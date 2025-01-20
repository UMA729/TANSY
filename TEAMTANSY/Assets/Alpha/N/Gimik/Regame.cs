using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Regame : MonoBehaviour
{
    public GameObject regame;
    // Start is called before the first frame update
    void Start()
    {
        regame.SetActive(false);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadSceneByButton()
    {
        regame.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}

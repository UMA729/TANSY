using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{
    public GameObject pause;
    public GameObject Resumepause;

    private Button btm;
    // Start is called before the first frame update
    void Start()
    {
        pause.SetActive(false);
        Resumepause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void LoadSceneByButton()
    {
        pause.SetActive(true);
        Time.timeScale = 0;
       
    }
    
}

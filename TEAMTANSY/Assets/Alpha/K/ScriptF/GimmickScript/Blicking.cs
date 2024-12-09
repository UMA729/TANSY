using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blicking : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject arrow;
    float Duration = 1f;
    float time = 0f;
    bool arrowTrue = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= Duration)
        {
            arrow.SetActive(false);
            time = 0;
            arrowTrue = true;
        }
        if(arrowTrue)
        {
            arrow.SetActive(true);
            arrowTrue = false;
        }
    }
}
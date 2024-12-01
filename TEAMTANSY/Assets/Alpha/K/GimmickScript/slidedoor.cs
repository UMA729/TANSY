using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class slidedoor : MonoBehaviour
{
    public float openrange = 3;
    public float openspeed = 1;
    private bool openTF = false;
    Vector3 doorPos;
    // Start is called before the first frame update
    void Start()
    {
        doorPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
            
        }
    }
}

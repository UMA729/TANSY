using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDelete : MonoBehaviour
{
    public GameObject obj;
    public float deleteTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(obj, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject,deleteTime);
        }
    }
}

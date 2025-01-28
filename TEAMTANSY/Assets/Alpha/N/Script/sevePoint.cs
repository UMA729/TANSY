using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sevePoint : MonoBehaviour
{
    public static Vector3 PlayerStartPoint;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStartPoint = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("A");
            PlayerStartPoint = this.transform.position;
        }
    }
}

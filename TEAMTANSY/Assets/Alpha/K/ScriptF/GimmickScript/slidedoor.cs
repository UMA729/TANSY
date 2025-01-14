using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class slidedoor : MonoBehaviour
{
    public float openrange = 3;
    public float openspeed = 1;
    [HideInInspector] public bool openTF = false;
    Vector3 doorPos;
    // Start is called before the first frame update
    void Start()
    {
        doorPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (openTF)
        {
            
                Debug.Log("i");
                Vector3 opendoor = doorPos;

                opendoor.y += openrange;

                transform.position = Vector3.MoveTowards(transform.position, opendoor, openspeed * Time.deltaTime);

            if (transform.position == opendoor)
            {
                ItemKeeper.hasDoorKey -= 1;
            }          
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           if (ItemKeeper.hasDoorKey == 1)
            {
                openTF = true;
            }
        }
    }
}

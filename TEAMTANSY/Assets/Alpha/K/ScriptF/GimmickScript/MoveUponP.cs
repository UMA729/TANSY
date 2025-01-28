using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUponP : MonoBehaviour
{
    public Vector3 floor_pos;
    public float Distance;

    bool isUP;

    float Up_Count = 0;

    float Movespeed = 0;
    // Start is called before the first frame update
    void Start()
    {
        Movespeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Up_Count);
        UponFloor();
    }

    void UponFloor()
    {
        if (isUP && Up_Count <= 200)
        {
            if(floor_pos.y <= 0)
            floor_pos.y += Distance;

            Up_Count++;
        }
        else if (!isUP && Up_Count > 0)
        {
            floor_pos.y -= Distance;

            Up_Count--;
            if (Up_Count == 0)
            {
                Movespeed = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, floor_pos, Movespeed * Time.deltaTime);
      
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isUP = true;
            Movespeed = 2;
        }
            
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isUP = false;
    }
}

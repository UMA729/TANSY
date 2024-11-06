using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignBord : MonoBehaviour
{

    public GameObject signmess;
    // Start is called before the first frame update
    void Start()
    {
        signmess.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            signmess.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Playret"))
        {
            signmess.SetActive(false);
        }
    }
}

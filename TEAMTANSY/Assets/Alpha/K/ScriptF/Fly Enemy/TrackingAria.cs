using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingAria : MonoBehaviour
{
    private FlyingEnemyController FEC;
    // Start is called before the first frame update
    void Start()
    {
        FEC = FindObjectOfType<FlyingEnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FEC.isChasing = true;
        }
    }

    
}

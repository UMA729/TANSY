using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedWalls : MonoBehaviour
{
    [SerializeField] GameObject Prefab;
    // Start is called before the first frame update
    void Start()
    {
        Prefab.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D Prefab)
    {

    }
}

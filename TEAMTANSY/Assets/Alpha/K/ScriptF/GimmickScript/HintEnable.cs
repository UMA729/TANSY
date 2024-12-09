using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintEnable : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemKeeper.hasDoorKey > 0)
        {
            Destroy(gameObject);
        }
    }
}

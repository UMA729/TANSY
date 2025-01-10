using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public GameObject Efc;
    public bool EF_FoT;
    float Timea = 0;
    SpriteRenderer SRr;
    // Start is called before the first frame update
    void Start()
    {
        SRr = GetComponent<SpriteRenderer>();
        SRr.enabled = false;
    }
    private void Update()
    {
        if (EF_FoT == true)
        {
            EffectSwitch();
        }

    }
    void EffectSwitch()
    {
        Timea += Time.deltaTime;
        if (Timea >= 2)
        {
            Timea = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleOnly : MonoBehaviour
{
    PlayerRopeSwing PRS;
    private void Start()
    {
        PRS = GetComponent<PlayerRopeSwing>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GrappleSorR();
        }
    }

    private void GrappleSorR()
    {
        if(!PRS.lineRenderer.enabled)
        {
            PRS.ExtendRope();
        }
        else if(PRS.isSwinging)
        {
            PRS.ReleaseRope();
        }
    }
}

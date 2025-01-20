using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onButtonS : MonoBehaviour
{
    public AudioClip OnButtonS;

    private void OnPointEnter()
    {
        AudioSource.PlayClipAtPoint(OnButtonS, transform.position);
    }
}

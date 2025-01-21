using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onButtonS : MonoBehaviour
{
    public AudioClip OnButtonSound;

    public void OnPointEnter()
    {
        AudioSource.PlayClipAtPoint(OnButtonSound, transform.position);
    }
}

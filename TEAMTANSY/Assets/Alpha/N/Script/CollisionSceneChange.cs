using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionSceneChange : MonoBehaviour
{
    private savetrigger SaveTrigger;

    private void Start()
    {
        SaveTrigger = FindObjectOfType<savetrigger>();

    }
   
}

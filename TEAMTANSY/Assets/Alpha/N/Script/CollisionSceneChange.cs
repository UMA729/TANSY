using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionSceneChange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // "NextScene"�Ƃ������O�̃V�[���ɑJ��
            SceneManager.LoadScene("stage2");
        }
    }
   
}

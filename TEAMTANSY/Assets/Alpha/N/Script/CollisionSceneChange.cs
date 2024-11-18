using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionSceneChange : MonoBehaviour
{
    public string sceneName;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // "NextScene"‚Æ‚¢‚¤–¼‘O‚ÌƒV[ƒ“‚É‘JˆÚ
            SceneManager.LoadScene(sceneName);
        }
    }
   
}

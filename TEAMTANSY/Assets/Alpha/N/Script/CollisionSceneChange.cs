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
            // プレイヤーの位置を保存
            Vector3 playerPosition = transform.position;
            PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
            PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
            PlayerPrefs.SetFloat("PlayerPosZ", playerPosition.z);
            // "NextScene"という名前のシーンに遷移
            SceneManager.LoadScene(sceneName);
            //カーソル表示
            Cursor.visible = true;
        }
    }
   
}

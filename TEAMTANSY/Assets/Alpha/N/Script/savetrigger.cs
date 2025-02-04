using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class savetrigger : MonoBehaviour
{
    private List<Vector2> savedPosition = new List<Vector2>();
    public PlayerController playerController;
    public GameObject restart;
    public Transform player;

    private void Update()
    {
        for (int i = 0; i < savedPosition.Count; i++)
        {
            Debug.Log("あああ");
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("押してる");
                //シーンがリセットされた後に位置設定
                StartCoroutine(WaitForSceneReload());

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            SavePlayerPosition();
        }
    }

    void SavePlayerPosition()
    {
        savedPosition.Add(transform.position);

        if(savedPosition.Count > 1)
        {
            savedPosition.RemoveAt(0);
        }

        Debug.Log("Player Position Saved: " + transform.position);
    }

    public Vector2 GetSavePosition(int index)
    {
        if(index >= 0 && index < savedPosition.Count)
        {
            return savedPosition[index];
        }
        else
        {
            Debug.Log("Invalid index: " + index);
            return Vector2.zero;
        }
    }

    private System.Collections.IEnumerator WaitForSceneReload()
    {
        // シーンが完全に再読み込みされるまで少し待つ
        yield return new WaitForSeconds(0.1f);

        // シーン再読み込み後、保存した位置に移動
        player.position = savedPosition[0];
        Debug.Log("位置がロードされました: " + transform.position);
    }
        public void ClearHistory()
    {
        savedPosition.Clear();
    }
}

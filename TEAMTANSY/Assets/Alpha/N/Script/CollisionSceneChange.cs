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
            // �v���C���[�̈ʒu��ۑ�
            Vector3 playerPosition = transform.position;
            PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
            PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
            PlayerPrefs.SetFloat("PlayerPosZ", playerPosition.z);
            // "NextScene"�Ƃ������O�̃V�[���ɑJ��
            SceneManager.LoadScene(sceneName);
            //�J�[�\���\��
            Cursor.visible = true;
        }
    }
   
}

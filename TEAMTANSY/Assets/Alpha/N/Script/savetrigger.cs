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
            Debug.Log("������");
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("�����Ă�");
                //�V�[�������Z�b�g���ꂽ��Ɉʒu�ݒ�
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
        // �V�[�������S�ɍēǂݍ��݂����܂ŏ����҂�
        yield return new WaitForSeconds(0.1f);

        // �V�[���ēǂݍ��݌�A�ۑ������ʒu�Ɉړ�
        player.position = savedPosition[0];
        Debug.Log("�ʒu�����[�h����܂���: " + transform.position);
    }
        public void ClearHistory()
    {
        savedPosition.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class savetrigger : MonoBehaviour
{
    private List<Vector2> savedPosition = new List<Vector2>();
    private PlayerController playerController;
    private GameManager GameManager;

    public GameObject mainImage;
    public Sprite gameOverSpr;//�Q�[���I�[�o�[�e�L�X�g
    public Image gameOverPanel;//�t�F�[�h�p�p�l��
    public GameObject panel;//���C���̃p�l��
    public GameObject restarttext;//���X�^�[�g�p�̃e�L�X�g
    public GameObject restart; //
    public Transform player; //player�̈ʒu


    public static string gameState = "playing";

    void Start()
    {
        panel.SetActive(false);
        Cursor.visible = false;
    }

    private void Update()
    {
        for (int i = 0; i < savedPosition.Count; i++)
        {
            Debug.Log("������");
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("�����Ă�");
                //�v���C���[�����蔻�������
                GetComponent<CapsuleCollider2D>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
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
        yield return new WaitForSeconds(0.5f);

        PlayerController.gameState = "playing";

        // �V�[���ēǂݍ��݌�A�ۑ������ʒu�Ɉړ�
        player.position = savedPosition[0];
        Debug.Log("�ʒu�����[�h����܂���: " + transform.position);
    }
        public void ClearHistory()
    {
        savedPosition.Clear();
    }
}

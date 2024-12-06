using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    public GameObject Panelfade;//�t�F�[�h�p�l���̎擾
    Image fadealpha;//�t�F�[�h�p�l���̃C���[�W�擾�ϐ�
    private float alpha;//�p�l����alpha�l�擾
    private bool fadeout;//�t�F�[�h�A�E�g�̃t���O�ϐ�
    public string sceneName;//�ړ���̃V�[����


    // Start is called before the first frame update
    void Start()
    {
        fadealpha = Panelfade.GetComponent<Image>();
        alpha = fadealpha.color.a;
        fadeout = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (fadeout == true)
            {
                Fadeout();
                Debug.Log("�t�F�[�h�A�[�[�[�[�[�[�E�g");
            }
        }
    }

    void Fadeout()
    {
        Debug.Log("�A���������܂��[�[�[�[��");
        alpha += 0.001f;
        fadealpha.color = new Color(0, 0, 0, alpha);
        if(alpha >= 1)
        {
            fadeout = false;
            Debug.Log("�ړ�����[�[��");
            //SceneManager.LoadScene(sceneName);
        }
        
    }
}

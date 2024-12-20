using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    public Sprite gameOverSpr;//�Q�[���I�[�o�[�e�L�X�g
    public Image gameOverPanel;//�t�F�[�h�p�p�l��
    public GameObject panel;//���C���̃p�l��
    public GameObject restarttext;//���X�^�[�g�p�̃e�L�X�g
    
    Image titleImage;

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip meGameOver;    //�Q�[���I�[�o�[


    // Start is called before the first frame update
    void Start()
    {
        //�摜�̔�\��
        Invoke("InactiveImage", 1.5f);
        //�{�^��(�p�l��)���\���ɂ���
        gameOverPanel.color = new Color(0, 0, 0, 0);
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.gameState == "gameover")
        {
            //�Q�[���I�[�o�[
            mainImage.SetActive(true);
            panel.SetActive(true);
            GameStop();//�Q�[����~
            PlayerController.gameState = "gameend";

            //+++ �T�E���h�Đ��ǉ� +++
            //�T�E���h�Đ�
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //�Q�[���I�[�o�[
                mainImage.SetActive(true);
                panel.SetActive(true);
                //BGM��~
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(meGameOver);
            }
        }
        else if(HPController.gameState == "gameover")
        {
            //�Q�[���I�[�o�[
            GameStop();//�Q�[����~
            Debug.Log("���񂾁`");
            //RESTART�{�^��
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            PlayerController.gameState = "gameend";
            Debug.Log(" ̩");
        }

        else if(PlayerController.gameState == "playing")
        {
            //�Q�[����
        }
    }
    //�摜��\��
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }


    public void GameStop()
    {
        //�Q�[����ʂ��Â�����p�l����\��
        gameOverPanel.color = new Color(0, 0, 0, 255);
        mainImage.SetActive(true);
        panel.SetActive(true);
        Debug.Log("�^���Â�!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;
    public GameObject panel;
    public GameObject restartButton;
    
    Image titleImage;

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip meGameOver;    //�Q�[���I�[�o�[


    // Start is called before the first frame update
    void Start()
    {
        //�摜�̔�\��
        Invoke("InactiveImage", 3.0f);
        //�{�^��(�p�l��)���\���ɂ���
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.gameState == "gameclear")
        {
            //�Q�[���N���A
            mainImage.SetActive(true);
            panel.SetActive(true);
            //RESTART�{�^��
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            PlayerController.gameState = "gameend";
        }
        else if(PlayerController.gameState == "gameover")
        {
            //�Q�[���I�[�o�[
            mainImage.SetActive(true);
            panel.SetActive(true);
            //RESTART�{�^��
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            PlayerController.gameState = "gameend";

            //+++ �T�E���h�Đ��ǉ� +++
            //�T�E���h�Đ�
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM��~
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(meGameOver);
            }
        }
        else if(PlayerController.gameState == "palying")
        {
            //�Q�[����
        }
    }
    //�摜��\��
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}

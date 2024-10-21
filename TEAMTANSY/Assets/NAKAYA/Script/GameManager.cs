using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI���g���̂ɕK�v

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;    //�摜������GameObject
    public Sprite gameOverSpr;      //GAME OVER�摜
    public Sprite gameClearSpr;     //GAME CLEAR�摜
    public GameObject panel;        //�p�l��
    public GameObject restartButton;    //RESTART�{�^��
    public GameObject nextButton; //�l�N�X�g�{�^��

    Image titleImage;
    // Start is called before the first frame update
    void Start()
    {
        //�摜���\���ɂ���
        Invoke("InactiveImage", 1.0f);
        //�{�^��(�p�l��)���\���ɂ���
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameState == "gameclear")
        {
            //�Q�[���N���A
            mainImage.SetActive(true);
            panel.SetActive(true);
            //
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            PlayerController.gameState = "gameend";
        }
        else if(PlayerController.gameState == "gameover")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);
            //next�{�^���𖳌��ɂ���
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;   // �摜��\��
            PlayerController.gameState = "gameend";
        }
        else if (PlayerController.gameState == "Playing")
        {
            //�Q�[����
        }

    }
    //�摜���\������
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}

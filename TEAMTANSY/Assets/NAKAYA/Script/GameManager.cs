using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;

    public GameObject restartButton;
    

    Image titleImage;

    // Start is called before the first frame update
    void Start()
    {
        //�摜�̔�\��
        Invoke("InactiveImage", 1.0f);
        //�{�^��(�p�l��)���\���ɂ���

    }

    // Update is called once per frame
    void Update()
    {
        if(player.gameState == "gameclear")
        {
            //�Q�[���N���A
            mainImage.SetActive(true);
 
            //RESTART�{�^��
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            player.gameState = "gameend";
        }
        else if(player.gameState == "gameover")
        {
            //�Q�[���I�[�o�[
            mainImage.SetActive(true);
          
            //NEXT�{�^����\��
 
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            player.gameState = "gameend";
        }
        else if(player.gameState == "palying")
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
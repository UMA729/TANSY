using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UIを使うのに必要

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;    //画像を持つGameObject
    public Sprite gameOverSpr;      //GAME OVER画像
    public Sprite gameClearSpr;     //GAME CLEAR画像
    public GameObject panel;        //パネル
    public GameObject restartButton;    //RESTARTボタン
    public GameObject nextButton; //ネクストボタン

    Image titleImage;
    // Start is called before the first frame update
    void Start()
    {
        //画像を非表示にする
        Invoke("InactiveImage", 1.0f);
        //ボタン(パネル)を非表示にする
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameState == "gameclear")
        {
            //ゲームクリア
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
            //nextボタンを無効にする
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;   // 画像を表示
            PlayerController.gameState = "gameend";
        }
        else if (PlayerController.gameState == "Playing")
        {
            //ゲーム中
        }

    }
    //画像を非表示する
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    public Sprite gameOverSpr;//ゲームオーバーテキスト
    public Image gameOverPanel;//フェード用パネル
    public GameObject panel;//メインのパネル
    public GameObject restarttext;//リスタート用のテキスト
    
    Image titleImage;

    //+++ サウンド再生追加 +++
    public AudioClip meGameOver;    //ゲームオーバー


    // Start is called before the first frame update
    void Start()
    {
        //画像の非表示
        Invoke("InactiveImage", 1.5f);
        //ボタン(パネル)を非表示にする
        gameOverPanel.color = new Color(0, 0, 0, 0);
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.gameState == "gameover")
        {
            //ゲームオーバー
            mainImage.SetActive(true);
            panel.SetActive(true);
            GameStop();//ゲーム停止
            PlayerController.gameState = "gameend";

            //+++ サウンド再生追加 +++
            //サウンド再生
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //ゲームオーバー
                mainImage.SetActive(true);
                panel.SetActive(true);
                //BGM停止
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(meGameOver);
            }
        }
        else if(HPController.gameState == "gameover")
        {
            //ゲームオーバー
            GameStop();//ゲーム停止
            Debug.Log("死んだ～");
            //RESTARTボタン
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            PlayerController.gameState = "gameend";
            Debug.Log(" ﾌｩ");
        }

        else if(PlayerController.gameState == "playing")
        {
            //ゲーム中
        }
    }
    //画像非表示
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }


    public void GameStop()
    {
        //ゲーム画面を暗くするパネルを表示
        gameOverPanel.color = new Color(0, 0, 0, 255);
        mainImage.SetActive(true);
        panel.SetActive(true);
        Debug.Log("真っ暗に!");
    }
}

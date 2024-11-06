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

    //+++ サウンド再生追加 +++
    public AudioClip meGameOver;    //ゲームオーバー


    // Start is called before the first frame update
    void Start()
    {
        //画像の非表示
        Invoke("InactiveImage", 1.0f);
        //ボタン(パネル)を非表示にする

    }

    // Update is called once per frame
    void Update()
    {
        if(player.gameState == "gameclear")
        {
            //ゲームクリア
            mainImage.SetActive(true);
        }
        else if(player.gameState == "gameover")
        {
            //ゲームオーバー
            mainImage.SetActive(true);

            //RESTARTボタン
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            player.gameState = "gameend";

            //+++ サウンド再生追加 +++
            //サウンド再生
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM停止
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(meGameOver);
            }
        }
        else if(player.gameState == "palying")
        {
            //ゲーム中
        }
    }
    //画像非表示
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}

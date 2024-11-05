using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    public Image mainItemImage;        // 中央のアイテム画像
    public Image previousItemImage;    // 前のアイテム画像
    public Image nextItemImage;        // 次のアイテム画像
    public Image ganModeImage;　　　　　//がんモードのイメージ
    public List<Sprite> itemSpritesMagic = new List<Sprite>(); // アイテム画像のリスト
    public List<Sprite> itemSpritesOp = new List<Sprite>();
    public List<string> magicName;
    public List<string> optionName;
    
    private int modechangeIndex;
    private PlayerRopeSwing PRS;
    private BulletComtller BCN;
    private WindBullet BCW;
    private int currentIndex = 0;
    private int currentIndexMode = 0;
    private bool isGanMagic = false;
    private bool isGanOp = false;

    void Start()
    {
        ganModeImage.sprite = itemSpritesOp[modechangeIndex];

        PRS = FindObjectOfType<PlayerRopeSwing>(); // PRSClassのインスタンスを取得
        BCN = FindObjectOfType<BulletComtller>();
        BCW = FindObjectOfType<WindBullet>();

        isGanMagic = true;         //isGanMagicのフラグをおろす
        isGanOp = false;            //isGanOpのフラグをおろす
        UpdateItemImages();
    }

    void Update()
    {
        // Eキーで右、Qキーで左にアイテムを切り替える
        if (Input.GetKeyDown(KeyCode.E))
        {
            MoveNext();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            MovePrevious();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Wキーが押されたので銃モードの切り替えに移ります");
            GanModeChoose();
        }
        else if(Input.GetMouseButtonDown(0))
        {
            UseSelectedItem();
        }
    }

    void MoveNext()
    {
        if (isGanMagic)
        {
            currentIndex = (currentIndex + 1) % itemSpritesMagic.Count;
        }
        else if (isGanOp)
        {
            currentIndex = (currentIndex + 1) % itemSpritesOp.Count;
        }
        UpdateItemImages();
    }

    void MovePrevious()
    {
        if (isGanMagic)
        {
            currentIndex = (currentIndex - 1 + itemSpritesMagic.Count) % itemSpritesMagic.Count;
        }
        else if (isGanOp)
        {
            currentIndex = (currentIndex - 1 + itemSpritesOp.Count) % itemSpritesOp.Count;
        }
        UpdateItemImages();
    }

    void UpdateItemImages()
    {
        Debug.Log(currentIndex);
        if (isGanMagic)
        {
            // 中央のアイテムを更新
            mainItemImage.sprite = itemSpritesMagic[currentIndex];

            // 前のアイテムのインデックスを計算して更新
            int previousIndex = (currentIndex - 1 + itemSpritesMagic.Count) % itemSpritesMagic.Count;
            previousItemImage.sprite = itemSpritesMagic[previousIndex];

            // 次のアイテムのインデックスを計算して更新
            int nextIndex = (currentIndex + 1) % itemSpritesMagic.Count;
            nextItemImage.sprite = itemSpritesMagic[nextIndex];
        }
        else if(isGanOp)
        {
            // 中央のアイテムを更新
            mainItemImage.sprite = itemSpritesOp[currentIndex];

            // 前のアイテムのインデックスを計算して更新
            int previousIndex = (currentIndex - 1 + itemSpritesOp.Count) % itemSpritesOp.Count;
            previousItemImage.sprite = itemSpritesOp[previousIndex];

            // 次のアイテムのインデックスを計算して更新
            int nextIndex = (currentIndex + 1) % itemSpritesOp.Count;
            nextItemImage.sprite = itemSpritesOp[nextIndex];
        }
    }
    void GanModeChoose()
    {
        if (isGanOp)
        {
            currentIndexMode = (currentIndexMode - 1) % itemSpritesOp.Count;

            Debug.Log("魔法弾に切り替えます");

            modechangeIndex = currentIndexMode % itemSpritesOp.Count;
            ganModeImage.sprite = itemSpritesOp[modechangeIndex];
            Debug.Log(modechangeIndex);

            isGanOp    = false;
            isGanMagic = true;
        }
        else if (isGanMagic)
        {
            currentIndexMode = (currentIndexMode + 1) % itemSpritesMagic.Count;

            Debug.Log("銃機能に切り替えます");

            modechangeIndex = (currentIndexMode -1 + itemSpritesMagic.Count) % itemSpritesMagic.Count;
            ganModeImage.sprite = itemSpritesMagic[modechangeIndex];
            Debug.Log(modechangeIndex);

            isGanMagic = false;
            isGanOp    = true;
        }
        currentIndex = 0;
        UpdateItemImages();
    }
    public void UseSelectedItem()
    {
        Debug.Log(currentIndex);

        string selectMode;

        if (isGanMagic)
        {
            selectMode = magicName[currentIndex];
            switch (selectMode)
            {
                case "通弾丸":
                    Debug.Log("通常弾丸が選ばれました。");
                    if (Time.time >= BCN.nextFireTime)
                    {
                        BCN.LaunchBall();
                        BCN.nextFireTime = Time.time + 1f / BCN.fireRate; // クールタイムを設定


                        //+++ サウンド再生追加 +++
                        //サウンド再生
                        AudioSource soundPlayer = GetComponent<AudioSource>();
                        if (soundPlayer != null)
                        {
                            //BGM停止
                            soundPlayer.Stop();
                            soundPlayer.PlayOneShot(BCN.meShoot);
                        }
                    }
                    break;
                case "弾丸風":
                    if (Time.time >= BCW.nextFireTime) // 右クリック
                    {
                        BCW.LaunchBall();
                        BCW.nextFireTime = Time.time + 5f / BCW.fireRate; // クールタイムを設定


                        //+++ サウンド再生追加 +++
                        //サウンド再生
                        AudioSource soundPlayer = GetComponent<AudioSource>();
                        if (soundPlayer != null)
                        {
                            //BGM停止
                            soundPlayer.Stop();
                            soundPlayer.PlayOneShot(BCW.Bullet);
                        }
                    }
                    break;
                case "弾丸火":
                    break;
            }
        }

        if (isGanOp)
        {
            selectMode = optionName[currentIndex];

            switch (selectMode)
            {
                case "グラップル":
                    Debug.Log("グラップルがセレクトされました");
                    Debug.Log("lineRendererの状態: " + PRS.lineRenderer.enabled);

                    if (!PRS.lineRenderer.enabled)
                    {
                        PRS.ExtendRope();
                    }
                    break;
                case "ライト":
                    break;
            }
        }
        if (PRS.isSwinging)
        {
            PRS.ReleaseRope();
        }
    }
}
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
    public static string selectMode;

    private int modechangeIndex;
    private PlayerRopeSwing PRS;
    private BulletComtller BCN;
    private WindBullet BCW;
    private PortalGun PG;
    private fireBullet FB;
    private MPController mp;
    private HPController hp;
    private int currentIndexMode = 0;
    private bool isGanMagic = false;
    private bool isGanOp = false;
    [HideInInspector]
    public int currentIndex = 0;
    void Start()
    {
        ganModeImage.sprite = itemSpritesOp[modechangeIndex];

        PRS = FindObjectOfType<PlayerRopeSwing>(); // PRSClassのインスタンスを取得
        BCN = FindObjectOfType<BulletComtller>();
        BCW = FindObjectOfType<WindBullet>();
        PG = FindObjectOfType<PortalGun>();
        mp = FindObjectOfType<MPController>();
        hp = FindObjectOfType<HPController>();
        FB = FindObjectOfType<fireBullet>();

        isGanMagic = true;         //isGanMagicのフラグをおろす
        isGanOp = false;            //isGanOpのフラグをおろす
        UpdateItemImages();
    }

    void Update()
    {
        // Eキーで右、Qキーで左にアイテムを切り替える
        if (Input.GetKeyDown(KeyCode.E) && hp.Deth == false)
        {
            MoveNext();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && hp.Deth == false)
        {
            MovePrevious();
        }
        else if (Input.GetKeyDown(KeyCode.W) && hp.Deth == false)
        {
            Debug.Log("Wキーが押されたので銃モードの切り替えに移ります");
            GanModeChoose();
        }
        else if(Input.GetMouseButtonDown(0) && hp.Deth == false)
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
            currentIndexMode = (currentIndexMode) % itemSpritesOp.Count;

            Debug.Log("魔法弾に切り替えます");
            Debug.Log(currentIndexMode);
            modechangeIndex = currentIndexMode % itemSpritesOp.Count;
            Debug.Log(modechangeIndex);
            ganModeImage.sprite = itemSpritesOp[modechangeIndex];

            isGanOp    = false;
            isGanMagic = true;
        }
        else if (isGanMagic)
        {
            currentIndexMode = (currentIndexMode) % itemSpritesMagic.Count;

            Debug.Log("銃機能に切り替えます");

            modechangeIndex = currentIndexMode % itemSpritesMagic.Count;
            Debug.Log(modechangeIndex);
            ganModeImage.sprite = itemSpritesMagic[modechangeIndex];

            isGanMagic = false;
            isGanOp    = true;
        }
        currentIndex = 0;
        UpdateItemImages();
    }
    public void UseSelectedItem()
    {
        Debug.Log(currentIndex);

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
                    Debug.Log("風が選択されました");
                    if (mp.Mp >= 0 && Time.time >= BCW.nextFireTime) 
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
                    Debug.Log("火が選択されました。");
                    if (mp.Mp >= 10 && Time.time >= FB.nextFireTime)
                    {
                        FB.LaunchBall();
                        FB.nextFireTime = Time.time + 5f / FB.fireRate; // クールタイムを設定


                        //+++ サウンド再生追加 +++
                        //サウンド再生
                        AudioSource soundPlayer = GetComponent<AudioSource>();
                        if (soundPlayer != null)
                        {
                            //BGM停止
                            soundPlayer.Stop();
                            soundPlayer.PlayOneShot(FB.meShoot);
                        }
                    }
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
                case "ポータル":
                    PG.CreatePortal();
                    break;
            }
        }
        if (PRS.isSwinging)
        {
            PRS.ReleaseRope();
        }
    }
    public void ObtainMagicItem(Sprite newMagicSprite, string newMagicName)
    {
        Debug.Log("本を取得しました");
        // 新しいアイテムをリストに追加
        itemSpritesMagic.Add(newMagicSprite);
        magicName.Add(newMagicName);

        // 現在選択中のアイテムが新しく追加されたアイテムになるように設定
        currentIndex = itemSpritesMagic.Count - 1;
        
        if (isGanOp)
        {
            isGanOp = false;
            isGanMagic = true;
            ganModeImage.sprite = itemSpritesOp[modechangeIndex]; //銃機能をモードチェンジイメージ
        }
        if (isGanMagic)
        {
            selectMode = magicName[currentIndex];
        }

        Debug.Log($"{selectMode}");

        // UIを更新
        UpdateItemImages();

        Debug.Log($"新しい魔法アイテム '{newMagicName}' を取得しました。現在のアイテム数: {itemSpritesMagic.Count}");
    }
}
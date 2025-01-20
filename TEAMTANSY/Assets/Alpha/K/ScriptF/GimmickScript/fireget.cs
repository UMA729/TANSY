using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireget : MonoBehaviour
{
    private bool isInCol  = false;      // コライダー範囲内にいるかどうか
    public float Duration = 5f;        // たいまつが復活するまでの時間(目的)
    public float revSec   = 0f;          // たいまつが復活するまでの時間(計測)
    public float animeSec = 0f;        // アニメーションが終わるまでの時間
    private fireBullet   FB;             // 火弾丸スクリプト変数
    private ItemSelector IS;           // アイテム選択スクリプト変数

    Animator animator;                                    //たいまつアニメーター変数
    public string torchtrue  = "torch Animation";         //通常時アニメーション
    public string torchfalse = "torchfalse Animation";    //火弾丸でバフ取得時アニメーション
    public string Torchrevive = "torchrevive Animation";  //復活アニメーション
    public bool torchCharge = true;                       //火弾丸でしている状態かどうかのフラグ
    public GameObject torchBord;                          //たいまつ説明オブジェクト

    private string nowAnime = "";　　  //現在アニメーション
    private string oldAnime = "";      //前のアニメーション

    private void Awake()
    {
        Application.targetFrameRate = 60; //フレームレートを60fpsに制限
    }

    void Start()
    {
        torchBord.SetActive(false);             //たいまつ説明obj初期化
        animator = GetComponent<Animator>();    //アニメーター取得
        FB = FindAnyObjectByType<fireBullet>(); //fireBulletClassのインスタンスを取得
        IS = FindObjectOfType<ItemSelector>();  //ItemSelectorClassのインスタンスを取得
        nowAnime = torchtrue;                   
        oldAnime = torchtrue;                   
    }
    void Update()
    {
        // 範囲内で右クリックされた場合
        if (isInCol && Input.GetMouseButtonDown(1) && // 1は右クリック
            FB.fireBaff == false &&                   // 火バフ未取得時
            IS.currentIndex == 1)                     // 選択弾丸が火弾丸の時
        {
            //火バフ取得時
            FireGet();            
            if (torchBord == true)
            {
                Destroy(torchBord, 1f); //トーチの説明オブジェクトを消す
            }
        }
        //たいまつ状態がバフ取得不可の時
        if (torchCharge == false)
        {
            //現在アニメーションが復活アニメーションではない場合
            if (nowAnime != Torchrevive)
            {                           
                nowAnime = torchfalse;
            }

            revSec += Time.deltaTime; //たいまつ復活時間測定開始
            if (revSec >= Duration)
            {
                nowAnime = Torchrevive;//復活アニメーションを現在アニメーションに
                torchCharge = true;   //たいまつを火弾丸で再取得可能に
                revSec = 0f;        //たいまつ復活時間を再度初期化
            }
        }
        //現在アニメーションが変わった場合
        if (nowAnime != oldAnime)    
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime); //現在のアニメーションを再生
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); //アニメーションの現状を変数

        // アニメーションが終了しているか確認
        if (stateInfo.IsName("torchrevive Animation") && stateInfo.normalizedTime >= 1.0f)
        {
            Debug.Log("アニメーションが終了しました");
            // アニメーション終了後の処理
            nowAnime = torchtrue;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // タグがプレイヤーなら
        {
            isInCol = true;                                       //コライダーの中
            //魔法本を持っている状態
            if(torchBord != null && ItemKeeper.hasMagicBook == 1)
            torchBord.SetActive(true);//たいまつ説明オブジェを見えるように                           
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // タグがプレイヤーなら
        {
            isInCol = false;           //コライダー外

            //たいまつ説明オブジェクトがある場合
            if(torchBord != null)
            torchBord.SetActive(false);//説明オブジェを見えないように
        }
    }
    public void FireGet()
    {
        if (FB.fireBaff == false)
        {
            BaffIcon BI;                          //スクリプト変数
            BI = FindObjectOfType<BaffIcon>();    //BaffIconスクリプトを取得

            BI.Set_BuffandKey_Icon(false, 1);
        }
        FB.fireBaff = true;                       //火バフ取得
        torchCharge = false;                       //たいまつでバフを取得をできない状態に
    }
}
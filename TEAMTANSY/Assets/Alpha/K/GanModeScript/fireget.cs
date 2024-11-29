using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireget : MonoBehaviour
{
    public LayerMask interactableLayer; // 対象となるレイヤーマスク

    private bool isInRange = false;    // 範囲内にいるかどうか
    public float duration = 5f;       // 持続時間（秒）
    public float animeDuration = 5f;       // 持続時間（秒）
    public float reviveSec = 0f;
    public float animeSec = 0f;
    private fireBullet FB;
    private ItemSelector IS;

    Animator animator;
    public string torchtrue  = "torch Animation";
    public string torchfalse = "torchfalse Animation";
    public string torchrevive = "torchrevive Animation";

    string nowAnime = "";
    string oldAnime = "";

    private void Awake()
    {
        Application.targetFrameRate = 60; //
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        FB = FindAnyObjectByType<fireBullet>();
        IS = FindObjectOfType<ItemSelector>();
        nowAnime = torchtrue;
        oldAnime = torchtrue;
    }
    void Update()
    {
        // 範囲内で右クリックされた場合
        if (isInRange && Input.GetMouseButtonDown(1) && FB.fireBaff == false && IS.currentIndex == 1) // 1は右クリック
        {
            PerformRaycast();
        }
        //トーチのアニメーション
        if (FB.torchCharge == true)
        {
            if (nowAnime != torchrevive)
            {
                nowAnime = torchfalse;
            }

            reviveSec += Time.deltaTime;
            if (reviveSec >= duration)
            {
                nowAnime = torchrevive;
                FB.torchCharge = false;
                reviveSec = 0f;
            }
        }
        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // アニメーションが終了しているか確認
        if (stateInfo.IsName("torchrevive Animation") && stateInfo.normalizedTime >= 1.0f)
        {
            Debug.Log("アニメーションが終了しました");
            // アニメーション終了後の処理
            nowAnime = torchtrue;
        }
    }


    private void PerformRaycast()
    {
        FB.Active();
    }

   

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // タグがプレイヤーなら
        {
            Debug.Log("範囲に入りました");
            isInRange = true;
            Debug.Log($"isInRangeは{isInRange}");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // タグがプレイヤーなら
        {
            Debug.Log("範囲からでました");
            isInRange = false;
            Debug.Log($"isInRangeは{isInRange}");
        }
    }
}
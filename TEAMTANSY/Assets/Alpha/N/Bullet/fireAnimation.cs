using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireAnimation : MonoBehaviour
{
    Animator animator;
    string NfireBullet = "fire Animation";
    string GfireBullet = "FGfireBullet Animation";
    string nowAnime = "";
    string oldAnime = "";

    private fireBullet FB;

    // Start is called before the first frame update
    void Start()
    {
        FB = FindObjectOfType<fireBullet>();
        nowAnime = "fire Animation";
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //アニメーション
        if (FB.fireBaff == true)
        {
            nowAnime = GfireBullet;
        }
        else if (FB.fireBaff == false)
        {
            nowAnime = NfireBullet;
        }
        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
    }
}

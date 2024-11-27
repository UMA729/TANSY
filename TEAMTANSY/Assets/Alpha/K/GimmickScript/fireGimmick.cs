using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireGimmick : MonoBehaviour
{
    Animator animator;
    private string fireAnime = "fireGimmick Animation";
    private string stopAnime = "fireGimmckstop Animation";
    string nowAnime = "";
    string oldAnime = "";
    private fireBullet FB;

    // Start is called before the first frame update
    void Start()
    {
        FB = FindObjectOfType<fireBullet>();
        nowAnime = stopAnime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oldAnime != nowAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // アニメーションが終了しているか確認
        if (nowAnime == fireAnime)
        {
            if (stateInfo.IsName("fireGimmick Animation") && stateInfo.normalizedTime >= 1.0f)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FireBullet") && FB.fireBaff == true)
        {
            nowAnime = fireAnime;

            FB.fireBaff = false;
              
        }
    }
}

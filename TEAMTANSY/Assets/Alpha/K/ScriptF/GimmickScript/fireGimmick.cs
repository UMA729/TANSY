using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireGimmick : MonoBehaviour
{
    public AudioClip TBursound; //いばらが燃えたときの効果音
    public AudioClip NBursound; //いばらが燃えなかったときの効果音 
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
            //是焼却効果音
            AudioSource.PlayClipAtPoint(TBursound, transform.position);

            nowAnime = fireAnime;

            FB.fireBaff = false;
              
        }
        else if (collision.gameObject.CompareTag("FireBullet") && FB.fireBaff == false)
        {
            //否焼却効果音
            AudioSource.PlayClipAtPoint(NBursound, transform.position);
        }
    }
}

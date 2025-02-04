using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickButton : MonoBehaviour
{
    [HideInInspector] public bool gimmickceiling = false;
    public GameObject Floor;
    public GameObject Boss;
    public AudioClip Bsound;
    Animator animator; 
    public string StopAnime = "Stop";
    public string PushAnime = "gimmickbutton Animation";
    string nowAnime = "";
    string oldAnime = "";


    // Start is called before the first frame update
    void Start()
    {
        //Boss.SetActive(false);
        Floor.SetActive(false);
        animator = GetComponent<Animator>();
        nowAnime = StopAnime;
        oldAnime = StopAnime;
    }

    // Update is called once per frame
    void Update()
    {
        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" ||   //
            collision.gameObject.tag == "Player" ||   //���̃^�O�ɐG���ƃ{�^�����쓮
            collision.gameObject.tag == "FireBullet") //
        {
            if (gimmickceiling)
            {
                AudioSource.PlayClipAtPoint(Bsound, transform.position);
            }
            nowAnime = PushAnime;
            gimmickceiling = true;

            Floor.SetActive(true);
            //Boss.SetActive(true);
        }
    }
}

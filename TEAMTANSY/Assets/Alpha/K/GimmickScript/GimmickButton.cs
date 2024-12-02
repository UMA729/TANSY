using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickButton : MonoBehaviour
{
    [HideInInspector] public bool gimmickceiling = false;
    Animator animator;
    public string StopAnime = "Stop";
    public string PushAnime = "gimmickbutton Animation";
    string nowAnime = "";
    string oldAnime = "";


    // Start is called before the first frame update
    void Start()
    {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("a");
            nowAnime = PushAnime;
            gimmickceiling = true;
        }
        else if (collision.gameObject.tag == "Player")
        {
            nowAnime = PushAnime;
            gimmickceiling = true;
        }
    }
}

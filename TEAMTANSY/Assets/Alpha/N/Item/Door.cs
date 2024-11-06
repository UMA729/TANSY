using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int arrangeId = 0;   //配置の識別に使う
    public bool IsGoldDoor = false;   //ドア

    //アニメーション
    Animator animator;
    public string stopAnime = "door";
    public string moveAnime = "door1";

    string nowAnime = "";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
         nowAnime = stopAnime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //鍵を持っている
            if (IsGoldDoor)
            {
                if (ItemKeeper.hasDoorKey > 0)
                {
                    ItemKeeper.hasDoorKey--;
                    GetComponent<BoxCollider2D>().enabled = false;

                    animator.Play(moveAnime);
                }
            }

        }
    }
}

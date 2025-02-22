using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickApple : MonoBehaviour
{
    public float Lenght = 0.0f;     //自動落下検知距離
    public bool isDelete = false;   //落下後の削除
    public GameObject deadObj;      //死亡フラグ

    bool isFell = false;            //落下フラグ
    float fadeTime = 0.5f;          //フェードアウト時間

    //+++ サウンド再生追加 +++
    public AudioClip GA;    //銃放つ
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player =
            GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            float d = Vector2.Distance(
                transform.position, player.transform.position);
            if(Lenght >= d)
            {
                Rigidbody2D rbody = GetComponent<Rigidbody2D>();
                if(rbody.bodyType == RigidbodyType2D.Static)
                {
                    rbody.bodyType = RigidbodyType2D.Dynamic;
                    //+++ サウンド再生追加 +++
                    //サウンド再生
                    AudioSource soundPlayer = GetComponent<AudioSource>();
                    if (soundPlayer != null)
                    {
                        //BGM停止
                        soundPlayer.Stop();
                        soundPlayer.PlayOneShot(GA);
                    }
                    Debug.Log("なりました");
                }
            }
        }
        if(isFell)
        {
            fadeTime -= Time.deltaTime;
            Color col = GetComponent<SpriteRenderer>().color;
            col.a = fadeTime;
            GetComponent<SpriteRenderer>().color = col;
            if(fadeTime <= 0.0f)
            {
                
                Debug.Log("消えましたね");
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(isDelete)
        {
            isFell = true;
        }

        if (collision.gameObject.CompareTag("Graund"))
        {
            Debug.Log("colliderが消えたはずです!今です!シンジ君");
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, Lenght);   
    }
}

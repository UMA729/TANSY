using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickApple : MonoBehaviour
{
    public float Lenght = 0.0f;     //�����������m����
    public bool isDelete = false;   //������̍폜
    public GameObject deadObj;      //���S�t���O

    bool isFell = false;            //�����t���O
    float fadeTime = 0.5f;          //�t�F�[�h�A�E�g����

    //+++ �T�E���h�Đ��ǉ� +++
    public AudioClip GA;    //�e����
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
                    //+++ �T�E���h�Đ��ǉ� +++
                    //�T�E���h�Đ�
                    AudioSource soundPlayer = GetComponent<AudioSource>();
                    if (soundPlayer != null)
                    {
                        //BGM��~
                        soundPlayer.Stop();
                        soundPlayer.PlayOneShot(GA);
                    }
                    Debug.Log("�Ȃ�܂���");
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
                
                Debug.Log("�����܂�����");
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
            Debug.Log("collider���������͂��ł�!���ł�!�V���W�N");
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, Lenght);   
    }
}

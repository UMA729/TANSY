using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxS : MonoBehaviour
{
    public AudioClip IBsound; //�󔠌��ʉ�
    ItemBox IB;               //�󔠃X�N���v�g

    // Start is called before the first frame update
    void Start()
    {
        IB = FindObjectOfType<ItemBox>(); //�C���X�^���X��
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && IB.isClosed)
        {
            AudioSource.PlayClipAtPoint(IBsound, transform.position); //������
        }
    }
}

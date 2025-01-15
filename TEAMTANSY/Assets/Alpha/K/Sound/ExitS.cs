using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitS : MonoBehaviour
{
    public AudioClip Esound;    //�o�����J����
    slidedoor SD;               //�o���̃X�N���v�g

    // Start is called before the first frame update
    void Start()
    {
        SD = FindObjectOfType<slidedoor>();//�C���X�^���X��
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !SD.openTF)
        {
            AudioSource.PlayClipAtPoint(Esound, transform.position); //������
        }
    }
}

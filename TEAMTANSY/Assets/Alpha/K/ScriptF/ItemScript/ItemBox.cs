using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public Sprite      openImage; //�J�����摜
    public GameObject itemPrefab; //�o�Ă���A�C�e���̃v���n�u
    public int     arrangeId = 0; //�z�u�̎��ʂɎg��
    public GameObject    gettext; //�A�C�e������e�L�X�g
    public GameObject ExplaTorch; //�����܂̐����`���[�g���A���I�u�W�F�N�g
    public AudioClip IBsound;
    [HideInInspector]public bool isClosed = true; //true= ���܂��Ă���false= �J���Ă���
    // Start is called before the first frame update
    void Start()
    {
        if(gettext != null)
        gettext.SetActive(false);
        if(ExplaTorch != null)
        ExplaTorch.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isClosed && collision.gameObject.tag == "Player")
        {
            //�������܂��Ă����ԂŃv���C���[�ɐڐG
            GetComponent<SpriteRenderer>().sprite = openImage;
            isClosed = false; //�J���Ă����Ԃɂ���
            if (itemPrefab != null)
            {
                //�A�C�e�����v���n�u������
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
                //�A�C�e�����莞�̌��ʉ�
                AudioSource.PlayClipAtPoint(IBsound, transform.position);
            }
            GetComponent<PolygonCollider2D>().enabled = false;
            if (gettext != null)   //�A�C�e������e�L�X�g�̃I�u�W�F�N�g�������
            {
                gettext.SetActive(true);//�\��
                Destroy(gettext, 1f);   //�Z�b��ɍ폜

            }
            if (ExplaTorch != null)//�����܂����I�u�W�F�N�g�������
            {
                ExplaTorch.SetActive(true);//�\��
            }
        }
    }
}

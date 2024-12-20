using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public Sprite openImage; //�J�����摜
    public GameObject itemPrefab; //�o�Ă���A�C�e���̃v���n�u
    public bool isClosed = true; //true= ���܂��Ă���false= �J���Ă���
    public int arrangeId = 0;    //�z�u�̎��ʂɎg��
    public GameObject gettext1;
    public GameObject gettext2;
    public GameObject ExplaTorch;
    private HPController HP;
    // Start is called before the first frame update
    void Start()
    {
        gettext1.SetActive(false);
        gettext2.SetActive(false);
        ExplaTorch.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isClosed&&collision.gameObject.tag == "Player")
        {
            //�������܂��Ă����ԂŃv���C���[�ɐڐG
            GetComponent<SpriteRenderer>().sprite = openImage;
            isClosed = false; //�J���Ă����Ԃɂ���
            if(itemPrefab !=null)
            {
                //�A�C�e�����v���n�u������
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
            gettext1.SetActive(true);
            gettext2.SetActive(true);
            Destroy(gettext1, 1f);
            ExplaTorch.SetActive(true);
        }
    }
}

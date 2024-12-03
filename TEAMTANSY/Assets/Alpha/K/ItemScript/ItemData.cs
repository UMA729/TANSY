using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�A�C�e���̎��
public enum ItemType
{
    DoorKey,            //���̌�
    MagicBook,          //���@��
    HealBullet,         //�񕜒e
}
public class ItemData : MonoBehaviour
{

    public ItemType type;
    public int count = 1;
    public int arrangeId = 0;
    public List<Sprite> newMagic = new List<Sprite>();
    public List<string> newMagicname = new List<string> { "�e�ۉ�" };

    private ItemSelector IS;
    private bool isObtained = false; // �A�C�e���擾�ς݃t���O
    // Start is called before the first frame update
    void Start()
    {
        IS = FindAnyObjectByType<ItemSelector>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //�ڐG
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isObtained) return; // ���łɎ擾����Ă���Ώ������X�L�b�v

        if (collision.gameObject.tag == "Player")
        {
            isObtained = true;
            if (type == ItemType.DoorKey)
            {
                //���̌�
                ItemKeeper.hasDoorKey += count;
            }
            else if (type == ItemType.MagicBook)
            {
                ////���@���@��
                //ItemKeeper.hasMagicBook += count;
                //// �V�����A�C�e���̉摜�Ɩ��O��n���ă��X�g�ɒǉ�
                //Debug.Log(ItemKeeper.hasMagicBook);
                //if (ItemKeeper.hasMagicBook == 1)
                //{
                //    Debug.Log("iji");
                 IS.ObtainMagicItem(newMagic[0], newMagicname[0]);
                //}
            }
            //�A�C�e���擾���o
            //�����蔻�������
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            //�A�C�e����Rigidbody2D���Ƃ��Ă���
            Rigidbody2D itemBody = GetComponent<Rigidbody2D>();
            //�d�͂�߂�
            itemBody.gravityScale = 2.5f;
            //��ɏ������ˏグ�鉉�o
            itemBody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
            //0.5�b��ɍ폜
            Destroy(gameObject, 0.5f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アイテムの種類
public enum ItemType
{
    DoorKey,            //扉の鍵
    MagicBook,          //魔法所
    HealBullet,         //回復弾
}
public class ItemData : MonoBehaviour
{

    public ItemType type;
    public int count = 1;
    public int arrangeId = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //接触
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (type == ItemType.DoorKey)
            {
                //扉の鍵
                ItemKeeper.hasDoorKey += count;
            }
            else if (type == ItemType.MagicBook)
            {
                //魔法魔法書
                ItemKeeper.hasMagicBook += count;
            }
            //アイテム取得演出
            //あたり判定を消す
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            //アイテムのRigidbody2Dをとってくる
            Rigidbody2D itemBody = GetComponent<Rigidbody2D>();
            //重力を戻す
            itemBody.gravityScale = 2.5f;
            //上に少し跳ね上げる演出
            itemBody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
            //0.5秒後に削除
            Destroy(gameObject, 0.5f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public Sprite      openImage; //開いた画像
    public GameObject itemPrefab; //出てくるアイテムのプレハブ
    public int     arrangeId = 0; //配置の識別に使う
    public GameObject    gettext; //アイテム入手テキスト
    public GameObject ExplaTorch; //たいまつの説明チュートリアルオブジェクト
    [HideInInspector]public bool isClosed = true; //true= しまっているfalse= 開いている
    // Start is called before the first frame update
    void Start()
    {
        gettext.SetActive(false);
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
            //箱が締まっている状態でプレイヤーに接触
            GetComponent<SpriteRenderer>().sprite = openImage;
            isClosed = false; //開いている状態にする
            if (itemPrefab != null)
            {
                //アイテムをプレハブから作る
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
            GetComponent<PolygonCollider2D>().enabled = false;
            if (gettext != null)//アイテム入手テキストのオブジェクトがあれば
            {
                gettext.SetActive(true);//表示
                Destroy(gettext, 1f);   //〇秒後に削除

            }
            if (ExplaTorch != null)//たいまつ説明オブジェクトがあれば
            {
                ExplaTorch.SetActive(true);//表示
            }
        }
    }
}

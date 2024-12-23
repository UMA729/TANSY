using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public Sprite openImage; //開いた画像
    public GameObject itemPrefab; //出てくるアイテムのプレハブ
    public bool isClosed = true; //true= しまっているfalse= 開いている
    public int arrangeId = 0;    //配置の識別に使う
    public GameObject gettext;
    public GameObject ExplaTorch;
    private HPController HP;
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
        if(isClosed&&collision.gameObject.tag == "Player")
        {
            //箱が締まっている状態でプレイヤーに接触
            GetComponent<SpriteRenderer>().sprite = openImage;
            isClosed = false; //開いている状態にする
            if(itemPrefab !=null)
            {
                //アイテムをプレハブから作る
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
            gettext.SetActive(true);
            Destroy(gettext, 1f);
            ExplaTorch.SetActive(true);
        }
    }
}

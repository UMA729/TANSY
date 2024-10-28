using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float leftLimit = 0.0f;//左スクロール
    public float rightLimit = 0.0f;//右スクロール
    public float topLimit = 0.0f;//上スクロール
    public float bottomLimit = 0.0f;//下スクロール

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player =
            GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            //カメラ更新座標
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = transform.position.y;
            //横同期
            //両端に移動制限を付ける
            if(x < leftLimit)
            {
                x = leftLimit;
            }
            else if(x > rightLimit)
            {
                x = rightLimit;
            }
            //縦同期
            //上下に移動制限を付ける
            if(y < bottomLimit)
            {
                y = bottomLimit;
            }
            else if(y > topLimit)
            {
                y = topLimit;
            }
            //カメラ位置のVector3を作る
            Vector3 v3 = new Vector3(x,y,z);
            transform.position = v3;
        }
    }
}

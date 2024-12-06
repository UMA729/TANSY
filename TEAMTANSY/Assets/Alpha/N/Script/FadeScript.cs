using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    public GameObject Panelfade;//フェードパネルの取得
    Image fadealpha;//フェードパネルのイメージ取得変数
    private float alpha;//パネルのalpha値取得
    private bool fadeout;//フェードアウトのフラグ変数
    public string sceneName;//移動先のシーン名

    private SceneMovement SM;

    // Start is called before the first frame update
    void Start()
    {
        fadealpha = Panelfade.GetComponent<Image>();
        alpha = fadealpha.color.a;
        fadeout = true;
        SM = GameObject.Find("Canvas").GetComponent<SceneMovement>();
    }

    void Fadeout()
    {
        Debug.Log("アムロいきまぁーーーーす");
        alpha += 0.001f;
        fadealpha.color = new Color(0, 0, 0, alpha);
        if(alpha >= 1)
        {
            fadeout = false;
            Debug.Log("移動じゃーーい");
            SceneManager.LoadScene(sceneName);
        }
        
    }
}

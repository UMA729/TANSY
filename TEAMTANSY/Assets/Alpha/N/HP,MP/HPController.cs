using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UIを使うときに書きます。

public class HPController : MonoBehaviour
{
    //最大HPと現在のHP。
    int maxHp = 100;
    int Hp;
    private PlayerController player;
    public int recoveryAmout = 10;  //1回の回復量
    public float recoveryInterval = 10f;    //回復の間隔
    public int consumptionAmount = 10;
    public bool lighthit = false;
    public bool Deth = false;
    public float damageRevive = 5;
    public float duration = 0;


    Animator animator; // アニメーション
    public string deadAnime = "PlayerOver";
    public static string gameState = "playing"; // ゲームの状態
    //Slider
    public Slider slider;

    //+++ サウンド再生追加 +++
    public AudioClip meHP;    //銃放つ

    void Start()
    {
        slider.interactable = false;
        //Sliderを最大にする。
        slider.value = 100;
        //HPを最大HPと同じ値に。
        Hp = maxHp;
        if(Deth == false)
        StartCoroutine(RecoverHP());    //最初の回復時間を設定

        animator = GetComponent<Animator>();        //　Animator を取ってくる
        gameState = "playing";  // ゲーム中にする
    }

    private void Update()
    {
        if (lighthit == true)
        {
            damageRevive += Time.deltaTime;

            if (damageRevive >= duration)
            {
                damageRevive = 0f;
                lighthit = false;
            }
        }
        if (Hp <= 0)
        {
            Debug.Log("kieta");
            animator.Play(deadAnime);

            PlayerController.gameState = "gameover";
            gameState = "gameover";
            Deth = true;
            GameStop();
            //======================
            //ゲーム演出
            //=======================
            //プレイヤー当たりを消す
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            Deth = true;
            // オブジェクトを破壊する
            //Destroy(transform.root.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Enemyタグを設定しているオブジェクトに接触したとき
        if (collision.gameObject.tag == "Enemy")
        {
            //HPから1を引く
            Hp = Hp - 10;

            //HPをSliderに反映。
            slider.value = (float)Hp;
        }

        if(collision.gameObject.tag == "Boss")
        {
            //HPから1を引く
            Hp = Hp - 5;

            //HPをSliderに反映。
            slider.value = (float)Hp;
        }
        if (collision.gameObject.tag == "needle")
        {
            Hp = Hp - 50;

            slider.value = (float)Hp;
        }

        if (Hp <= 30)
        {
            //+++ サウンド再生追加 +++
            //サウンド再生
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM停止
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(meHP);
            }
            Debug.Log("なりました");
        }

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //雷ダメージ
        if (other.gameObject.tag == "lightning" && !lighthit)
        {
            Debug.Log("ataltutq");
            //HPから1を引く
            Hp = Hp - 30;
            lighthit = true;

            //HPをSliderに反映。
            slider.value = (float)Hp;
        }

        //ドラゴンの雷ダメージ
        if (other.gameObject.tag == "thunder")
        {
            Debug.Log("ataltutq");
            //HPから1を引く
            Hp = Hp - 5;

            //HPをSliderに反映。
            slider.value = (float)Hp;
        }

        //ボスの技を受けたとき
        if(other.gameObject.tag == "waza")
        {
            Debug.Log("技を食らった");
            //hpが減る
            Hp = Hp - 20;
            //hpをSliderに反映
            slider.value = (float)Hp;
        }

        //if (Hp <= 0)
        //{
        //    Debug.Log("kieta");
        //    animator.Play(deadAnime);

        //    gameState = "gameover";
        //    player.GameStop();
        //    //======================
        //    //ゲーム演出
        //    //=======================
        //    //プレイヤー当たりを消す
        //    GetComponent<CapsuleCollider2D>().enabled = false;
        //    GetComponent<BoxCollider2D>().enabled = false;
        //    Deth = true;
        //    // オブジェクトを破壊する
        //    //Destroy(transform.root.gameObject);
           
        //}
    }


    private IEnumerator RecoverHP()
    {
        while (true)    //無限ループで回復を続ける
        {
            yield return new WaitForSeconds(recoveryInterval);  //指定した時間を待つ

            if (Hp < maxHp)
            {
                Hp += recoveryAmout;
                if (Hp > maxHp)
                {
                    Hp = maxHp;
                }
                //HPをSliderに反映。
                slider.value = (float)Hp;
                Debug.Log("Hp:" + Hp);
            }
        }
    }
    void GameStop()
    {
        //Rigidbody2Dを取ってくる
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        //速度を0にして強制停止
        rbody.velocity = new Vector2(0, 0);
    }
}


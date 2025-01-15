using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornS : MonoBehaviour
{
    public AudioClip Tsound;        //Ç¢ÇŒÇÁèƒãpâπ

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FireBullet")
        {
            AudioSource.PlayClipAtPoint(Tsound, transform.position);//ÇªÇÃà íuÇ≈âπÇ™ñ¬ÇÈ
        }
    }
}

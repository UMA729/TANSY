using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCommtller : MonoBehaviour
{
    public float speed = 0.0f;
    public float jump = 0.0f;
    public GameObject bullet;

    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(attack());
    }
    IEnumerator attack()
    {
        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("Player"));
        if (player != null)
        {
            float d = Vector2.Distance(
               transform.position, player.transform.position);
            while(true)
            {
                float y = Random.Range(-4.5f, 4.5f);
                
            }

        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}

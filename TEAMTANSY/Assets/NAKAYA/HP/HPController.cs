using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    public static int hp = 100;
    public static int mp = 100;
    public static string gamestate;


    // Start is called before the first frame update
    void Start()
    {
        gamestate = "playing";
    }

    // Update is called once per frame
    void Update()
    {
        //ƒQ[ƒ€’†‚ÆƒQ[ƒ€ˆÈŠO‚Å‚Í‰½‚à‚µ‚È‚¢
        if(gamestate != "playing")
        {
            return;
        }
    }

 
}

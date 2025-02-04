using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sevePoint : MonoBehaviour
{

    private const string PositionXKey = "PositininX";
    private const string PositionYKey = "PositininY";
    private const string PositionZKey = "PositininZ";

    
    public static void SavePlayerPosition(Transform playertransform)
    {
        PlayerPrefs.SetFloat(PositionXKey, playertransform.position.x);
        PlayerPrefs.SetFloat(PositionYKey, playertransform.position.y);
        PlayerPrefs.SetFloat(PositionZKey, playertransform.position.z);
        PlayerPrefs.Save();
    }
   
    public static Vector3 LoadPlayerPosition()
    {
        float x = PlayerPrefs.GetFloat(PositionXKey, 0f);
        float y = PlayerPrefs.GetFloat(PositionYKey, 0f);
        float z = PlayerPrefs.GetFloat(PositionZKey, 0f);

        return new Vector3(x, y, z);
    }


    public static bool HasSaveDate()
    {
        //データの保持ができているかどうかの確認
        return PlayerPrefs.HasKey(PositionXKey);
    }


}

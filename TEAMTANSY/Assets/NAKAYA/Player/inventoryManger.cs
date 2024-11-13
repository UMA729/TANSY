using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryManger : MonoBehaviour
{
    public GameObject inventoryPanel;
    private bool isInventoryOpen = false;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        //持ち物UIが開いているかどうか確認
        isInventoryOpen = !isInventoryOpen;
        //持ち物UIの表示/非表示を切り替え
        inventoryPanel.SetActive(isInventoryOpen);

        //ゲームの一時停止と再開
        if(isInventoryOpen)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}

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
        //������UI���J���Ă��邩�ǂ����m�F
        isInventoryOpen = !isInventoryOpen;
        //������UI�̕\��/��\����؂�ւ�
        inventoryPanel.SetActive(isInventoryOpen);

        //�Q�[���̈ꎞ��~�ƍĊJ
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

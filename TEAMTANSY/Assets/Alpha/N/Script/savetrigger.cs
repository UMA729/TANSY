using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savetrigger : MonoBehaviour
{
    //�G�ꂽ��v���C���[�̍��W�ʒu���Z�[�u����
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            sevePoint.SavePlayerPosition(other.transform);
            Debug.Log("game Saved");
        }
    }

}

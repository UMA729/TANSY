using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaffIcon : MonoBehaviour
{
    [SerializeField]
    private List<RectTransform> BuffandKey_Icon_List = new List<RectTransform>();
    public RectTransform BuffandKey_Icon_Panel;
    public RectTransform BuffandKey_Icon_base;
    public Sprite[] Icon_BuffandKey = new Sprite[2];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Set_BuffandKey_Icon(bool Itemcon,int ID)
    {
        //�A�C�R�����p�l���̎q�ɂ���
        var icon = Instantiate(BuffandKey_Icon_base, BuffandKey_Icon_Panel);
        
        //�A�C�R���摜������
        if (Itemcon)
        {
            Debug.Log("��");
            icon.GetComponent<Image>().sprite = Icon_BuffandKey[ID];
        }
        else
        {
            Debug.Log("�t�@�C���[");
            icon.GetComponent<Image>().sprite = Icon_BuffandKey[ID];
        }

        BuffandKey_Icon_List.Add(icon);

    }
    public void Remove_BuffandKey_Icon(bool Itemcon, int ID)
    {
        foreach (RectTransform icon in BuffandKey_Icon_List)
        {
            if (Itemcon)
            {
                if (icon.GetComponent<Image>().sprite == Icon_BuffandKey[ID])
                {
                    Destroy(icon.gameObject);

                    BuffandKey_Icon_List.Remove(icon);
                    break;
                }
            }
            else
            {
                if (icon.GetComponent<Image>().sprite == Icon_BuffandKey[ID])
                {
                    Destroy(icon.gameObject);

                    BuffandKey_Icon_List.Remove(icon);
                    break;
                }
            }
        }
    }
}

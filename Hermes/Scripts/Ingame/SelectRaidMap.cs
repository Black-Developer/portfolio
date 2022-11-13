using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectRaidMap : MonoBehaviour
{
    public GameObject RaidMap;
    public Image im;
    void Start()
    {
        RaidMap = null;
    }
    void Update()
    {

    }
    public void DivideRaidMapName()
    {
        if (!RaidMap) return;

        if(RaidMap.name == "BTN_CastleMap")
        {
            GameObject.Find("Canvas_ Raid").GetComponent<UIChanger>().SelectRaidMap("S_Castle");
        }
        else if (RaidMap.name == "BTN_ForestMap")
        {
            GameObject.Find("Canvas_ Raid").GetComponent<UIChanger>().SelectRaidMap("S_ForestMap");
        }
    }

    public void selectRaidMap(GameObject go)
    {
        TwinkleSprite(go);
        ChangeInfo(go);
        RegistThisRaidMap(go);
    }
    private void TwinkleSprite(GameObject go) //������ ���� ��� ��¦�̰�
    {
        im = go.GetComponent<Image>();
        //�����̰ų� ����Ʈ �߰� �ϰų�~
    }
    private void ChangeInfo(GameObject go)
    {
        if(RaidMap)
        {
            for (int i = 0; i < RaidMap.transform.childCount; i++)
            {
                RaidMap.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < go.transform.childCount; i++)
        {
            go.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    private void RegistThisRaidMap(GameObject go)
    {
        RaidMap = go;
    }

}

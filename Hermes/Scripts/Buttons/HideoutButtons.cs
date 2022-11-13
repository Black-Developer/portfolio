using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideoutButtons : MonoBehaviour
{
    [Header("버튼 리스트")]
    public GameObject Btn_Stash;
    public GameObject Btn_Character;
    public GameObject Btn_Equipment;

    [Header("패널 리스트")]
    public GameObject Panel_Stash;
    public GameObject Panel_Character;
    public GameObject Panel_CharacterInfo;
    public GameObject Panel_Equipment;

    public void On_Click_Btn_Stash()
    {
        Panel_Stash.SetActive(true);
        Panel_Character.SetActive(false);
        Panel_Equipment.SetActive(false);
        Panel_CharacterInfo.SetActive(false);

        Btn_Stash.GetComponent<Image>().color = Color.gray;
        Btn_Character.GetComponent<Image>().color = Color.white;
        Btn_Equipment.GetComponent<Image>().color = Color.white;


    }
    public void On_Click_Btn_Character()
    {
        Panel_Stash.SetActive(false);
        Panel_Character.SetActive(true);
        Panel_Equipment.SetActive(false);
        Panel_CharacterInfo.SetActive(false);

        Btn_Stash.GetComponent<Image>().color = Color.white;
        Btn_Character.GetComponent<Image>().color = Color.gray;
        Btn_Equipment.GetComponent<Image>().color = Color.white;

    }
    public void On_Click_Btn_Equipment()
    {
        Panel_Stash.SetActive(false);
        Panel_Character.SetActive(false);
        Panel_Equipment.SetActive(true);
        Panel_CharacterInfo.SetActive(false);

        Btn_Stash.GetComponent<Image>().color = Color.white;
        Btn_Character.GetComponent<Image>().color = Color.white;
        Btn_Equipment.GetComponent<Image>().color = Color.gray;

    }
}
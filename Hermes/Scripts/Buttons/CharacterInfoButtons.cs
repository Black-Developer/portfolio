using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CharacterInfoButtons : MonoBehaviour
{
    public GameObject Panel_CharacterInfo;
    public TextMeshProUGUI Text_Story_Info;
    public TextMeshProUGUI Text_Skill_Info;

    [Header("캐릭터1 정보")]
    [SerializeField,TextArea,Multiline]
    string Character1_story_Info;
    [SerializeField,TextArea,Multiline]
    string Character1_skill_Info;
    public void OnClick_Character1()
    {
        Panel_CharacterInfo.SetActive(true);
        Text_Story_Info.SetText(Character1_story_Info);
        Text_Skill_Info.SetText(Character1_skill_Info);
    }
    [Header("캐릭터2 정보")]
    [SerializeField,TextArea,Multiline]
    string Character2_story_Info;
    [SerializeField,TextArea,Multiline]
    string Character2_skill_Info;
    public void OnClick_Character2()
    {
        Panel_CharacterInfo.SetActive(true);
        Text_Story_Info.SetText(Character2_story_Info);
        Text_Skill_Info.SetText(Character2_skill_Info);
    }
}

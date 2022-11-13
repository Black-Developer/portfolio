using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCharacterManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] characterSlot;
    private void Start()
    {
        // 게임 시작 시 캐릭터 목록을 불러오는 부분
    //    characterSlot = GameObject.Find("ChooseCharacter").GetComponent<ChooseCharacter>().LoadCharacter();
    //    GameObject.Find("ChooseCharacter").GetComponent<ChooseCharacter>().DeleteThisObject();
    }
}
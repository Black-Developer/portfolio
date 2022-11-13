using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReadyToRaid : MonoBehaviour
{
    public int[] raidCharacterSlotArray = new int[8]; //내가 보유한 8개의 캐릭터 목록
    public int[] selectedCharacterArray = new int[4]; //레이드 입장하는 4명의 캐릭터 목록 (보유한 8개의 캐릭터의 슬롯넘버가 들어감)
    public int selectCharacter;
    public int selectSlotNum;

    public GameObject Characters_Select;
    public GameObject Inventory;


    private Color ImageColor;
    private Transform dummyCharacters;
    public void Start()
    {
        selectedCharacterArray = new int[4] { -1, -1, -1, -1 };
        selectCharacter = -1;
        selectSlotNum = - 1;
        ImageColor = new Color(191, 191, 191, 1);
        dummyCharacters = GameObject.Find("DummyCharacters").transform;
        //fillAllSlot();
    }

    public void startRaid() //최종 게임 시작
    {
        Managers.Data.Ingame.InitGame(selectedCharacterArray);
    }

    public void selectCharacterChoiceSlot_BTN(int selectedCount)
    {
        selectSlotNum = selectedCount;

        if(isSelectedSlot()) //이미 해당하는 자리에 캐릭터가 등록되어있으면 실행
        {
            setActiveWarningUnmountCharacterPopup();
            return;
        }
        setActiveSelectedCharacters(false);
        setActiveCharacters_Select(false);
        setActiveInventory(true);

        fillAllSlot();
        checkSelectedCharacter();
    }

    public void selectCharacter_BTN(int num)
    {
        selectCharacter = raidCharacterSlotArray[num];

        if (selectCharacter == -1) return;
        showCharacterLeft();
        showInfoCharacterMiddle();
        //오른쪽에 캐릭터와 장비 등등 띄워야함
    }



    public void selectedSlot() //해당 캐릭터를 슬롯에 저장
    {

        if (selectCharacter == -1)
        {
            setActiveSelectedCharacters(true);
            selectSlotNum = -1;
            return;
        }


        selectedCharacterArray[selectSlotNum] = selectCharacter;

        Characters_Select.transform.GetChild(selectSlotNum).GetComponent<Image>().color = new Color(255, 255, 255, 0.0f);
        Characters_Select.transform.GetChild(selectSlotNum).GetChild(1).gameObject.SetActive(true);
        Characters_Select.transform.GetChild(selectSlotNum).GetChild(0).gameObject.SetActive(false);

        CharacterSystem cs = dummyCharacters.GetChild(0).GetComponent<CharacterSystem>();
        Characters_Select.transform.GetChild(selectSlotNum).GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>().text =
            cs.equippeditem.name;

        dummyCharacters.GetChild(0).gameObject.SetActive(false);
        dummyCharacters.GetChild(selectSlotNum + 1).GetComponent<CharacterSystem>().LoadSlot(selectCharacter);

        setActiveSelectedCharacters(true);
        selectCharacter = -1;
        selectSlotNum = -1;
    }
    public void cancelWarningUnmountCharacterPopup()
    {
        selectSlotNum = -1;
    }
    public void selectWarningUnmountCharacterPopup()
    {
        Characters_Select.transform.GetChild(selectSlotNum).GetComponent<Image>().color = new Color(255, 255, 255, 1.0f);
        Characters_Select.transform.GetChild(selectSlotNum).GetChild(1).gameObject.SetActive(false);
        Characters_Select.transform.GetChild(selectSlotNum).GetChild(0).gameObject.SetActive(true);
        dummyCharacters.GetChild(selectSlotNum + 1).gameObject.SetActive(false);
      
        selectedCharacterArray[selectSlotNum] = -1;
        selectCharacter = -1;
        selectSlotNum = -1;
    }

    public void CheckEmptyHeroSlot() //선택 캐릭터가 총 0명인데 레이드를 진행하는것을 막음
    {
        if(isEmptySlot())
        {
            PopupZoroHeroWarring(true);
            return;
        }
        changeUIBetweenCharSelectAndWorldSelect(true);
    }
    private bool isEmptySlot()
    {
        for (int i = 0; i < selectedCharacterArray.Length; i++)
        {
            if (selectedCharacterArray[i] == -1) continue;
            return false;
        }
        return true;
    }
    private void PopupZoroHeroWarring(bool isactive)
    {
        Inventory.transform.parent.GetChild(1).GetChild(6).gameObject.SetActive(isactive);
    }
    private void changeUIBetweenCharSelectAndWorldSelect(bool change)
    {
        //true is change from CharSelect to WorldSelect
        Inventory.transform.parent.parent.GetChild(0).gameObject.SetActive(!change);
        Inventory.transform.parent.parent.GetChild(1).gameObject.SetActive(change);
    }
    private void setActiveSelectedCharacters(bool isActive)
    {
        for (int i = 1; i < 5; i++)
        {
            if(selectedCharacterArray[i-1] != -1)
                dummyCharacters.GetChild(i).gameObject.SetActive(isActive);
        }
    }
    private void setActiveInventory(bool isActive)
    {
        Inventory.SetActive(isActive);
        Inventory.transform.GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
        makeBlankInfoCharacter();
    }
    private void setActiveCharacters_Select(bool isActive)
    {
        Characters_Select.SetActive(isActive);
    }
    private void setActiveWarningUnmountCharacterPopup()
    {
        Characters_Select.transform.GetChild(5).gameObject.SetActive(true);
    }

    private bool isSelectedSlot()
    {
        if (selectedCharacterArray[selectSlotNum] == -1) return false;

        return true;
    }
    private void showInfoCharacterMiddle()
    {
        CharacterSystem cs = dummyCharacters.GetChild(0).GetComponent<CharacterSystem>();

        Transform textGO = Inventory.transform.GetChild(1).GetChild(1);

        textGO.GetChild(1).GetChild(0).GetComponent<Text>().text = cs.equippeditem.name;
        textGO.GetChild(2).GetChild(0).GetComponent<Text>().text = cs.equippeditem.LoadEquippedItemNames();
        textGO.GetChild(3).GetChild(0).GetComponent<Text>().text = cs.equippeditem.LoadEquippedItemAbility();


        

    }
    private void makeBlankInfoCharacter()
    {
        Inventory.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = "캐릭터 이름";
        Inventory.transform.GetChild(1).GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        Inventory.transform.GetChild(1).GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>().text = "착용 장비 목록";
        Inventory.transform.GetChild(1).GetChild(1).GetChild(3).GetChild(0).GetComponent<Text>().text = "캐릭터 상세 능력치";
        //능력치
    }
    private void showCharacterLeft()
    {
        Inventory.transform.GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
        dummyCharacters.GetChild(0).gameObject.SetActive(true);
        dummyCharacters.GetChild(0).GetComponent<CharacterSystem>().LoadSlot(selectCharacter);
        dummyCharacters.GetChild(0).GetComponent<EquipmentSystem>().ChangeEquipment();
    }
    private void fillAllSlot()
    {
        //int i = 0;
        foreach (var character in Managers.Data.myCharacterDict)
        {
            Sprite sprite = findCharacterImage(character.Value.characterId);
            raidCharacterSlotArray[character.Value.num-1] = character.Value.num;
            fillSlot((character.Value.num-1) / 2, (character.Value.num-1) % 2, sprite, 1.0f);
            //i++;
        }
    }
    private void checkSelectedCharacter()
    {
        for (int i = 0; i < raidCharacterSlotArray.Length; i++)
        {
            checkIsExist(i);
        }
    }
    private void checkIsExist(int num)
    {
        for (int i = 0; i < selectedCharacterArray.Length; i++)
        {
            if (raidCharacterSlotArray[num] == selectedCharacterArray[i])
            {
                blockSelectedCharater(num);
                break;
            }
            else
            {
                unblockSelectedCharater(num);
            }
        }

    }

    private void unblockSelectedCharater(int num)
    {
        changeImageAlpha(num, 1.0f);
        choiceButtonActivate(num, true);
    }
    private void blockSelectedCharater(int num)
    {
        changeImageAlpha(num, 0.25f);
        choiceButtonActivate(num, false);
    }

    private void choiceButtonActivate(int num, bool isActive)
    {
        GameObject slot = findHero_slot();
        slot.transform.GetChild(0).GetChild(0).GetChild(num / 2).GetChild(num % 2).GetComponent<Button>().interactable = isActive;
    }
    private void makeBlankSlot()
    {
        GameObject slot = findHero_slot();
        for (int i = 0; i < 4; i++)
        {
            slot.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = null;
            slot.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
            slot.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(1).GetChild(0).GetComponent<Image>().sprite = null;
            slot.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(1).GetChild(0).gameObject.SetActive(false);
        }
    }
    private void fillSlot(int i,int j, Sprite sprite, float alpha)
    {
        GameObject slot = findHero_slot();
        slot.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(j).GetChild(0).GetComponent<Image>().sprite = sprite;
        slot.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(j).GetComponent<Button>().interactable = true;
        slot.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(j).GetChild(0).gameObject.SetActive(true);
    }
    private void changeImageAlpha(int num, float alpha)
    {
        GameObject slot = findHero_slot();
        slot.transform.GetChild(0).GetChild(0).GetChild(num/2).GetChild(num%2).GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, alpha);
    }
    private GameObject findHero_slot()
    {
        return GameObject.Find("Hero_slot");
    }
    private Sprite findCharacterImage(int id)
    {
        return Managers.Resource.Load<Sprite>($"Sprites/Character/{Managers.Data.CharacterStatDict[id].name}");
    }
}

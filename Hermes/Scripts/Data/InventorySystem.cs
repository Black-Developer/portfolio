using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventorySystem : MonoBehaviour
{
    public GameObject character;
    GameObject category;
    public int EquipmentSlotNum;
    public int inventorySlotNum;
    public int[] equipmentSlotDictKey = new int[6];
    //int[] InventorySlotDictKey = new int[6];

    //======================================
    //use drawCharInfo
    public GameObject mainCanvas;
    //======================================

    private void Start()
    {
        character = GameObject.Find("ModularCharacters");
        category = GameObject.Find("Category");
        CharacterSystem cs = character.GetComponent<CharacterSystem>();
        if(cs.slot == 0)
        {
            //character.SetActive(false);
        }
    }

    #region itemSystem
    public GameObject BTN_Equipment_slot;
    private void MakeBlankEquipmentItemInventory()
    {
        GameObject inventory = findEquipmentItemInventory();

        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            inventory.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            inventory.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
        }
    }
    private GameObject findEquipmentItemInventory()
    {
        return GameObject.Find("Equipment_slot");
    }
    private string findClickGameObjectName()
    {
        return EventSystem.current.currentSelectedGameObject.name;
    }
    private bool isEmptyItemDict(KeyValuePair<int,Information> item)
    {
        if (item.Value.count == 0) return true;
        return false;
    }
    private void makeEmptyInventorySlotDictKey()
    {
        for (int i = 0; i < equipmentSlotDictKey.Length; i++)
        {
            equipmentSlotDictKey[i] = 0;
        }
    }
    private void ShowItemEquipmentItemImage()
    {

    }
    private void ShowEquipmentItemCount()
    {
       
    }


    public void ClickMountingEquipmentSlot()
    {
        string clickGameObjectName = findClickGameObjectName();

    }
    public void ClickWeaponSlot()
    {
        int i = 0;
        EquipmentSlotNum = 4;
        makeEmptyInventorySlotDictKey();

        MakeBlankEquipmentItemInventory();
        Transform inventory = findEquipmentItemInventory().transform;
        foreach (var item in MyItemListContents.Instance.myWeaponItemDict)
        {
            if (isEmptyItemDict(item)) continue;

            equipmentSlotDictKey[i] = item.Value.id;

            inventory.GetChild(i).GetChild(1).gameObject.SetActive(true);
            inventory.GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text = item.Value.count + "개";

            inventory.GetChild(i).GetChild(0).gameObject.SetActive(true);
            inventory.GetChild(i++).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Weapons/{Managers.Data.WeaponStatDict[item.Value.id].name}");
        }




    }
    public void ClickHelmetSlot()
    {
        int i = 0;
        EquipmentSlotNum = 1;
        makeEmptyInventorySlotDictKey();

        MakeBlankEquipmentItemInventory();
        Transform inventory = findEquipmentItemInventory().transform;

        foreach (var item in MyItemListContents.Instance.myHelmetItemDict)
        {
            if (isEmptyItemDict(item)) continue;
            equipmentSlotDictKey[i] = item.Value.id;
            inventory.GetChild(i).GetChild(1).gameObject.SetActive(true);
            inventory.GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text = item.Value.count + "개";

            inventory.GetChild(i).GetChild(0).gameObject.SetActive(true);
            inventory.GetChild(i++).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.HelmetStatDict[item.Value.id].name}");
        }
    }
    public void ClickPantsSlot()
    {
        int i = 0;
        EquipmentSlotNum = 3;
        makeEmptyInventorySlotDictKey();
        MakeBlankEquipmentItemInventory();
        Transform inventory = findEquipmentItemInventory().transform;

        foreach (var item in MyItemListContents.Instance.myPantsItemDict)
        {
            if (isEmptyItemDict(item)) continue;
            equipmentSlotDictKey[i] = item.Value.id;
            inventory.GetChild(i).GetChild(1).gameObject.SetActive(true);
            inventory.GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text = item.Value.count + "개";

            inventory.GetChild(i).GetChild(0).gameObject.SetActive(true);
            inventory.GetChild(i++).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.PantsStatDict[item.Value.id].name}");
        }
    }
    public void ClickClothesSlot()
    {
        int i = 0;
        EquipmentSlotNum = 2;
        makeEmptyInventorySlotDictKey();
        MakeBlankEquipmentItemInventory();
        Transform inventory = findEquipmentItemInventory().transform;

        foreach (var item in MyItemListContents.Instance.myClothesItemDict)
        {
            print($"대박코드: {Managers.Data.ClothesStatDict[item.Value.id].name}");
            if (isEmptyItemDict(item)) continue;
            equipmentSlotDictKey[i] = item.Value.id;
            inventory.GetChild(i).GetChild(1).gameObject.SetActive(true);
            inventory.GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text = item.Value.count + "개";

            inventory.GetChild(i).GetChild(0).gameObject.SetActive(true);
            inventory.GetChild(i++).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.ClothesStatDict[item.Value.id].name}");
        }
    }
    public void ClickAccessarySlot()
    {
        int i = 0;
        EquipmentSlotNum = 5;
        makeEmptyInventorySlotDictKey();
        MakeBlankEquipmentItemInventory();
        Transform inventory = findEquipmentItemInventory().transform;

        foreach (var item in MyItemListContents.Instance.myAccessaryItemDict)
        {
            if (isEmptyItemDict(item)) continue;
            equipmentSlotDictKey[i] = item.Value.id;
            inventory.GetChild(i).GetChild(1).gameObject.SetActive(true);
            inventory.GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text = item.Value.count + "개";

            inventory.GetChild(i).GetChild(0).gameObject.SetActive(true);
            inventory.GetChild(i++).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.AccessaryStatDict[item.Value.id].name}");
        }
    }
    public void ClickBagSlot()
    {
        int i = 0;
        EquipmentSlotNum = 6;
        makeEmptyInventorySlotDictKey();
        MakeBlankEquipmentItemInventory();
        Transform inventory = findEquipmentItemInventory().transform;

        foreach (var item in MyItemListContents.Instance.myBagItemDict)
        {
            if (isEmptyItemDict(item)) continue;
            equipmentSlotDictKey[i] = item.Value.id;
            inventory.GetChild(i).GetChild(1).gameObject.SetActive(true);
            inventory.GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text = item.Value.count + "개";
 
            inventory.GetChild(i).GetChild(0).gameObject.SetActive(true);
            inventory.GetChild(i++).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/{Managers.Data.BagStatDict[item.Value.id].name}");
        }
    }

    public void clickInventoryslotNum()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        inventorySlotNum = clickObject.transform.GetSiblingIndex();


        Transform wearingPopup = mainCanvas.transform.GetChild(0).GetChild(2).GetChild(3).GetChild(2);

        string nameing = "";
        //text 출력을 해줘야함
        if (EquipmentSlotNum == 1) //모자
        {
            nameing = Managers.Data.HelmetStatDict[equipmentSlotDictKey[inventorySlotNum]].name;
        }
        else if (EquipmentSlotNum == 2) //상의
        {
            nameing = Managers.Data.ClothesStatDict[equipmentSlotDictKey[inventorySlotNum]].name;

        }
        else if (EquipmentSlotNum == 3) //하의
        {
            nameing = Managers.Data.PantsStatDict[equipmentSlotDictKey[inventorySlotNum]].name;

        }
        else if (EquipmentSlotNum == 4) //무기
        {
            nameing = Managers.Data.WeaponStatDict[equipmentSlotDictKey[inventorySlotNum]].name;

        }
        else if (EquipmentSlotNum == 5) //견장
        {
            nameing = Managers.Data.AccessaryStatDict[equipmentSlotDictKey[inventorySlotNum]].name;

        }
        else if (EquipmentSlotNum == 6) //가방
        {
            nameing = Managers.Data.BagStatDict[equipmentSlotDictKey[inventorySlotNum]].name;
        }

        wearingPopup.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = nameing + " 장비를 착용 하시겠습니까?";
    }
    public void ClickEquipItemToCharacter() //특정 아이템을 눌러 장착
    {
        //GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        //int num = clickObject.transform.GetSiblingIndex();


        CharacterSystem cs = character.GetComponent<CharacterSystem>();
        EquipmentSystem es = character.GetComponent<EquipmentSystem>();


        if (EquipmentSlotNum == 1)
        {
            if(cs.equippeditem.helmetId != 0)
                MyItemListContents.Instance.increaseMyItem(ItemCategory.Helmet, cs.equippeditem.helmetId, 1);
            cs.equippeditem.helmetId = equipmentSlotDictKey[inventorySlotNum];
            MyItemListContents.Instance.increaseMyItem(ItemCategory.Helmet, cs.equippeditem.helmetId, -1);
            ClickHelmetSlot();

            es.ChangeHelmet(cs.equippeditem.helmetId);
            es.ChangeEquipment();
            category.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            category.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.HelmetStatDict[cs.equippeditem.helmetId].name}");
        }
        if (EquipmentSlotNum == 2)
        {

            if (cs.equippeditem.clothesId != 12120001)
                MyItemListContents.Instance.increaseMyItem(ItemCategory.Clothes, cs.equippeditem.clothesId, 1);
            cs.equippeditem.clothesId = equipmentSlotDictKey[inventorySlotNum];
            MyItemListContents.Instance.increaseMyItem(ItemCategory.Clothes, cs.equippeditem.clothesId, -1);
            ClickClothesSlot();


            es.ChangeClothes(cs.equippeditem.clothesId);
            es.ChangeEquipment();
            category.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            category.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.ClothesStatDict[cs.equippeditem.clothesId].name}");
        }
        if (EquipmentSlotNum == 3)
        {
            if (cs.equippeditem.pantsId != 12130001)
                MyItemListContents.Instance.increaseMyItem(ItemCategory.Pants, cs.equippeditem.pantsId, 1);
            cs.equippeditem.pantsId = equipmentSlotDictKey[inventorySlotNum];
            MyItemListContents.Instance.increaseMyItem(ItemCategory.Pants, cs.equippeditem.pantsId, -1);
            ClickPantsSlot();


            es.ChangePants(cs.equippeditem.pantsId);
            es.ChangeEquipment();
            category.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            category.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.PantsStatDict[cs.equippeditem.pantsId].name}");
        }
        if (EquipmentSlotNum == 4)
        {
            if (cs.equippeditem.weaponId != 0)
                MyItemListContents.Instance.increaseMyItem(ItemCategory.Weapon, cs.equippeditem.weaponId, 1);
            cs.equippeditem.weaponId = equipmentSlotDictKey[inventorySlotNum];
            MyItemListContents.Instance.increaseMyItem(ItemCategory.Weapon, cs.equippeditem.weaponId, -1);
            ClickWeaponSlot();



            es.ChangeWeapon(cs.equippeditem.weaponId);
            es.ChangeEquipment();
            category.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
            category.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite =
               Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Weapons/{Managers.Data.WeaponStatDict[cs.equippeditem.weaponId].name}");
        }
        if (EquipmentSlotNum == 5)
        {
            if (cs.equippeditem.accessaryRId != 0)
                MyItemListContents.Instance.increaseMyItem(ItemCategory.Accessary, cs.equippeditem.accessaryRId, 1);
            cs.equippeditem.accessaryLId = equipmentSlotDictKey[inventorySlotNum] - 100;
            cs.equippeditem.accessaryRId = equipmentSlotDictKey[inventorySlotNum];
            MyItemListContents.Instance.increaseMyItem(ItemCategory.Accessary, cs.equippeditem.accessaryRId, -1);
            ClickAccessarySlot();


            es.ChangeLeftAccessary(cs.equippeditem.accessaryLId);
            es.ChangeRightAccessary(cs.equippeditem.accessaryRId);
            es.ChangeEquipment();

            category.transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
            category.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.AccessaryStatDict[cs.equippeditem.accessaryRId].name}");
        }
        if (EquipmentSlotNum == 6)
        {
            if (cs.equippeditem.bagId != 0)
                MyItemListContents.Instance.increaseMyItem(ItemCategory.Bag, cs.equippeditem.bagId, 1);
            cs.equippeditem.bagId = equipmentSlotDictKey[inventorySlotNum];
            MyItemListContents.Instance.increaseMyItem(ItemCategory.Bag, cs.equippeditem.bagId, -1);
            ClickBagSlot();


            es.ChangeBag(cs.equippeditem.bagId);
            es.ChangeEquipment();

            category.transform.GetChild(5).GetChild(0).gameObject.SetActive(true);
            category.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.BagStatDict[cs.equippeditem.bagId].name}");
        }

        DrawCharacterInfo_Guild(cs.equippeditem);
    }
    #endregion


    #region characterSystem
    public GameObject BTN_Hero_slot;

    private void MakeBlankCharacterSlot()
    {
        for (int i = 0; i < BTN_Hero_slot.transform.GetChild(2).childCount; i++)
        {
            BTN_Hero_slot.transform.GetChild(2).GetChild(i).GetChild(0).gameObject.SetActive(false);

        }
    }
    public void ClickCharaterSlot()
    {
        MakeBlankCharacterSlot();

        
        foreach (var character in Managers.Data.myCharacterDict)
        {
            BTN_Hero_slot.transform.GetChild(2).GetChild(character.Value.num - 1).GetChild(0).gameObject.SetActive(true);
            BTN_Hero_slot.transform.GetChild(2).GetChild(character.Value.num - 1).GetChild(0).GetComponent<Image>().sprite =
           Sprite.Create(Managers.Resource.Load<Texture2D>($"Sprites/Character/{Managers.Data.CharacterStatDict[character.Value.characterId].name}"), new Rect(0, 0, 500, 500), new Vector2(0.5f, 0.5f));
        }

    }


    private void makeBlankEquipmentSlot()
    {
        for (int i = 0; i < 6; i++)
        {
            category.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            category.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = null;
        }
    }
    public void selectCharacterSlot(int slotNum) //영웅 탭에서 특정 캐릭터 선택
    {
        //character.SetActive(true);

        //GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        //int num = clickObject.transform.GetSiblingIndex() + 1; // num번째 슬롯

        makeBlankEquipmentSlot();
        DrawCharacterInfo_Guild(Managers.Data.myCharacterDict[slotNum]);

        CharacterSystem cs = character.GetComponent<CharacterSystem>();

        CheckHaveSlot(cs);
        
        /*--실제 해당 아이템의 데이터가 존재하는지 유효 검사--/
        Managers.Data.HelmetStatDict.TryGetValue(Managers.Data.myCharacterDict[num].helmetId, out var helmetName);
        Managers.Data.ClothesStatDict.TryGetValue(Managers.Data.myCharacterDict[num].clothesId, out var clothesName);
        Managers.Data.PantsStatDict.TryGetValue(Managers.Data.myCharacterDict[num].pantsId, out var pantsName);
        Managers.Data.WeaponStatDict.TryGetValue(Managers.Data.myCharacterDict[num].weaponId, out var weaponName);
        Managers.Data.AccessaryStatDict.TryGetValue(Managers.Data.myCharacterDict[num].accessaryId, out var accessaryName);
        Managers.Data.BagStatDict.TryGetValue(Managers.Data.myCharacterDict[num].bagId, out var bagName);
        ---------------------------------------------------------*/
    }
    private void CheckHaveSlot(CharacterSystem cs) //아이템이 있는 슬롯 active를 true로 바꿈
    {
        if (!IsEmptyItem(cs.equippeditem.helmetId))
        {
            category.transform.GetChild(0).GetChild(0).gameObject.SetActive(true); //모자
            category.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.HelmetStatDict[cs.equippeditem.helmetId].name}"); //_Static
        }
        if (!IsEmptyItem(cs.equippeditem.clothesId))
        {
            category.transform.GetChild(1).GetChild(0).gameObject.SetActive(true); //상의
            category.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.ClothesStatDict[cs.equippeditem.clothesId].name}"); //_Static
        }
        if (!IsEmptyItem(cs.equippeditem.pantsId))
        {
            category.transform.GetChild(2).GetChild(0).gameObject.SetActive(true); //하의
            category.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.PantsStatDict[cs.equippeditem.pantsId].name}"); //_Static
        }
        if (!IsEmptyItem(cs.equippeditem.weaponId))
        {
            category.transform.GetChild(3).GetChild(0).gameObject.SetActive(true); //무기
            category.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Weapons/{Managers.Data.WeaponStatDict[cs.equippeditem.weaponId].name}");
        }
        if (!IsEmptyItem(cs.equippeditem.accessaryRId))
        {
            category.transform.GetChild(4).GetChild(0).gameObject.SetActive(true); //견장(악세사리)
            category.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.AccessaryStatDict[cs.equippeditem.accessaryRId].name}"); //_Static
        }
        if (!IsEmptyItem(cs.equippeditem.bagId))
        {
            category.transform.GetChild(5).GetChild(0).gameObject.SetActive(true); //가방
            category.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Image/Item/Equipments/{Managers.Data.BagStatDict[cs.equippeditem.bagId].name}");
        }
    }    
    private bool IsEmptyItem(int num)
    {
        return num == 0;
    }
    #endregion


    #region drawCharInfo
    public void DrawCharacterInfo_HeroDraw(EquippedItem charInfo)
    {
        Transform infoGo = mainCanvas.transform.GetChild(0).GetChild(2);
        infoGo.GetChild(0).GetChild(0).GetComponent<Text>().text = charInfo.name;
        infoGo.GetChild(1).GetChild(0).GetComponent<Text>().text = charInfo.LoadEquippedItemNames();
        infoGo.GetChild(2).GetChild(0).GetComponent<Text>().text = charInfo.LoadEquippedItemAbility();
    }
    public void DrawCharacterInfo_Guild(EquippedItem charInfo)
    {

        Transform infoGo = GameObject.Find("Canvas_Guild").transform.GetChild(0).GetChild(2).GetChild(1);
        infoGo.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = charInfo.name;

        infoGo.GetChild(1).GetChild(0).GetComponent<Text>().text = charInfo.LoadEquippedItemAbility();
    }
    #endregion
}

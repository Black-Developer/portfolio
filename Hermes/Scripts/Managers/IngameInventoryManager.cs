using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//가방안에 가방 아이템을 먹는건 아직 구현안함 -> XXXXXXXXX 안함

public class IngameInventoryManager //레이드 시에만 동작하는 휘발성 인벤토리
{
    private int[] raidCharacterArray = new int[4];
    private int[,] bagSizeArray = new int[4, 2]; // [i,0] -> full size, [i,1] -> now size
    private Dictionary<int, Information>[] inventoryArray = new Dictionary<int, Information>[4];

    public void InitGame(int[] slotArray) //레이드 시작시마다 Init으로 초기화
    {
        ResetVariable();
        SetRaidCharacterArrayFromReadyToRaidSlot(slotArray);
        CheckDefaultBagSize();
    }
    public void QuitGame() //레이드 종료시 실행 (레이드 종료후 인벤토리 확인및 타이틀 돌아가기 버튼 누른 다음 사용 예정
    {
        //인벤토리 확인 과정 (UI도 필요)
        SaveInventoryToMyItemList();
    }
    public void IncreaseItemInInventory(int charSlotNum, int id, int quantity) //흭득가능한 아이템에서 처리하는 함수
    {
        int raidSlotNum = FindRaidSlotNumFromMyCharacterNum(charSlotNum);
        ItemCategory itemCategory = ClassifyCategoryNumFromItemId(id);
        int itemSize = FindItemSize(itemCategory, id);


        if (IsOverIfSumItemSizeAndNowBagSize(raidSlotNum, itemSize, quantity)) return;

        if (IsExistItem(raidSlotNum, id))
        {
            if (IsLessThanZero(raidSlotNum, quantity, id)) return;
            inventoryArray[raidSlotNum][id].count += quantity;
            SumItemSizeAndNowBagSize(raidSlotNum, itemSize, quantity);
        }
        else
        {
            if (IsLessThanZero(raidSlotNum, quantity)) return;
            AddToInventory(raidSlotNum, id, quantity);
            SumItemSizeAndNowBagSize(raidSlotNum, itemSize, quantity);
        }
    }

    private bool IsOverIfSumItemSizeAndNowBagSize(int raidSlotNum, int itemSize, int quantity)
    {
        return bagSizeArray[raidSlotNum, 0] < bagSizeArray[raidSlotNum, 1] + itemSize * quantity;
    }
    private void SumItemSizeAndNowBagSize(int raidSlotNum, int itemSize, int quantity)
    {
        bagSizeArray[raidSlotNum, 1] += itemSize * quantity;
    }
    private int FindItemSize(ItemCategory category, int itemId)
    {
        if(category == ItemCategory.Food)
        {
            foreach (var item in Managers.Data.FoodDict)
            {
                if(item.Value.id == itemId) return item.Value.itemSize;
            }
        }
        else if (category == ItemCategory.Portion)
        {
            foreach (var item in Managers.Data.PortionDict)
            {
                if (item.Value.id == itemId) return item.Value.itemSize;
            }
        }
        else if (category == ItemCategory.Helmet)
        {
            foreach (var item in Managers.Data.HelmetStatDict)
            {
                if (item.Value.id == itemId) return item.Value.itemSize;
            }
        }
        else if (category == ItemCategory.Clothes)
        {
            foreach (var item in Managers.Data.ClothesStatDict)
            {
                if (item.Value.id == itemId) return item.Value.itemSize;
            }
        }
        else if (category == ItemCategory.Pants)
        {
            foreach (var item in Managers.Data.PantsStatDict)
            {
                if (item.Value.id == itemId) return item.Value.itemSize;
            }
        }
        else if (category == ItemCategory.Accessary)
        {
            foreach (var item in Managers.Data.AccessaryStatDict)
            {
                if (item.Value.id == itemId) return item.Value.itemSize;
            }
        }

        else if (category == ItemCategory.Weapon)
        {
            foreach (var item in Managers.Data.WeaponStatDict)
            {
                if (item.Value.id == itemId) return item.Value.itemSize;
            }
        }

        /*
                 else if (category == ItemCategory.Bag)
        {
            foreach (var item in Managers.Data.BagStatDict)
            {
                if (item.Value.id == itemId) return item.Value.bagSize;
            }
        }
         */



        return 9999999;
    }
    public int[,] GetCharacterBag()
    {
        return bagSizeArray;
    }
    public int[] GetRaidCharacterArray()
    {
        return raidCharacterArray;
    }
    private void SetRaidCharacterArrayFromReadyToRaidSlot(int[] slotArray)
    {
        Debug.Log("raidCharacterArray: " + raidCharacterArray.Length + ", slotArray: " + slotArray.Length);
        if(raidCharacterArray.Length == slotArray.Length)
            raidCharacterArray = slotArray;
    }
    private void ResetVariable()
    {
        raidCharacterArray = new int[4];
        bagSizeArray = new int[4, 2] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } };
        inventoryArray = new Dictionary<int, Information>[4];

        inventoryArray[0] = new Dictionary<int, Information>();
        inventoryArray[1] = new Dictionary<int, Information>();
        inventoryArray[2] = new Dictionary<int, Information>();
        inventoryArray[3] = new Dictionary<int, Information>();
    }
    private void SaveInventoryToMyItemList() //실질적으로 인벤토리데이터를 저장
    {
        for (int i = 0; i < inventoryArray.Length; i++)
        {
            foreach (var item in inventoryArray[i])
            {
                SaveItem(item.Value);
            }
        }
    }
    private void SaveItem(Information info)
    {
        ItemCategory cat = ClassifyCategoryNumFromItemId(info.id);
        MyItemListContents.Instance.increaseMyItem(cat, info.id, info.count);
    }
    public ItemCategory ClassifyCategoryNumFromItemId(int id)
    {
        int leftFourNum = id / 10000;

        int left = leftFourNum / 100;
        int right = leftFourNum % 100;
        
        return (ItemCategory)right;
    }
    private void CheckDefaultBagSize()
    {
        for (int i = 0; i < raidCharacterArray.Length; i++)
        {
            if(Managers.Data.myCharacterDict.ContainsKey(raidCharacterArray[i]))
            {
                Debug.Log("캐릭터 번호" + raidCharacterArray[i]);
                Debug.Log("캐릭터 가방" + Managers.Data.myCharacterDict[raidCharacterArray[i]].bagId);        
                bagSizeArray[i,0] = Managers.Data.BagStatDict[Managers.Data.myCharacterDict[raidCharacterArray[i]].bagId].bagSize;
            }
        }
    }
    private int FindRaidSlotNumFromMyCharacterNum(int myCharacterNum)
    {
        //아이템을 흭득하는 캐릭터의 슬롯넘버가 4인 레이드 캐릭터중 몇번째인지 찾는 함수
        int i;
        for (i = 0; i < raidCharacterArray.Length; i++)
        {
            if(raidCharacterArray[i] == myCharacterNum) break;
        }
        return i; //결과값은 0~3
    }
    private bool IsExistItem(int slotNum, int id)
    {
        Debug.Log("슬롯넘버: " + slotNum);
        Debug.Log("DictL: " + inventoryArray[slotNum].Count);
        return inventoryArray[slotNum].ContainsKey(id);
    }   
    private bool IsLessThanZero(int slotNum, int count, int id = 0)
    {
        if(id != 0)
        {
            if(inventoryArray[slotNum][id].count + count <0)
            {
                Debug.Log($"you are decrementing the count from 0 items. plz check id.{id}");
                return true;
            }
        }
        else
        {
            if (count < 0)
            {
                Debug.Log($"you are decrementing the count from 0 items. plz check id.{id}");
                return true;
            }
        }


        return false;
    }
    private void AddToInventory(int slotNum, int id, int count)
    {
        inventoryArray[slotNum].Add(id,
             new Information
             {
                 id = id,
                 count = count
             });
    }


    #region With EndManager
    //레이드 중간이나 레이드 종료시 흭득한 아이템 표시 가능한 기능 만들어야함
    public Dictionary<int, Information> CheckInventoryArrayDict(int num)
    {
        return inventoryArray[num];
    }
    public Dictionary<int, Information>[] CheckInventoryArrayDict2()
    {
        return inventoryArray;
    }
    #endregion

    #region 졸작 시연 죽음 연출
    public void deadAll()
    {
        for (int i = 0; i < raidCharacterArray.Length; i++)
        {
            if (raidCharacterArray[i] == -1) continue;

            setEquippedItemWhenDead(raidCharacterArray[i]);
        }
    }
    

    public void setEquippedItemWhenDead(int deathCharNum)
    {
        //유효성 검사 필요없을듯?

        Managers.Data.myCharacterDict[deathCharNum].weaponId = 0;
        Managers.Data.myCharacterDict[deathCharNum].bagId = 0;
        Managers.Data.myCharacterDict[deathCharNum].accessaryLId = 0;
        Managers.Data.myCharacterDict[deathCharNum].accessaryRId = 0;
        Managers.Data.myCharacterDict[deathCharNum].helmetId = 0;
        Managers.Data.myCharacterDict[deathCharNum].pantsId = 12130001;
        Managers.Data.myCharacterDict[deathCharNum].clothesId = 12120001;
    }
    #endregion
}
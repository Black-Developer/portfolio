using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitManager : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject selectCamera;
    public GameObject endCamera;
    public GameObject mappingManager;
    public GameObject endGameObject;
    public Vector3[] lookUpList = new Vector3[3];
    public Vector3[] positionList = new Vector3[3];


    public Transform middleRoute;
    public Transform leftRoute;
    public Transform rightRoute;


    public int[] raidChar;
    //------------------탈출구--------------------//
    public GameObject exitArea;
    void Start()
    {
        positionList[0] = new Vector3(59.09f, 0.39f, 15.12f); //middle
        positionList[1] = new Vector3(17.88f, 13.95f, 74.95f); //left
        positionList[2] = new Vector3(100.12f,6.4f,79.79f); //right


        lookUpList[0] = new Vector3(13.96f, 30.5f, -33.07f); //middle
        lookUpList[1] = new Vector3(-36.3f,-21.9f,69.9f); //left
        lookUpList[2] = new Vector3(172,20.3f,16.9f); //right
    }


    void Update()
    {
    }
    public void CameraSetting(int num) //이친구를 coliCheckPlayer에서 가져가서 씀
    {
        SetEndingCamera();
        endCamera.transform.position = positionList[num];
        endCamera.transform.LookAt(lookUpList[num]);
    }
    public void FindExitGameObject()
    {
        exitArea = mappingManager.GetComponent<CastleMappingManager>().ReturnLastMappingGameObject();
    }
    private void SetEndingCamera()
    {
        playerCamera.SetActive(false);
        selectCamera.SetActive(false);
        endCamera.SetActive(true);
    }
    public FollowCurve FindCameraMovingSC(int num)
    {
        DivideRoute(num);
        return endCamera.GetComponent<FollowCurve>();
    }
    private void DivideRoute(int num)
    {
        if(num == 0)
        {
            endCamera.GetComponent<FollowCurve>().RegistRoute(middleRoute);
        }
        else if(num == 1)
        {
            endCamera.GetComponent<FollowCurve>().RegistRoute(leftRoute);
        }
        else if(num == 2)
        {
            endCamera.GetComponent<FollowCurve>().RegistRoute(rightRoute);
        }
    }


    public void GotoBackTitle()
    {
        Managers.Data.Ingame.QuitGame();
    }
    #region FillItemSlot
    public void ClickEndGameBTN()
    {
        endGameObject.transform.GetChild(1).gameObject.SetActive(true);

        int[] CharacterArray = Managers.Data.Ingame.GetRaidCharacterArray();
        int charCount = CheckRaidCharacterCount(CharacterArray);

        raidChar = new int[charCount];
        int j = 0;
        for (int i = 0; i < CharacterArray.Length; i++)
        {
            if (CharacterArray[i] != -1)
                raidChar[j++] = i;
        }



        setActiveBagslot(charCount);
        FillSlot(0);
    }
    private void InitSlotImage()
    {
        for (int i = 0; i < 8; i++)
        {
            ReturnEquipment_slot().transform.GetChild(i).GetChild(0).gameObject.SetActive(false); //이미지
            ReturnEquipment_slot().transform.GetChild(i).GetChild(1).gameObject.SetActive(false); //수량
        }
    }
    public void FillSlot(int num)
    {
        InitSlotImage();
        
        int slotNum = 0;
        foreach (var item in Managers.Data.Ingame.CheckInventoryArrayDict(raidChar[num]))
        {
            ReturnEquipment_slot().transform.GetChild(slotNum).GetChild(0).gameObject.SetActive(true);
            ReturnEquipment_slot().transform.GetChild(slotNum).GetChild(1).gameObject.SetActive(true);

            ItemCategory itemCategory = Managers.Data.Ingame.ClassifyCategoryNumFromItemId(item.Value.id);
            ReturnEquipment_slot().transform.GetChild(slotNum).GetChild(0).GetComponent<Image>().sprite = FindItemImage(itemCategory, item.Value.id);
            ReturnEquipment_slot().transform.GetChild(slotNum).GetChild(1).GetChild(0).GetComponent<Text>().text = item.Value.count + "개";
            slotNum++;
        }
        
    }
    private Sprite FindItemImage(ItemCategory category, int itemId)
    {
        if (category == ItemCategory.Food)
        {
            return Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Food/{Managers.Data.FoodDict[itemId].name}");
        }
        else if (category == ItemCategory.Portion)
        {
            return Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Portion/{Managers.Data.PortionDict[itemId].name}");
        }
        else if (category == ItemCategory.Helmet)
        {
            return Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.HelmetStatDict[itemId].name}");
        }
        else if (category == ItemCategory.Clothes)
        {
            return Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.ClothesStatDict[itemId].name}");
        }
        else if (category == ItemCategory.Pants)
        {
            return Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.PantsStatDict[itemId].name}");
        }
        else if (category == ItemCategory.Accessary)
        {
            return Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Armors/{Managers.Data.AccessaryStatDict[itemId].name}");
        }
        else if (category == ItemCategory.Weapon)
        {
            return Managers.Resource.Load<Sprite>($"Image/Item/Equipments/Weapons/{Managers.Data.WeaponStatDict[itemId].name}");
        }
        else if (category == ItemCategory.Bag)
        {
            return Managers.Resource.Load<Sprite>($"Image/Item/Equipments/{Managers.Data.BagStatDict[itemId].name}");
        }
        return null;
    }
    private int CheckRaidCharacterCount(int[] array)
    {
        int count = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if(array[i] != -1) count++;
        }    
        return count;
    }
    private void setActiveBagslot(int count)
    {
        for (int i = 0; i < count; i++)
        {
            returnSlotNumGameObject().transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    private GameObject ReturnEquipment_slot()
    {
        return endGameObject.transform.GetChild(1).GetChild(3).gameObject;
    }
    private GameObject returnSlotNumGameObject()
    {
        return endGameObject.transform.GetChild(1).GetChild(2).gameObject;
    }
    #endregion
}

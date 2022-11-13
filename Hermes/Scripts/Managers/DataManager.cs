using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEditor;
public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    #region ingameInventoryItemData
    IngameInventoryManager _ingameData;
    public IngameInventoryManager Ingame { get { return this._ingameData; } }
    #endregion
    #region LoadReferenceItemData
    public Dictionary<int, CharacterStat> CharacterStatDict { get; private set; } = new Dictionary<int, CharacterStat> ();
    public Dictionary<int, WeaponStat> WeaponStatDict { get; private set; } = new Dictionary<int, WeaponStat> ();
    public Dictionary<int, HelmetStat> HelmetStatDict { get; private set; } = new Dictionary<int, HelmetStat> ();
    public Dictionary<int, ClothesStat> ClothesStatDict { get; private set; } = new Dictionary<int, ClothesStat> ();
    public Dictionary<int, PantsStat> PantsStatDict { get; private set; } = new Dictionary<int, PantsStat> ();
    public Dictionary<int, AccessaryStat> AccessaryStatDict { get; private set; } = new Dictionary<int, AccessaryStat> ();
    public Dictionary<int, BagStat> BagStatDict { get; private set; } = new Dictionary<int, BagStat> ();
    public Dictionary<int, PortionStat> PortionDict { get; private set; } = new Dictionary<int, PortionStat> ();
    public Dictionary<int, FoodStat> FoodDict { get; private set; } = new Dictionary<int, FoodStat> ();
    #endregion
    #region LoadReferenceShapeData
    public Dictionary<int, ShapeStat> HairDict { get; private set; } = new Dictionary<int, ShapeStat>();
    public Dictionary<int, ShapeStat> EyebrowDict { get; private set; } = new Dictionary<int, ShapeStat>();
    public Dictionary<int, ShapeStat> FaceDict { get; private set; } = new Dictionary<int, ShapeStat>();
    public Dictionary<int, ShapeStat> FacialDict { get; private set; } = new Dictionary<int, ShapeStat>();
    public Dictionary<int, ShapeStat> ArmLRDict { get; private set; } = new Dictionary<int, ShapeStat>();
    public Dictionary<int, ShapeStat> ArmLLDict { get; private set; } = new Dictionary<int, ShapeStat>();
    public Dictionary<int, ShapeStat> ArmURDict { get; private set; } = new Dictionary<int, ShapeStat>();
    public Dictionary<int, ShapeStat> ArmULDict { get; private set; } = new Dictionary<int, ShapeStat>();
    public Dictionary<int, ShapeStat> HandLDict { get; private set; } = new Dictionary<int, ShapeStat>();
    public Dictionary<int, ShapeStat> HandRDict { get; private set; } = new Dictionary<int, ShapeStat>();
    public Dictionary<int, ShapeStat> LegLDict { get; private set; } = new Dictionary<int, ShapeStat>();
    public Dictionary<int, ShapeStat> LegRDict { get; private set; } = new Dictionary<int, ShapeStat>();
    #endregion
    #region LoadUserData
    [SerializeField] //유저 캐릭터(with 장착 아이템) 데이터는 직렬화를 통해 json포맷으로 저장
    public Dictionary<int, EquippedItem> myCharacterDict = new Dictionary<int, EquippedItem>();
    [SerializeField] //각 슬롯별 외형 데이터
    public Dictionary<int, CharacterShape> myCharacterShapeDict = new Dictionary<int, CharacterShape>();
    #endregion


    public void Init()
    {
        ReferenceItemInit();
        ReferenceShapeInit();
        UserDataInit();
        IngameInventoryItemDataInit();
    }
    private void IngameInventoryItemDataInit()
    {
        _ingameData = new IngameInventoryManager();
    }
    private void ReferenceItemInit()
    {
        CharacterStatDict = Loadjson<CharacterStatData, int, CharacterStat>("CharacterStatData").MakeDict();
        WeaponStatDict = Loadjson<WeaponStatData, int, WeaponStat>("WeaponItemData").MakeDict();
        HelmetStatDict = Loadjson<HelmetStatData, int, HelmetStat>("HelmetItemData").MakeDict();
        ClothesStatDict = Loadjson<ClothesStatData, int, ClothesStat>("ClothesItemData").MakeDict();
        PantsStatDict = Loadjson<PantsStatData, int, PantsStat>("PantsItemData").MakeDict();
        AccessaryStatDict = Loadjson<AccessaryStatData, int, AccessaryStat>("AccessaryItemData").MakeDict();
        BagStatDict = Loadjson<BagStatData, int, BagStat>("BagItemData").MakeDict();
        PortionDict = Loadjson<PortionStatData, int, PortionStat>("PortionItemData").MakeDict();
        FoodDict = Loadjson<FoodStatData, int, FoodStat>("FoodItemData").MakeDict();

        Debug.Log("success load reference item data");
    }
    private void ReferenceShapeInit()
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/Shape");
        ShapeStatData temp = JsonUtility.FromJson<ShapeStatData>(textAsset.text);
        HairDict = temp.MakeHairDict();
        EyebrowDict = temp.MakeEyebrowDict();
        FaceDict = temp.MakeFaceDict();
        FacialDict = temp.MakeFacialDict();
        ArmLRDict = temp.MakeArmLRDict();
        ArmLLDict = temp.MakeArmLLDict();
        ArmURDict = temp.MakeArmURDict();
        ArmULDict = temp.MakeArmULDict();
        HandLDict = temp.MakeHandLDict();
        HandRDict = temp.MakeHandRDict();
        LegLDict = temp.MakeLegLDict();
        LegRDict = temp.MakeLegRDict();

        Debug.Log("success load reference shape data");
    }
    private void UserDataInit()
    {
        //myCharacterDict = Loadjson<EquippedItemData, int, EquippedItem>("userData/EquippedItem").MakeDict();
        //myCharacterShapeDict = Loadjson<CharacterShapeData, int, CharacterShape>("userData/SlotsPerCharacter").MakeDict();


        myCharacterDict = Loadjson2<EquippedItemData, int, EquippedItem>("EquippedItem.json").MakeDict();
        myCharacterShapeDict = Loadjson2<CharacterShapeData, int, CharacterShape>("SlotsPerCharacter.json").MakeDict();
        Debug.Log("success load user data");
    }


    public void quitGame()
    {
        SaveUserCharacterData("EquippedItem");
        SaveUserCharacterShapeData("SlotsPerCharacter");
        SaveUserItemData("MyItemData");
        //AssetDatabase.Refresh();
    }

    Loader Loadjson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
    Loader Loadjson2<Loader, Key, Value>(string _path) where Loader : ILoader<Key, Value>
    {
        string path = Application.persistentDataPath + "/" + _path;
        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<Loader>(json);
    }
    #region saveUserData
    private void SaveUserCharacterData(string name)
    {
        EquippedItemData temp = new EquippedItemData();

        foreach (var item in myCharacterDict) //딕셔너리는 직렬화가 불가능하므로 다시 변환
        {
            temp.equippedItemStat.Add(item.Value);
        }
        //string _path = $"Assets/Resources/Data/userData/{name}.json"; //이 경로에 유저의 캐릭터 데이터 저장
        string _path = Application.persistentDataPath + $"/{name}.json";
        string jsonData = JsonUtility.ToJson(temp, true);
        File.WriteAllText(_path, jsonData);
        Debug.Log("success save user character data");
    }
    private void SaveUserCharacterShapeData(string name)
    {
        CharacterShapeData temp = new CharacterShapeData();
        foreach (var item in myCharacterShapeDict)
        {
            temp.SlotsPerCharacterShape.Add(item.Value);
        }

        //string _path = $"Assets/Resources/Data/userData/{name}.json";
        string _path = Application.persistentDataPath + $"/{name}.json";

        string jsonData = JsonUtility.ToJson(temp, true);
        File.WriteAllText(_path, jsonData);
        Debug.Log("success save user character shape data");
    }
    private void SaveUserItemData(string name)
    {
        MyItemData temp = new MyItemData();

        foreach (var item in MyItemListContents.Instance.myWeaponItemDict)
            temp.myWeaponItemList.Add(item.Value);
        foreach (var item in MyItemListContents.Instance.myPantsItemDict)
            temp.myPantsItemList.Add(item.Value);
        foreach (var item in MyItemListContents.Instance.myClothesItemDict)
            temp.myClothesItemList.Add(item.Value);
        foreach (var item in MyItemListContents.Instance.myHelmetItemDict)
            temp.myHelmetItemList.Add(item.Value);
        foreach (var item in MyItemListContents.Instance.myAccessaryItemDict)
            temp.myAccessaryItemList.Add(item.Value);
        foreach (var item in MyItemListContents.Instance.myBagItemDict)
            temp.myBagItemList.Add(item.Value);
        foreach (var item in MyItemListContents.Instance.myPortionItemDict)
            temp.myPortionItemList.Add(item.Value);
        foreach (var item in MyItemListContents.Instance.myFoodItemDict)
            temp.myFoodItemList.Add(item.Value);
        //string _path = $"Assets/Resources/Data/userData/{name}.json"; //이 경로에 유저의 아이템 데이터 저장
        string _path = Application.persistentDataPath + $"/{name}.json";

        string jsonData = JsonUtility.ToJson(temp, true);
        File.WriteAllText(_path, jsonData);
        Debug.Log("success save user item data");
    }
    #endregion
}
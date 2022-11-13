using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // dictionary의 ElementAt 사용
using System.IO;  //fileStream 사용
using System.Text; //Encoding 사용
public enum ItemCategory
{
    None = 8,
    Food,
    Portion,
    Helmet,
    Clothes,
    Pants,
    Accessary,
    Bag,
    Weapon
}

public class MyItemListContents : MonoBehaviour
{
    #region Singleton
    static MyItemListContents s_instance;
    public static MyItemListContents Instance { get { Init(); return s_instance; } }
    #endregion


    public Dictionary<int,Information> myWeaponItemDict = new Dictionary<int, Information>();
    public Dictionary<int,Information> myPantsItemDict = new Dictionary<int, Information>();
    public Dictionary<int,Information> myClothesItemDict = new Dictionary<int, Information>();
    public Dictionary<int,Information> myAccessaryItemDict = new Dictionary<int, Information>();
    public Dictionary<int, Information> myHelmetItemDict = new Dictionary<int, Information>();
    public Dictionary<int, Information> myBagItemDict = new Dictionary<int, Information>();
    public Dictionary<int, Information> myPortionItemDict = new Dictionary<int, Information>();
    public Dictionary<int, Information> myFoodItemDict = new Dictionary<int, Information>();

    private string[] nameArray = new string[] { "클라우드", "별헤아림", "이루스", "블랙", "에녹", "헤레이스", "아더", "세실", "에드윈", "로렌스", "올리버", "얼터", "미카엘", "다리아", "벨리타", "벨", "니키타", "힐다", "아리아", "아든", "오브리", "베일리", "케서디", "셀린", "일라이", "핀리", "프란시스", "글랜", "헤들리", "헤이든", "제스", "줄스", "킹슬리", "로건", "샤일로", "세마" };


    void Start()
    {
        Init();
        LoadUserItemDataJson("MyItemData.json");
    }

    static void Init()
    {
        if (s_instance != null) return;
        GameObject go = GameObject.Find("GameManager");
        if (go == null)
        {
            go = new GameObject { name = "GameManager" };
            go.AddComponent<MyItemListContents>();
        }
        DontDestroyOnLoad(go);
        s_instance = go.GetComponent<MyItemListContents>();
    }

    private void LoadUserItemDataJson(string name)
    {
        //TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/userData/{name}");
        //MyItemData temp = JsonUtility.FromJson<MyItemData>(textAsset.text);


        string path = Application.persistentDataPath + "/" + name;
        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json = Encoding.UTF8.GetString(data);
        MyItemData temp = JsonUtility.FromJson<MyItemData>(json);


        myWeaponItemDict = temp.MakeWeaponItemDict();
        myPantsItemDict = temp.MakePantsItemDict();
        myClothesItemDict = temp.MakeClothesItemDict();
        myAccessaryItemDict = temp.MakeAccessaryItemDict();
        myHelmetItemDict = temp.MakeHelmetItemDict();
        myBagItemDict = temp.MakeBagItemDict();
        myPortionItemDict = temp.MakePortionItemDict();
        myFoodItemDict = temp.MakeFoodItemDict();

        Debug.Log("success load myItemList");

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

    public (EquippedItem, CharacterShape)  createMyCharacter(int characterId)
    {
        EquippedItem myData = new EquippedItem();
        CharacterStat defaultData = Managers.Data.CharacterStatDict[characterId];

        myData.num = 0;
        myData.characterId = defaultData.id;
        myData.name = MakeRandomName();
        myData.weaponId = defaultData.weaponId;
        myData.pantsId = defaultData.pantsId;
        myData.clothesId = defaultData.clothesId;
        myData.accessaryLId = defaultData.accessaryLId;
        myData.accessaryRId = defaultData.accessaryRId;
        myData.bagId = defaultData.bagId;
        myData.helmetId = defaultData.helmetId;

        CharacterShape myShape = randomInputShape();
        return (myData, myShape);
    }
    public void insertMyCharacter(EquippedItem ei, CharacterShape cs, int num)
    {
        if(isEmptySlot(num))
        {
            registCharacterDict(ei, cs, num);
        }
        else
        {
            changerMyCharacter(ei, cs, num);
        }
    }
    private string MakeRandomName()
    {
        string holymoly = "기본값임. 무언가 잘못됨을 알려주는 닉네임 생성기의 무언가가 잘못됨을 알고있는 무언가가 이것은 잘못되었다고 판단하는 어떤 것";
        int rand = UnityEngine.Random.Range(0, nameArray.Length);
        holymoly = nameArray[rand];
        return holymoly;
    }
    public void sexonthesity() //자동 이름 생성기 테스트
    {
        for (int i = 0; i < 30; i++)
        {
            System.Random random = new System.Random();
            int rand = UnityEngine.Random.Range(0, nameArray.Length);
            print("이름: " + nameArray[rand]);
        }
        
    }
    private bool isEmptySlot(int num)
    {
        if (Managers.Data.myCharacterDict.ContainsKey(num)) return false;
        return true;
    }
    public bool isSlotCountLessThanSlotMaxCount()
    {
        if (findCharacterSlotCount() < 8) return true;
        return false;
    }
    public void removeMyCharacter(int num)
    {
        if (!isExistCharacter(num)) return;
        if (!isDisarm(num)) return;

        Managers.Data.myCharacterDict.Remove(num);
        Managers.Data.myCharacterShapeDict.Remove(num);
        Debug.Log($"success remove No.{num} slot");

    }

    public int findCharacterSlotCount()
    {

        return Managers.Data.myCharacterDict.Count;
    }
    public void registSlotNumToDummyData(ref EquippedItem ei, ref CharacterShape cs, int num)
    {
        //int slotNum = findCharacterSlotCount() + 1;
        int slotNum = num;
        ei.num = slotNum;
        cs.num = slotNum;
    }
    private bool isWantSlotChange(int num) //0에 해당하는 슬롯은 없음(1~8)
    {
        if (num == 0) return false;
        return true;
    }
    private bool isExistCharacter(int num)
    {
        if (!Managers.Data.myCharacterDict.ContainsKey(num)) //해당하는 슬롯이 없으면
        {
            Debug.Log($"null No.{num} character");
            return false;
        }
        return true;
    }
    private bool isDisarm(int num)
    {
        if (Managers.Data.myCharacterDict[num].weaponId != 0
            || Managers.Data.myCharacterDict[num].pantsId != 0
            || Managers.Data.myCharacterDict[num].clothesId != 0
            || Managers.Data.myCharacterDict[num].accessaryLId != 0
            || Managers.Data.myCharacterDict[num].accessaryRId != 0
            || Managers.Data.myCharacterDict[num].bagId != 0
            || Managers.Data.myCharacterDict[num].helmetId != 0
            )                                                           //장비를 해제하지 않았으면
        {
            Debug.Log($"plz dismount item in No.{num} character");
            return false;
        }
        return true;
    }
    private void registCharacterDict(EquippedItem ei, CharacterShape cs, int num)
    {
        registSlotNumToDummyData(ref ei, ref cs, num);
        Managers.Data.myCharacterDict.Add(ei.num, ei);
        Managers.Data.myCharacterShapeDict.Add(cs.num, cs);

        //Debug.Log(cs.num);
        //Debug.Log(cs.hairId);
    }
    private void changerMyCharacter(EquippedItem ei, CharacterShape cs, int num)
    {
        if (!isExistCharacter(num)) return;
        //if (!isDisarm(num)) return;  장비해제체크 코드

        

        CharacterStat defaultData = Managers.Data.CharacterStatDict[ei.characterId];
        Managers.Data.myCharacterDict[num].characterId = defaultData.id;
        Managers.Data.myCharacterDict[num].name = ei.name;
        Managers.Data.myCharacterDict[num].weaponId = defaultData.weaponId;
        Managers.Data.myCharacterDict[num].pantsId = defaultData.pantsId;
        Managers.Data.myCharacterDict[num].clothesId = defaultData.clothesId;
        Managers.Data.myCharacterDict[num].accessaryLId = defaultData.accessaryLId;
        Managers.Data.myCharacterDict[num].accessaryRId = defaultData.accessaryRId;
        Managers.Data.myCharacterDict[num].bagId = defaultData.bagId;
        Managers.Data.myCharacterDict[num].helmetId = defaultData.helmetId;

        cs.num = num;
        Managers.Data.myCharacterShapeDict[num] = cs;
    }
    private CharacterShape randomInputShape()
    {
        CharacterShape temp = new CharacterShape();

        System.Random random = new System.Random();

        temp.num = 0;
        temp.hairId = Managers.Data.HairDict.ElementAt(random.Next(Managers.Data.HairDict.Count)).Value.id;
        temp.eyebrowId = Managers.Data.EyebrowDict.ElementAt(random.Next(Managers.Data.EyebrowDict.Count)).Value.id;
        temp.faceId = Managers.Data.FaceDict.ElementAt(random.Next(Managers.Data.FaceDict.Count)).Value.id;
        temp.facialId = Managers.Data.FacialDict.ElementAt(random.Next(Managers.Data.FacialDict.Count)).Value.id;
        temp.armLRId = Managers.Data.ArmLRDict.ElementAt(random.Next(Managers.Data.ArmLRDict.Count)).Value.id;
        temp.armLLId = Managers.Data.ArmLLDict.ElementAt(random.Next(Managers.Data.ArmLLDict.Count)).Value.id;
        temp.armURId = Managers.Data.ArmURDict.ElementAt(random.Next(Managers.Data.ArmURDict.Count)).Value.id;
        temp.armULId = Managers.Data.ArmULDict.ElementAt(random.Next(Managers.Data.ArmULDict.Count)).Value.id;
        temp.handLId = Managers.Data.HandLDict.ElementAt(random.Next(Managers.Data.HandLDict.Count)).Value.id;
        temp.handRId = Managers.Data.HandRDict.ElementAt(random.Next(Managers.Data.HandRDict.Count)).Value.id;
        temp.legLId = Managers.Data.LegLDict.ElementAt(random.Next(Managers.Data.LegLDict.Count)).Value.id;
        temp.legRId = Managers.Data.LegRDict.ElementAt(random.Next(Managers.Data.LegRDict.Count)).Value.id;
        return temp;
    }


    
    /*--보유 아이템 증가 감소--*/
    public void increaseMyItem(ItemCategory name, int id, int count =0)
    {
        if (name == ItemCategory.Weapon)
            increaseMyitemImplementation(MyItemListContents.Instance.myWeaponItemDict, id, count);
        else if (name == ItemCategory.Pants)
            increaseMyitemImplementation(MyItemListContents.Instance.myPantsItemDict, id, count);
        else if (name == ItemCategory.Clothes)
            increaseMyitemImplementation(MyItemListContents.Instance.myClothesItemDict, id, count);
        else if (name == ItemCategory.Accessary)
            increaseMyitemImplementation(MyItemListContents.Instance.myAccessaryItemDict, id, count);
        else if (name == ItemCategory.Helmet)
            increaseMyitemImplementation(MyItemListContents.Instance.myHelmetItemDict, id, count);
        else if (name == ItemCategory.Bag)
            increaseMyitemImplementation(MyItemListContents.Instance.myBagItemDict, id, count);
        else if (name == ItemCategory.Portion)
            increaseMyitemImplementation(MyItemListContents.Instance.myPortionItemDict, id, count);
        else if (name == ItemCategory.Food)
            increaseMyitemImplementation(MyItemListContents.Instance.myFoodItemDict, id, count);
    }
    private void increaseMyitemImplementation(Dictionary<int, Information> category, int id, int count)
    {
        if (category.ContainsKey(id))
        {
            if(category[id].count + count < 0)
            {
                Debug.Log($"you are decrementing the count from 0 items. plz check id.{id}");
                return;
            }
            category[id].count += count;
            Debug.Log($"(already exists id) success increase {count} in id.{id}");
        }
        else
        {
            if(count < 0)
            {
                Debug.Log($"you are decrementing the count from 0 items. plz check id.{id}");
                return;
            }

            category.Add(id,
                new Information
                {
                    id = id,
                    count = count
                });
            Debug.Log($"(new id) success increase {count} in id.{id}");

        }
    }
}




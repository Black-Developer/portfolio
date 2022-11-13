    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[Serializable]
public class defaultItem
{
    public int id;
    public string name;
    public int itemSize;
}


/*--Default reference data--*/
#region CharactorStat
[Serializable]
public class CharacterStat
{
    public int id;
    public string name;

    public int hp;
    public int damage;
    public float attackSpeed;
    public float accuracy;
    public int critical;
    public int armor;

    //특정 캐릭터가 가지는 디폴트 장비
    public int weaponId;
    public int pantsId;
    public int clothesId;
    public int bagId;
    public int accessaryLId;
    public int accessaryRId;
    public int helmetId;
}
[Serializable]
public class CharacterStatData : ILoader<int, CharacterStat>
{
    public List<CharacterStat> characterStats = new List<CharacterStat>();
    public Dictionary<int, CharacterStat> MakeDict()
    {
        Dictionary<int, CharacterStat> dict = new Dictionary<int, CharacterStat>();
        foreach (CharacterStat characterStat in characterStats)
            dict.Add(characterStat.id, characterStat);
        return dict;
    }
}
#endregion
#region weaponStat
[Serializable]
public class WeaponStat : defaultItem
{
    public int damage;
    public float attackSpeed;
    public float accuracy;
    public int critical;
    public int type;
}
[Serializable]
public class WeaponStatData : ILoader<int, WeaponStat>
{
    public List<WeaponStat> weaponData = new List<WeaponStat>();
    public Dictionary<int, WeaponStat> MakeDict()
    {
        Dictionary<int, WeaponStat> dict = new Dictionary<int, WeaponStat>();
        foreach (WeaponStat weaponStat in weaponData)
            dict.Add(weaponStat.id, weaponStat);
        return dict;
    }
}
#endregion
#region helmetStat
[Serializable]
public class HelmetStat : defaultItem
{
    public float attackSpeed;
    public float accuracy;
    public int hp;
    public int armor;
}
[Serializable]
public class HelmetStatData : ILoader<int, HelmetStat>
{
    public List<HelmetStat> helmetData = new List<HelmetStat>();
    public Dictionary<int, HelmetStat> MakeDict()
    {
        Dictionary<int, HelmetStat> dict = new Dictionary<int, HelmetStat>();
        foreach (HelmetStat helmetStat in helmetData)
            dict.Add(helmetStat.id, helmetStat);
        return dict;
    }
}
#endregion
#region clothesStat
[Serializable]
public class ClothesStat : defaultItem
{
    public float attackSpeed;
    public float accuracy;
    public int hp;
    public int armor;
}
[Serializable]
public class ClothesStatData : ILoader<int, ClothesStat>
{
    public List<ClothesStat> clothesData = new List<ClothesStat>();
    public Dictionary<int, ClothesStat> MakeDict()
    {
        Dictionary<int, ClothesStat> dict = new Dictionary<int, ClothesStat>();
        foreach (ClothesStat clothesStat in clothesData)
            dict.Add(clothesStat.id, clothesStat);
        return dict;
    }
}
#endregion
#region pantsStat
[Serializable]
public class PantsStat : defaultItem
{
    public int hp;
    public int armor;
    public float critical;
}
[Serializable]
public class PantsStatData : ILoader<int, PantsStat>
{
    public List<PantsStat> pantsData = new List<PantsStat>();
    public Dictionary<int, PantsStat> MakeDict()
    {
        Dictionary<int, PantsStat> dict = new Dictionary<int, PantsStat>();
        foreach (PantsStat pantsStat in pantsData)
            dict.Add(pantsStat.id, pantsStat);
        return dict;
    }
}
#endregion
#region accessaryStat
[Serializable]
public class AccessaryStat : defaultItem
{
    public int damage;
    public float attackSpeed;
    public float critical;
}
[Serializable]
public class AccessaryStatData : ILoader<int, AccessaryStat>
{
    public List<AccessaryStat> accessaryData = new List<AccessaryStat>();
    public Dictionary<int, AccessaryStat> MakeDict()
    {
        Dictionary<int, AccessaryStat> dict = new Dictionary<int, AccessaryStat>();
        foreach (AccessaryStat accessaryStat in accessaryData)
            dict.Add(accessaryStat.id, accessaryStat);
        return dict;
    }
}
#endregion
#region bagStat
[Serializable]
public class BagStat
{
    public int id;
    public string name;
    public int bagSize;
}
[Serializable]
public class BagStatData : ILoader<int, BagStat>
{
    public List<BagStat> bagData = new List<BagStat>();
    public Dictionary<int, BagStat> MakeDict()
    {
        Dictionary<int, BagStat> dict = new Dictionary<int, BagStat>();
        foreach (BagStat bagStat in bagData)
            dict.Add(bagStat.id, bagStat);
        return dict;
    }
}
#endregion
#region portionStat
[Serializable]
public class PortionStat : defaultItem
{
}
[Serializable]
public class PortionStatData : ILoader<int, PortionStat>
{
    public List<PortionStat> portionData = new List<PortionStat>();
    public Dictionary<int, PortionStat> MakeDict()
    {
        Dictionary<int, PortionStat> dict = new Dictionary<int, PortionStat>();
        foreach (PortionStat portionStat in portionData)
            dict.Add(portionStat.id, portionStat);
        return dict;
    }
}
#endregion
#region foodStat
[Serializable]
public class FoodStat : defaultItem
{
}
[Serializable]
public class FoodStatData : ILoader<int, FoodStat>
{
    public List<FoodStat> foodData = new List<FoodStat>();
    public Dictionary<int, FoodStat> MakeDict()
    {
        Dictionary<int, FoodStat> dict = new Dictionary<int, FoodStat>();
        foreach (FoodStat foodStat in foodData)
            dict.Add(foodStat.id, foodStat);
        return dict;
    }
}
#endregion
#region ShapeStat
[Serializable]
public class ShapeStat
{
    public int id;
    public string name;
}
[Serializable]
public class ShapeStatData
{
    public List<ShapeStat> faceData = new List<ShapeStat>();
    public List<ShapeStat> hairData = new List<ShapeStat>();
    public List<ShapeStat> eyebrowData = new List<ShapeStat>();
    public List<ShapeStat> facialData = new List<ShapeStat>();
    public List<ShapeStat> armLRData = new List<ShapeStat>();
    public List<ShapeStat> armLLData = new List<ShapeStat>();
    public List<ShapeStat> armURData = new List<ShapeStat>();
    public List<ShapeStat> armULData = new List<ShapeStat>();
    public List<ShapeStat> handRData = new List<ShapeStat>();
    public List<ShapeStat> handLData = new List<ShapeStat>();
    public List<ShapeStat> legRData = new List<ShapeStat>();
    public List<ShapeStat> legLData = new List<ShapeStat>();
    public Dictionary<int, ShapeStat> MakeHairDict()
    {
        Dictionary<int, ShapeStat> dict = new Dictionary<int, ShapeStat>();
        foreach (ShapeStat item in hairData)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, ShapeStat> MakeEyebrowDict()
    {
        Dictionary<int, ShapeStat> dict = new Dictionary<int, ShapeStat>();
        foreach (ShapeStat item in eyebrowData)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, ShapeStat> MakeFaceDict()
    {
        Dictionary<int, ShapeStat> dict = new Dictionary<int, ShapeStat>();
        foreach (ShapeStat item in faceData)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, ShapeStat> MakeFacialDict()
    {
        Dictionary<int, ShapeStat> dict = new Dictionary<int, ShapeStat>();
        foreach (ShapeStat item in facialData)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, ShapeStat> MakeArmLRDict()
    {
        Dictionary<int, ShapeStat> dict = new Dictionary<int, ShapeStat>();
        foreach (ShapeStat item in armLRData)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, ShapeStat> MakeArmLLDict()
    {
        Dictionary<int, ShapeStat> dict = new Dictionary<int, ShapeStat>();
        foreach (ShapeStat item in armLLData)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, ShapeStat> MakeArmURDict()
    {
        Dictionary<int, ShapeStat> dict = new Dictionary<int, ShapeStat>();
        foreach (ShapeStat item in armURData)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, ShapeStat> MakeArmULDict()
    {
        Dictionary<int, ShapeStat> dict = new Dictionary<int, ShapeStat>();
        foreach (ShapeStat item in armULData)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, ShapeStat> MakeHandLDict()
    {
        Dictionary<int, ShapeStat> dict = new Dictionary<int, ShapeStat>();
        foreach (ShapeStat item in handLData)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, ShapeStat> MakeHandRDict()
    {
        Dictionary<int, ShapeStat> dict = new Dictionary<int, ShapeStat>();
        foreach (ShapeStat item in handRData)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, ShapeStat> MakeLegLDict()
    {
        Dictionary<int, ShapeStat> dict = new Dictionary<int, ShapeStat>();
        foreach (ShapeStat item in legLData)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, ShapeStat> MakeLegRDict()
    {
        Dictionary<int, ShapeStat> dict = new Dictionary<int, ShapeStat>();
        foreach (ShapeStat item in legRData)
            dict.Add(item.id, item);
        return dict;
    }
}
#endregion
/*--User Data--*/
#region myCharacterList
    [Serializable]  
public class EquippedItem
{
    public int num;
    public string name;
    public int characterId;
    public int weaponId;
    public int pantsId;
    public int clothesId;
    public int bagId;
    public int accessaryLId;
    public int accessaryRId;
    public int helmetId;

    public (int,int,float,float,int) SumAbility()
    {
        Managers.Data.WeaponStatDict.TryGetValue(weaponId, out var weapon);
        Managers.Data.AccessaryStatDict.TryGetValue(accessaryRId, out var accessary);
        Managers.Data.ClothesStatDict.TryGetValue(clothesId, out var clothes);
        Managers.Data.HelmetStatDict.TryGetValue(helmetId, out var helmet);
        Managers.Data.PantsStatDict.TryGetValue(pantsId, out var pants);
        if (weapon == null)
        {
            weapon = new WeaponStat();
            weapon.accuracy = 0.0f;
            weapon.attackSpeed = 0.0f;
            weapon.critical = 0;
            weapon.damage = 0;
        }
        if(accessary == null)
        {
            accessary = new AccessaryStat();
            accessary.damage = 0;
            accessary.critical = 0;
            accessary.attackSpeed = 0.0f;
        }
        if(clothes == null)
        {
            clothes = new ClothesStat();
            clothes.hp = 0;
            clothes.armor = 0;
        }
        if(helmet == null)
        {
            helmet = new HelmetStat();
            helmet.hp = 0;
            helmet.accuracy = 0.0f;
            helmet.armor = 0;
            helmet.attackSpeed = 0.0f;
        }
        if(pants == null)
        {
            pants = new PantsStat();
            pants.hp = 0;
            pants.armor = 0;
        }


        int hp = 0;
        int damage = 0;
        float attackSpeed = 0;
        float accuracy = 0;
        int armor = 0;

        hp = clothes.hp + helmet.hp + pants.hp;
        damage = weapon.damage + accessary.damage;
        attackSpeed = weapon.attackSpeed + accessary.attackSpeed + helmet.attackSpeed;
        accuracy = weapon.accuracy + helmet.accuracy;
        armor = clothes.armor + helmet.armor + pants.armor;
       

        return (hp, damage, attackSpeed, accuracy, armor);
    }
    public string LoadEquippedItemNames()
    {
        string equipmentText = "";
        if (Managers.Data.HelmetStatDict.TryGetValue(helmetId, out var helmet))
        {
            equipmentText += "헬맷: " + helmet.name + "\n";
        }
        else
            equipmentText += "헬맷: 미착용" + "\n";
        if (Managers.Data.WeaponStatDict.TryGetValue(weaponId, out var weapon))
        {
            equipmentText += "무기: " + weapon.name + "\n";
        }
        else
            equipmentText += "무기: 미착용" + "\n";
        if (Managers.Data.ClothesStatDict.TryGetValue(clothesId, out var clothes))
        {
            equipmentText += "상의: " + clothes.name + "\n";
        }
        else
            equipmentText += "상의: 미착용" + "\n";
        if (Managers.Data.PantsStatDict.TryGetValue(pantsId, out var pants))
        {
            equipmentText += "하의: " + pants.name + "\n";
        }
        else
            equipmentText += "하의: 미착용" + "\n";
        if (Managers.Data.AccessaryStatDict.TryGetValue(accessaryLId, out var accessary))
        {
            equipmentText += "견장: " + accessary.name + "\n";
        }
        else
            equipmentText += "견장: 미착용" + "\n";
        if (Managers.Data.BagStatDict.TryGetValue(bagId, out var bag))
        {
            equipmentText += "가방: " + bag.name;
        }
        else
            equipmentText += "가방: 미착용";

        return equipmentText;
    }
    public string LoadEquippedItemAbility()
    {
        string equipmentText = "";

        int hp = 0;
        int damage = 0;
        float attackSpeed = 0;
        float accuracy = 0;
        int armor = 0;

        (hp, damage, attackSpeed, accuracy, armor) = SumAbility();

        equipmentText += "체력: " + hp + "\n";
        equipmentText += "공격력: " + damage + "\n";
        equipmentText += "공격속도: " + attackSpeed + "\n";
        equipmentText += "치명타 확률: " + accuracy + "\n";
        equipmentText += "방어력: " + armor + "\n";



        return equipmentText;
    }
}
[Serializable]
public class EquippedItemData : ILoader<int, EquippedItem>
{
    public List<EquippedItem> equippedItemStat = new List<EquippedItem>();

    public Dictionary<int, EquippedItem> MakeDict()
    {
        Dictionary<int, EquippedItem> dict = new Dictionary<int, EquippedItem>();
        foreach (EquippedItem item in equippedItemStat)
            dict.Add(item.num, item);
        return dict;
    }
}

#endregion
#region myCharacterShapeList
[Serializable]
public class CharacterShape
{
    public int num;
    public int hairId;
    public int eyebrowId;
    public int faceId;
    public int facialId;
    public int armLRId;
    public int armLLId;
    public int armURId;
    public int armULId;
    public int handLId;
    public int handRId;
    public int legLId;
    public int legRId;
}
[Serializable]
public class CharacterShapeData : ILoader<int, CharacterShape>
{
    public List<CharacterShape> SlotsPerCharacterShape = new List<CharacterShape>();

    public Dictionary<int, CharacterShape> MakeDict()
    {
        Dictionary<int, CharacterShape> dict = new Dictionary<int, CharacterShape>();
        foreach (CharacterShape item in SlotsPerCharacterShape)
            dict.Add(item.num, item);
        return dict;
    }
}

#endregion
#region myItemList
[Serializable]
public class Information
{
    public int id;
    public int count;
}
[Serializable]
public class MyItemData
{
    public List<Information> myWeaponItemList = new List<Information>();
    public List<Information> myPantsItemList = new List<Information>();
    public List<Information> myClothesItemList = new List<Information>();
    public List<Information> myAccessaryItemList = new List<Information>();
    public List<Information> myHelmetItemList = new List<Information>();
    public List<Information> myBagItemList = new List<Information>();
    public List<Information> myPortionItemList = new List<Information>();
    public List<Information> myFoodItemList = new List<Information>();

    public Dictionary<int, Information> MakeWeaponItemDict()
    {
        Dictionary<int, Information> dict = new Dictionary<int, Information>();
        foreach (Information item in myWeaponItemList)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, Information> MakePantsItemDict()
    {
        Dictionary<int, Information> dict = new Dictionary<int, Information>();
        foreach (Information item in myPantsItemList)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, Information> MakeClothesItemDict()
    {
        Dictionary<int, Information> dict = new Dictionary<int, Information>();
        foreach (Information item in myClothesItemList)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, Information> MakeAccessaryItemDict()
    {
        Dictionary<int, Information> dict = new Dictionary<int, Information>();
        foreach (Information item in myAccessaryItemList)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, Information> MakeHelmetItemDict()
    {
        Dictionary<int, Information> dict = new Dictionary<int, Information>();
        foreach (Information item in myHelmetItemList)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, Information> MakeBagItemDict()
    {
        Dictionary<int, Information> dict = new Dictionary<int, Information>();
        foreach (Information item in myBagItemList)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, Information> MakePortionItemDict()
    {
        Dictionary<int, Information> dict = new Dictionary<int, Information>();
        foreach (Information item in myPortionItemList)
            dict.Add(item.id, item);
        return dict;
    }
    public Dictionary<int, Information> MakeFoodItemDict()
    {
        Dictionary<int, Information> dict = new Dictionary<int, Information>();
        foreach (Information item in myFoodItemList)
            dict.Add(item.id, item);
        return dict;
    }
}
#endregion
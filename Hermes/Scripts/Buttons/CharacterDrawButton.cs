using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDrawButton : MonoBehaviour
{
    public CharacterSystem characterSystem;
    public EquipmentSystem equipSystem;
    public void CharacterDraw()
    {
        int heroClass;
        heroClass =  Random.Range(1,9);
        (characterSystem.equippeditem, characterSystem.shape) = MyItemListContents.Instance.createMyCharacter(heroClass);
        equipSystem.ChangeEquipment();
        GameObject.Find("Canvas_HeroDraw").GetComponent<InventorySystem>().DrawCharacterInfo_HeroDraw(characterSystem.equippeditem);
    }

    public void SaveCharacter(int slot)
    {

        MyItemListContents.Instance.insertMyCharacter(
            characterSystem.equippeditem,
            characterSystem.shape,
            slot);
    }

}

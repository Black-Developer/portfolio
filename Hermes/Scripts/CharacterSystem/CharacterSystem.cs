using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSystem : MonoBehaviour
{
    public int slot;
    public EquippedItem equippeditem;
    public CharacterShape shape;
    public void Start()
    {
        if (slot == 0)
        {
            return;
        }
        equippeditem = Managers.Data.myCharacterDict[slot];
        shape = Managers.Data.myCharacterShapeDict[slot];
    }
    public void LoadSlot(int index)
    {
        slot = index;
        equippeditem = Managers.Data.myCharacterDict[slot];
        shape = Managers.Data.myCharacterShapeDict[slot];
    }
}

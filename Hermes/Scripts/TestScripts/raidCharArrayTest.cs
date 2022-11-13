using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raidCharArrayTest : MonoBehaviour
{
    public int[] testArray = new int[4];
    public int[,] bagArray = new int[4, 2];
    public GameObject[] middleCharacterList;
    public GameObject[] leftCharacterList;
    public GameObject[] rightCharacterList;

    public List<GameObject> mappingList = new List<GameObject>();

    public int startIndex;

    void Start()
    {
        testArray = Managers.Data.Ingame.GetRaidCharacterArray();
        bagArray = Managers.Data.Ingame.GetCharacterBag();


        for (int i = 0; i < 4; i++)
        {
            if (testArray[i] != -1)
            {
                switch (startIndex)
                {
                    case 0:
                        middleCharacterList[i].gameObject.GetComponent<CharacterSystem>().slot = testArray[i];
                        break;
                    case 1:
                        leftCharacterList[i].gameObject.GetComponent<CharacterSystem>().slot = testArray[i];
                        break;
                    case 2:
                        rightCharacterList[i].gameObject.GetComponent<CharacterSystem>().slot = testArray[i];
                        break;
                }
            }
        }
    }
    
    public void MappingEnd()
    {
        for (int i = 0; i < 4; i++)
        {
            if (testArray[i] != -1)
            {
                switch (startIndex)
                {
                    case 0:
                        middleCharacterList[i].gameObject.GetComponent<CharacterMovement>().m_Waypoints = mappingList;
                        break;
                    case 1:
                        leftCharacterList[i].gameObject.GetComponent<CharacterMovement>().m_Waypoints = mappingList;
                        break;
                    case 2:
                        rightCharacterList[i].gameObject.GetComponent<CharacterMovement>().m_Waypoints = mappingList;
                        break;
                }
            }
            else
            {
                switch (startIndex)
                {
                    case 0:
                        middleCharacterList[i].gameObject.SetActive(false);
                        break;
                    case 1:
                        leftCharacterList[i].gameObject.SetActive(false);
                        break;
                    case 2:
                        rightCharacterList[i].gameObject.SetActive(false);
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (testArray[i] != -1)
            {
                switch (startIndex)
                {
                    case 0:
                        middleCharacterList[i].gameObject.GetComponent<EquipmentSystem>().ChangeEquipment();
                        break;
                    case 1:
                        leftCharacterList[i].gameObject.GetComponent<EquipmentSystem>().ChangeEquipment();
                        break;
                    case 2:
                        rightCharacterList[i].gameObject.GetComponent<EquipmentSystem>().ChangeEquipment();
                        break;
                }
            }
        }
    }
}

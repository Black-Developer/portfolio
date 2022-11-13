using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[System.Serializable]
public class bagArrayTest : MonoBehaviour
{
    public Dictionary<int, Information>[] inventoryArray = new Dictionary<int, Information>[4];

    [SerializeField]
    public Dictionary<int, Information> inventory1 = new Dictionary<int, Information>();
    [SerializeField]
    public Dictionary<int, Information> inventory2 = new Dictionary<int, Information>();
    [SerializeField]
    public Dictionary<int, Information> inventory3 = new Dictionary<int, Information>();
    [SerializeField]
    public Dictionary<int, Information> inventory4 = new Dictionary<int, Information>();
    void Start()
    {
        inventoryArray[0] = new Dictionary<int, Information>();
        inventoryArray[1] = new Dictionary<int, Information>();
        inventoryArray[2] = new Dictionary<int, Information>();
        inventoryArray[3] = new Dictionary<int, Information>();
    }

    void Update()
    {
        //inventoryArray = Managers.Data.Ingame.GetTestCode();
        inventoryArray[0] = inventory1;
        inventoryArray[1] = inventory2;
        inventoryArray[2] = inventory3;
        inventoryArray[3] = inventory4;
    }
}

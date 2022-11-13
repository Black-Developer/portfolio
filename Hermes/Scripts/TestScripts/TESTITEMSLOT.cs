using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTITEMSLOT : MonoBehaviour
{
    public List<int> itemId = new List<int>();
    public List<int> itemCount = new List<int>();
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        itemId = new List<int>();
        itemCount = new List<int>();
        foreach (var item in MyItemListContents.Instance.myHelmetItemDict)
        {
            itemId.Add(item.Value.id);
            itemCount.Add(item.Value.count);

        }
    }
}

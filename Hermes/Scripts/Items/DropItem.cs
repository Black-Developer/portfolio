using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public int id; //이 스크립트가 붙은 아이템의 고유 ID.
    public int quantity = 1; //이 스크립트가 붙은 아이템을 흭득할 시 증가할 수량 (일반적으로 1개)

    public int slotnum; // 흭득 코드는 캐릭터에 있으면 되는데 일단 임시로 여기붙여놔서 슬롯넘버 필요함

    public Item item;

    public int[] raidSlots = new int[4];
    void Start()
    {
        item = GetComponent<Item>();
        id = item.index;
        raidSlots = Managers.Data.Ingame.GetRaidCharacterArray();
    }

    public void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("Player"))
        {
            slotnum = other.gameObject.GetComponent<CharacterSystem>().slot;
            AcquiredItem();
            Destroy(this.gameObject);
        }
    }

    private int LoopRaidSlots(int num)
    {
        int i = 0;
        for (; i < raidSlots.Length; i++)
        {
            if (raidSlots[i] == num) break;
        }
        return i;
    }
    private void AcquiredItem()
    {
        Managers.Data.Ingame.IncreaseItemInInventory(slotnum, id, quantity);
        //print("slotNum: " + slotnum + ", id: " + id + ", quantity: " + quantity);
    }
}

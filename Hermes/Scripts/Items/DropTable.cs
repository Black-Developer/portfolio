using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    [Header("떨어지는 아이템")]
    public GameObject[] rareDropObj;
    public GameObject[] normalDropObj;
    [Header("전체 드랍 확률")]
    public const float maxDrops = 100.0f;
    [Header("아이템 확률")]
    public float rarePersent = 1.0f;
    public float normalPersent = 1.0f;
    public void Drop()
    {
        float persent = Random.Range(0, maxDrops);
        if(persent <= normalPersent)
        {
            Debug.Log("노멀 등급 아이템 드랍");
            StartCoroutine(ItemDrop(normalDropObj));
        }
        else if(normalPersent < persent && persent <= normalPersent+rarePersent)
        {
            Debug.Log("레어 등급 아이템 드랍");
            StartCoroutine(ItemDrop(rareDropObj));
        }
    }
    IEnumerator ItemDrop(GameObject[] obj)
    {
        int count = Random.Range(0, obj.Length);

        for(int i = 0; i < count; i++)
        {
            Instantiate(obj[Random.Range(0, obj.Length)],transform.position + new Vector3(0,0.5f,0),Quaternion.identity);
        }


        yield return null;
    }
}

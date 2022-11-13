using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacter : MonoBehaviour
{
    [SerializeField]
    private GameObject[] slot;
   void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public GameObject[] LoadCharacter()
    {
        return slot;
    }
    public void DeleteThisObject()
    {
        Destroy(this);
    }
}

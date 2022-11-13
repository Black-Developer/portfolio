using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public string name;
    public int hp;
    public int armor;
    public int damage;
    public float attackSpeed;
    public float accuracy;
    public float critical;
    public EquipmentSystem equipmentSystem;
    // Start is called before the first frame update

    void Start()
    {
        equipmentSystem = GetComponent<EquipmentSystem>();
        equipmentSystem.LoadStats(
            hp,
            damage,
            armor,
            attackSpeed,
            critical,
            accuracy
            );
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

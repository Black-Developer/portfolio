using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    public int hp;
    public int damage;
    public int armor;
    public float attackSpeed;
    public float critical;
    public float accuracy;

    public void LoadStats(int _hp, int _damage, int _armor, float _attackSpeed, float _critical, float _accuracy)
    {
        _hp += hp;
        _damage += damage;
        _armor += armor;
        _attackSpeed += attackSpeed;
        _critical += critical;
        _accuracy += accuracy;
    }
}
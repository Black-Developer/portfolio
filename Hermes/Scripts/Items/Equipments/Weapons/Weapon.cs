using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    IsVaild = -1,
    None = 0,
    OneHandedSword = 1,
    TwoHandedSword,
    Axe,
    Spear
}

public class Weapon : Equipment
{
    [HideInInspector]
    public WeaponTypeConfig typeConfig;
    public AudioSource weaponSound;
    public WeaponTrail trail;
    public WeaponType type;
    public ParticleSystem hitEffect;
    private float range;
    public WeaponColliderController weaponCollider;

    private void Start()
    {
        weaponCollider = GetComponent<WeaponColliderController>();
        weaponSound = GetComponent<AudioSource>();
        SetWeaponType();
    }
    private void SetWeaponType()
    {
        switch (type)
        {
            case WeaponType.OneHandedSword:
                range = typeConfig.OneHandedSwordRange;
                break;
            case WeaponType.TwoHandedSword:
                range = typeConfig.TwoHandedSwordRange;
                break;
            case WeaponType.Axe:
                range = typeConfig.AxeRange;
                break;
            case WeaponType.Spear:
                range = typeConfig.SpearRange;
                break;
        }
    }
    public void WeaponEffectOn()
    {
        if (trail != null)
        {
            Debug.Log("이펙트 온");
            trail.TrailOn();
        }
    }
    public void WeaponEffectOff()
    {
        if (trail != null)
        {
            Debug.Log("이펙트 오프");
            trail.TrailOff();
        }
    }

    public float Range
    {
        get
        {
            return range;
        }
    }

    public void HitEffect()
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
    }
}

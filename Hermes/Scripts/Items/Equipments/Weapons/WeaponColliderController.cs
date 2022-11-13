using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderController : MonoBehaviour
{
    public BoxCollider colliderWeapon;
    private Weapon weapon;
    public string targetLayer;
    private void Start()
    {
        weapon = GetComponent<Weapon>();
        colliderWeapon = GetComponent<BoxCollider>();
        colliderWeapon.enabled = false;
    }
    public void AttackStart()
    {
        colliderWeapon.enabled = true;
    }
    public void AttackEnd()
    {
        colliderWeapon.enabled = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(targetLayer))
        {
            other.gameObject.GetComponent<HitBox>().OnHit(weapon);
           if( other.gameObject.GetComponent<AiAgent>().targetTransform == null)
            {
                other.gameObject.GetComponent<AiAgent>().targetTransform = GetComponentInParent<Transform>();
            }
        }
    }
}

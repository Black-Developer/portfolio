using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAnimatorController : MonoBehaviour
{
    public Animator animator;
    public AiAgent agent;
    public EquipmentSystem equipmentSystem;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<AiAgent>();
        animator = gameObject.GetComponent<Animator>();
        equipmentSystem = gameObject.GetComponent<EquipmentSystem>();

    }
    public void SetWeaponType()
    {
            if (agent.weapon != null)
            {
                animator.SetInteger("WeaponType", ((int)agent.weapon.type));
            }
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void MoveAnimation(float _x, float _z)
    {
        animator.SetBool("Moving", true);
        animator.SetFloat("Velocity X", _x);
        animator.SetFloat("Velocity Z", _z);

    }
    public void TargetingAnimation()
    {
        animator.SetBool("Targeting", true);
    }public void UnTargetingAnimation()
    {
        animator.SetBool("Targeting", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    [HideInInspector]
    public AiAnimatorController animatorController;
    [HideInInspector]
    public EquipmentSystem equipmentSystem;
    //[HideInInspector]
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    //[HideInInspector]
    public Transform targetTransform;
    public Transform dropTransform;
    [HideInInspector]
    public Rigidbody rigidbody;

    public AudioSource hitSound;

    public Health health;

    public Weapon weapon;

    public AiStateID initialState;
    public AiAgentConfig config;
    public AiStateMachine stateMachine;
    public CharacterMovement characterMovement;
    public DropTable dropTable;
    public CapsuleCollider collider;
    // Start is called before the first frame update
    void Start()
    {
        targetTransform = null;
        rigidbody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterMovement = GetComponent<CharacterMovement>();
        equipmentSystem = GetComponent<EquipmentSystem>();
        animatorController = GetComponent<AiAnimatorController>();
        health = GetComponent<Health>();
        hitSound = GetComponent<AudioSource>();
        dropTable = GetComponent<DropTable>();
        collider = GetComponent<CapsuleCollider>();
        #region StateMachine
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChaseTargetState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiAttackState());
        stateMachine.RegisterState(new AiMoveState());
        stateMachine.RegisterState(new AiTargetingState());
        stateMachine.RegisterState(new AiHitState());
        stateMachine.RegisterState(new AiDieState());
        stateMachine.RegisterState(new AiGetItem());
        #endregion StateMachine

        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    public void Update()
    {
        animatorController.SetWeaponType();
        stateMachine.Update();
    }

    public void AttackStart()
    {
        weapon.weaponCollider.AttackStart();
    }

    public void AttackEnd()
    {
        weapon.weaponCollider.AttackEnd();
    }
    public void TrailStart()
    {
        weapon.WeaponEffectOn();
    }
    public void TrailEnd()
    {
        weapon.WeaponEffectOff();
    }
    public void DropWeapon()
    {
        weapon.gameObject.SetActive(false);
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void DisableCollision()
    {
        collider.enabled = false;
    }
    public void FootR() { }
    public void FootL() { }
}
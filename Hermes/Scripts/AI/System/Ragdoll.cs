using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ragdoll : MonoBehaviour
{
    public Rigidbody[] rigidbodies;
    public Animator animator;

    private void Start()
    {
        try
        {
            rigidbodies = GetComponentsInChildren<Rigidbody>();
            animator = GetComponent<Animator>();
        }
        catch(Exception error)
        {
            throw new Exception("Component is Null" + "error code :" + error);
        }
        disableRagdoll();
    }
    // 레그돌 활성화 == 캐릭터가 쓰러짐
    public void enableRagdoll()
    {
        foreach(var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
        animator.enabled = false;
    }
    // 레그돌 비활성화 == 캐릭터가 정상적으로 동작함
    public void disableRagdoll()
    {
        foreach(var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        animator.enabled = true;
       
    }
}

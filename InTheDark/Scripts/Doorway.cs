using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    public GameObject collisionDoor;
    public bool connectNextRoom;

    private void Start()
    {
        connectNextRoom = false;
    }
    private void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position, transform.rotation * Vector3.forward);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Doorway"))
        {
            connectNextRoom = true;
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Doorway")
    //    {
    //        Debug.Log("방문이 겹쳤습니다.");
    //        collisionDoor = collision.gameObject;
    //    }
    //}
    public bool GetCollisionDoorway()
    {
        return connectNextRoom;
    }
}

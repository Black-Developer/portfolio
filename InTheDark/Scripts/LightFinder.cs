using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LightFinder : MonoBehaviourPunCallbacks
{
    public GameObject light;
    private CapsuleCollider collider;
    PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        pv = GetComponent<PhotonView>();
    }

    //[PunRPC]
    void LightOn()
    {

        light.gameObject.SetActive(true);
    }
    //[PunRPC]
    void LightOff()
    {
        light.gameObject.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LightOn();
            pv.RPC("LightOn", RpcTarget.All);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LightOff();
            pv.RPC("LightOff", RpcTarget.All);
        }
    }
}

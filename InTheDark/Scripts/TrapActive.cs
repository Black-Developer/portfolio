using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class TrapActive : MonoBehaviourPunCallbacks
{
    PhotonView pv;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<TrapInteraction>().PlayerTraped();
            pv.RPC("DestroySelf", RpcTarget.All);
        }
    }

    [PunRPC]
    void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}

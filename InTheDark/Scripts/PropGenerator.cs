using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PropGenerator : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    [SerializeField]
    public GameObject[] propObjs;
    public PhotonView pv;
    void Start()
    {
        pv = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            int random = Random.RandomRange(0, 1000);
            pv.RPC("SpawnProps", RpcTarget.All, random);
        }
    }

    [PunRPC]
    public void SpawnProps(int masterSeed)
    {
        Random.seed = masterSeed;
        foreach (GameObject propObj in propObjs)
        {
            if (propObj.CompareTag("Item"))
            {
                if (98 < Random.RandomRange(0, 100))
                {
                    propObj.SetActive(true);
                }
                else
                {
                    propObj.SetActive(false);
                }
            }
            else
            {
                if (95 < Random.RandomRange(0, 100))
                {
                    propObj.SetActive(true);
                }
                else
                {
                    propObj.SetActive(false);
                }
            }
        }
    }
}

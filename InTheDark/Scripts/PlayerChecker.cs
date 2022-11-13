using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class PlayerChecker : MonoBehaviourPunCallbacks
{
    PhotonView pv;
    int playerCount;
    private void Start()
    {
        playerCount = 0;
        pv = GetComponent<PhotonView>();
        pv.RPC("JoinPlayer", RpcTarget.All);
    }

    [PunRPC]
    void JoinPlayer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playerCount ++;
            if(playerCount == 2)
            {
                pv.RPC("GameStart", RpcTarget.All);
            }

            
        }
    }
    [PunRPC]
    void GameStart()
    {
        SceneManager.LoadScene("PlayScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SetTrap : MonoBehaviourPunCallbacks
{
    PhotonView pv;
    public Camera camera;
    public GameObject Checker;

    SetTrap settrap;
    private void Start()
    {
        settrap = GetComponent<SetTrap>();
        pv = GetComponent<PhotonView>();
        camera = GetComponentInChildren<Camera>();
    }
    [PunRPC]
    void PRC_SpawnTrap(Vector3 clickPosition)
    {
        Instantiate(Checker, clickPosition, Checker.transform.rotation);
    }

    void Update()
    {

        Vector3 clickedPosition = Vector3.zero;
        Plane plane = new Plane(Vector3.up, 0);

        float distance;
        if (camera != null)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                clickedPosition = ray.GetPoint(distance);
            }
            UsingAttack(clickedPosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    Instantiate(Checker, clickedPosition, Checker.transform.rotation);
                }
            }
        }
    }
    void UsingAttack(Vector3 clickPosition)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pv.RPC("PRC_SpawnTrap", RpcTarget.All, clickPosition);

            }
            return;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ChaserPlayer : MonoBehaviour
{
    //private Rigidbody rigidbody;
    //private Camera camera;
    //private CharacterController controller;
    //private Vector3 playerVelocity;
    //private const float playerSpeed = 3.0f;
    //public GameObject Checker;
    //public RectTransform panelTransform;
    //public PhotonView pv;
    //SetTrap settrap;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    settrap = GetComponent<SetTrap>();
    //    pv = GetComponent<PhotonView>();
    //    rigidbody = GetComponent<Rigidbody>();
    //    controller = gameObject.GetComponent<CharacterController>();
    //    camera = GetComponentInChildren<Camera>();
    //}
    //
    //// Update is called once per frame
    //void Update()
    //{
    //    //Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    //    //controller.Move(moveVector * Time.deltaTime * playerSpeed);
    //
    //    Vector3 clickedPosition= Vector3.zero;
    //    Plane plane = new Plane(Vector3.up, 0);
    //
    //    float distance;
    //
    //    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
    //    if(plane.Raycast(ray, out distance))
    //    {
    //        clickedPosition = ray.GetPoint(distance);
    //    }
    //    //UsingAttack(Checker, clickedPosition);
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        Instantiate(Checker, clickedPosition, Checker.transform.rotation);
    //    }
    //}
    //void UsingAttack(GameObject instance, Vector3 clickPosition)
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Rect rect = new Rect(panelTransform.rect);
    //        Debug.Log(rect.x + ", " + rect.y);
    //        Debug.Log(panelTransform.rect.x + ", " + panelTransform.rect.y);
    //
    //        settrap.SpawnTrap(instance,clickPosition);
    //
    //    }
    //    return;
    //}
    
}

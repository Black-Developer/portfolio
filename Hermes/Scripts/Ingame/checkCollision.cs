using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCollision : MonoBehaviour
{
    //이스크립트는 전투나 특정 줌인 줌아웃이 필요한 장소에 부착해야합니다.
    private CameraMoving cameraManager;
    void Start()
    {
        cameraManager = GameObject.Find("GameManager").GetComponent<CameraMoving>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        cameraManager.checkBattleArea(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        cameraManager.checkBattleArea(false);
    }
}

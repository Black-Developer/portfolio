using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCollision : MonoBehaviour
{
    //�̽�ũ��Ʈ�� ������ Ư�� ���� �ܾƿ��� �ʿ��� ��ҿ� �����ؾ��մϴ�.
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

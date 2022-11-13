using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coliCheckPlayer : MonoBehaviour
{
    public GameObject exitManager;
    public bool isFirstEnter;
    public int exitEreaNum;
    public void Start()
    {
        exitManager = GameObject.Find("ExitManager");
        isFirstEnter = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (isFirstEnter) return;
            isFirstEnter = true;
            exitManager.GetComponent<ExitManager>().CameraSetting(exitEreaNum); //0 Áß¾Ó, 1¿ÞÂÊ, 2¿À¸¥ÂÊ
            StartCoroutine(CameraMove(exitEreaNum));
        }
    }



    IEnumerator CameraMove(int num)
    {
        yield return new WaitForSeconds(1.0f);
        exitManager.GetComponent<ExitManager>().FindCameraMovingSC(num).coroutineAllowed = true;
    }
}

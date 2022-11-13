using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartScene : MonoBehaviour
{
    Text flashingText;
    bool isflashing;
    Camera MainCamera;
    bool isClickStart;
    public GameObject startPannel;
    public GameObject titlePannel;

    private bool mi;
    void Start()
    {
        isflashing = true;
        isClickStart = false;
        flashingText = GetComponent<Text>();
        StartCoroutine(BlinkText());
        MainCamera = Camera.main;
        startPannel.SetActive(true);
        titlePannel.SetActive(false);
        mi = true;
    }
    public void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<CameraMoving>().isAlreadyStart)
        {
            AlreadyStart();
        }
    }
    private void AlreadyStart()
    {
        MainCamera.transform.position = new Vector3(18.79423f, 2.6687f, -305.9738f);
        MainCamera.transform.rotation = Quaternion.Euler(new Vector3(9.16f, 165.993f, 0));
        //MainCamera.transform.LookAt(new Vector3(9.16f, 165.993f, 0));
        startPannel.SetActive(false);
        titlePannel.SetActive(true);
    }
    public IEnumerator BlinkText()
    {
        float alpha = 1.0f;
        while (isflashing)
        {

            if (mi)
            {
                if(alpha == 0.0f) mi = false;
                alpha = Mathf.Clamp(alpha - Time.deltaTime, 0.0f, 1.0f);
            }
            else
            {
                if(alpha == 1.0f) mi = true;
                alpha = Mathf.Clamp(alpha + Time.deltaTime, 0.0f, 1.0f);
            }
            flashingText.color = new Color(flashingText.color.r, flashingText.color.g, flashingText.color.b,
                alpha);

            yield return null;
            //flashingText.text = "";
            //yield return new WaitForSeconds(.35f);
            //flashingText.text = "Touch To Start";
            //yield return new WaitForSeconds(.35f);
        }
    }
    public void ActiveTitlePannel()
    {
        titlePannel.SetActive(true);
    }
    public void ChangeCameraSetting()
    {
        isflashing = false;
        startPannel.SetActive(false);
        MainCamera.GetComponent<FollowCurve>().coroutineAllowed = true;
        GameObject.Find("GameManager").GetComponent<CameraMoving>().isAlreadyStart = true;
    }
}

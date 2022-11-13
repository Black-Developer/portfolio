using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    //기본적으로 castle에서 사용되는 스크립트임
    public GameObject cameraList;
    public Camera playerCamera;
    public Camera selectCamera;
    public bool isRotate;
    public bool isStartGame;

    public bool isPlayerInArea;
    public Vector3 dis;


    //==========Title에서 사용되는 변수임
    public bool isAlreadyStart;

    //==========카메라가 캐릭터들 follow를 위해
    public Transform startPoint;

    void Start()
    {
        //CameraSetting();
        isAlreadyStart = false;
        isStartGame = false;
        isRotate = false;
        isPlayerInArea = false;
        dis = new Vector3(-12.3f, 10.0f, -4.1f);
        //mainCamera = Camera.main;

    }
    void Update()
    {
        if (!isStartGame) return;


        followCharacter();
        //CheckEyesight();
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShakeCamera(playerCamera);
        }
    }
    public void CameraSetting()
    {
        cameraList = GameObject.Find("CameraList");
        if (cameraList != null)
        {
            playerCamera = cameraList.transform.GetChild(0).GetComponent<Camera>();
            selectCamera = cameraList.transform.GetChild(1).GetComponent<Camera>();
        }
    }
    public void startGame()
    {
        isStartGame = true;
        playerCamera.gameObject.SetActive(true);
        selectCamera.gameObject.SetActive(false);
    }

    public void checkBattleArea(bool isArea)
    {
        isPlayerInArea = isArea;
    }
   
    #region follow character
    public void followCharacter() //캐릭터 따라가기
    {
        Vector3 charactersAVGPosition = FindStartPointAVG();
        if (isPlayerInArea)
        {
            dis = Vector3.Lerp(dis, new Vector3(-7.51f, 6.1f, -2.5f), Time.deltaTime * 2);
        }
        else
        {
            dis = Vector3.Lerp(dis, new Vector3(-12.3f, 10.0f, -4.1f), Time.deltaTime * 2);
        }
        print(dis);
        regulateCameraDistance(charactersAVGPosition, dis);

    }
    public void SetStartPointTr(Transform tr)
    {
        startPoint = tr;
    }
    private Vector3 FindStartPointAVG()
    {
        Vector3 po = new Vector3();
        int count = 0;
        for (int i = 0; i < startPoint.childCount; i++)
        {
            if (startPoint.GetChild(i).gameObject.activeSelf)
            {
                po += startPoint.GetChild(i).position;
                count++;
            }
        }
        po = new Vector3(po.x / count, po.y / count, po.z / count);
        return po;
    }
    private void regulateCameraDistance(Vector3 po, Vector3 dis)
    {
        playerCamera.transform.position = po + dis;
    }
    private void regulateCameraRotation(GameObject go, float theta = 0)
    {

    }
    //private Transform findCharacter() //따라갈 캐릭터 선택
    //{
    //    //살아있는 특정 캐릭터를 매번 설정한다?
    //    //살아있는 모든 캐릭터들의 평균값 사용
        
    //    return GameObject.Find("ModularCharacters").transform;
    //}
    #endregion
    #region shake camera
    public void ShakeCamera(Camera camera)
    {
        Vector3 originPos = camera.transform.localPosition;
        Quaternion originRot = camera.transform.localRotation;
        StartCoroutine(ShakeCamera(originPos, originRot, 0.07f, 0.2f, 0.3f));
    }

    public IEnumerator ShakeCamera(Vector3 originPos, Quaternion originRot, float duration = 0.1f,
                                    float magnitudePos = 0.8f,
                                    float magnitudeRot =0.1f)
    {
        float passTime = 0.0f;

        while (passTime < duration)
        {
            Vector3 shakePos = Random.insideUnitSphere;
            playerCamera.transform.localPosition = originPos + shakePos * magnitudePos;

            if(isRotate)
            {
                Vector3 shakeRot = new Vector3(0, 0, Mathf.PerlinNoise(Time.time * magnitudeRot, 0.0f));
                playerCamera.transform.localRotation = Quaternion.Euler(shakeRot);
            }
            passTime += Time.deltaTime;
            yield return null;

        }
        playerCamera.transform.localPosition = originPos;
        playerCamera.transform.localRotation = originRot;
    }
    #endregion
}

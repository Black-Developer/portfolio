using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CastleMappingManager : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject ExitManager;

    //------------지역 순서
    public List<GameObject> mapping = new List<GameObject>();
    private int areaCount;
    public GameObject startArea; //게임 내 초기 시작 지역
    public GameObject exitArea; //게임 내 탈출구 지역


    //------------시작 지역
    public GameObject[] startPoint;
    public GameObject[] exitPoint;
    //------------엔드 지역
    public GameObject[] exitAreaList;



    //------------각 지역별 캐릭터
    [Header("캐릭터 목록")]
    public GameObject[] middleCharacter;
    public GameObject[] leftCharacter;
    public GameObject[] rightCharacter;

    //------------캐릭터 매니저
    public GameObject characterManager;

    //------------지역 선택 플로팅 시작
    public GameObject selectAreaPanelWithCancel;

    private RaycastHit hit;
    private Vector3 uiPosition;
    private GameObject hitArea;

    public Transform selectBehaviour;
    public GameObject mainCanvas;
    private Transform selectedArea;

    private bool isSelectedFinish;

    //------------맵핑 마지막 지역 이미지
    public Sprite exitImage;

    //------------터치 시작
    private float distance;
    private float wheelspeed = 100.0f;
    private Vector3 offset = new Vector3(57.67f, 120, -29.85f);
    Vector2 startPos, deltaPos, nowPos;
    const float dragAccuracy = 50f;
    private bool isMove;

    //-----------게임 시작
    private bool isStart;
    //-----------그외 UI
    public GameObject ExitSelectArea;
    public GameObject FinishSelected;

    void Start()
    {
        RandomStartArea();
        mainCamera = Camera.main.GetComponent<Camera>();
        areaCount = GameObject.Find("Area").transform.childCount;

        isSelectedFinish = false;
        isMove = true;
        isStart = false;
        mainCamera.transform.position = offset;
        mainCamera.transform.rotation = Quaternion.Euler(60, 0, 0);
        //transform.parent.GetChild(0).GetComponent<CameraMoving>().CameraSetting();
        GameObject.Find("GameManager").GetComponent<CameraMoving>().CameraSetting();
    }


    void Update()
    {
        if (isStart) return;
        ClickArea(); //지역선택
        SelectAreaBehaviour();//행동선택 플로팅
    }
    void LateUpdate()
    {
        MouseMove();
    }
    public void GameStart() //게임 시작시 실행
    {
        //FinishSelected의 Start가 제어함
        for (int i = 0; i < 4; i++)
        {
            mainCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }
        mainCanvas.transform.GetChild(4).GetComponent<PauseSystem>().StartGame();
        GameObject.Find("GameManager").GetComponent<CameraMoving>().startGame();

        GameObject.Find("AreaPoint").SetActive(false);

        EraseNavMeshLine();
        characterManager.GetComponent<raidCharArrayTest>().mappingList = GetMappingAreaList();
        characterManager.GetComponent<raidCharArrayTest>().MappingEnd();

        ExitManager.GetComponent<ExitManager>().FindExitGameObject();
    }
    #region GetMappingAreaList
    public List<GameObject> GetMappingAreaList()
    {
        return mapping;
    }
    public GameObject ReturnLastMappingGameObject()
    {
        if(mapping.Count == 0) return null;
        return mapping[mapping.Count - 1];
    }
    #endregion
    #region randomArea with start, end
    private void RandomStartArea()
    {
        Vector3[] startPosition = new Vector3[3] { new Vector3(57.18f,2.41f,-7.86f),
                                                new Vector3(-8.67f,2.41f,71.97f),
                                                new Vector3(122.43f,2.41f,71.03f)};

        int rand = Random.Range(0, startPosition.Length);


        //--------end지점과 start지점 구분
        for (int i = 0; i < 3; i++)
        {
            if (i != rand)
            {
                exitPoint[i].transform.GetChild(0).gameObject.SetActive(false);
                exitPoint[i].transform.GetChild(1).gameObject.SetActive(true);
                continue;
            }
            exitAreaList[i].gameObject.SetActive(false);
            exitAreaList[i] = null;
        }
        //--------------------------------



        characterManager.GetComponent<raidCharArrayTest>().startIndex = rand;
        startPoint[rand].SetActive(true);



        GameObject.Find("GameManager").GetComponent<CameraMoving>().SetStartPointTr(startPoint[rand].transform);
        startArea.transform.position = startPosition[rand];
        switch (rand)
        {
            case 0:
                for(int i = 0; i < 4; i++)
                {
                    middleCharacter[i].SetActive(true);
                }
                break;
            case 1:
                for(int i = 0; i < 4; i++)
                {
                    leftCharacter[i].SetActive(true);
                }
                break;
            case 2:
                for(int i = 0; i < 4; i++)
                {
                    rightCharacter[i].SetActive(true);
                }
                break;
        }
    }
    #endregion
    #region ClickArea
    private void ClickArea()
    {
        if (isSelectedFinish) return; //지역선택이 다되면 선택못함

        if (!Input.GetMouseButtonDown(0)) return;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit)) return;
        if (hit.transform.tag != "SelectArea") return;
        if (selectAreaPanelWithCancel.activeSelf) return;


        if (mapping.Count >= 1) //이미 맵핑된 친구는 맵핑이 안되도록 리턴시킴
        {
            for (int i = 0; i < mapping.Count - 1; i++)
            {
                if (hit.transform.parent.gameObject == mapping[i]) return;
            }
        }
        if (isLastMappingAreaEqualExitAreaList()) //맵핑이 다 안차도 탈출구를 맵핑했으면 무조건 탈출구만 눌러야 함 (탈출구를 이어 맵핑할 수는 없다)
        {
            if (mapping[mapping.Count - 1] != hit.transform.parent.gameObject) return;
        }

        hitArea = hit.transform.gameObject;

        //Debug.DrawRay(ray.origin, ray.direction * 200, Color.red, 5f);
    }
    private void SelectAreaBehaviour()
    {
        if (!hitArea) return;


        uiPosition = Camera.main.WorldToScreenPoint(hitArea.transform.position);
        uiPosition.z = 0.0f;
        float yPos = uiPosition.y + Screen.height / 5;
        selectAreaPanelWithCancel.transform.position = new Vector3(uiPosition.x, yPos, uiPosition.z);
        isActivePannel_SelectAreaPanelWithCancel(true);
    }
    #endregion
    #region Select or cancle
    public void ReSelect()
    {
        isMove = true;
        isSelectedFinish = false;
        mainCanvas.transform.GetChild(0).gameObject.SetActive(true);
        ExitSelectArea.SetActive(true);
        FinishSelected.SetActive(false);
    }
    public void SelectedCancel()
    {
        if (mapping.Count == 0)
        {
            hitArea = null;
            isActivePannel_SelectAreaPanelWithCancel(false);
            return;
        }
        if (mapping[mapping.Count - 1] == hitArea.transform.parent.gameObject) //클릭한 지역이 마지막으로 선택한 지역인지 체크
        {
            if (mapping.Count >= 2)
            {
                LineRenderer lr = mapping[mapping.Count - 2].transform.GetChild(0).GetComponent<LineRenderer>();
                lr.positionCount = 0;
                
            }
            if(mapping.Count == 1)
            {
                LineRenderer lr = startArea.GetComponent<LineRenderer>();
                lr.positionCount = 0;
            }
            mapping.RemoveAt(mapping.Count - 1);
            Destroy(GameObject.Find("selected " + hitArea.transform.parent.name));
        }


        hitArea = null;
        isActivePannel_SelectAreaPanelWithCancel(false);
    }
    public void SelectedArea(Sprite image) //파밍 /휴식 선택시 나오는 플로팅 화면
    {
        if (isHitAreaEqualLastMappingArea()) return; //이미 선택된 지역이면 return
        if (lastMappingAreaIsExit()) return; //5칸을 채우면 나머지 하나는 무조건 탈출구로 유도하는 안내 문구 띄워야함

        

        if (!IsSelectExitAreaBeforeFullMapping()) 
        {
            ChangeSelectBehaviourContents(image);
            EditFinishSelectedImage(image);
        }
        else
        {
            ChangeSelectBehaviourContents(exitImage);
            EditFinishSelectedImage(exitImage);
        }


        AddThisAreaInmappingList(hitArea);
        RegistImageInCastleMap(hitArea);

        DrawLine();

        hitArea = null;
        isActivePannel_SelectAreaPanelWithCancel(false);
        Selected(); //지역 선택이 완료 되었으면 나오는 거임
    }
    private bool isHitAreaEqualLastMappingArea()
    {
        if(mapping.Count == 0) return false;
        if (hitArea.transform.parent.gameObject == mapping[mapping.Count - 1]) return true;
        return false;
    }
    private bool isActivePannel_SelectAreaPanelWithCancel(bool isActive)
    {
        selectAreaPanelWithCancel.SetActive(isActive);

        return selectAreaPanelWithCancel.activeSelf;
    }
    private void RegistImageInCastleMap(GameObject go)
    {
        uiPosition = Camera.main.WorldToScreenPoint(go.transform.position);
        Transform selected = Instantiate(selectBehaviour,
            uiPosition,
            Quaternion.identity
            );
        selected.name = "selected " + go.transform.parent.name;

        selected.parent = mainCanvas.transform.GetChild(0);
    }
    private void EraseNavMeshLine()
    {
        startArea.GetComponent<LineRenderer>().positionCount = 0;
        for (int i = 0; i < mapping.Count - 1; i++)
        {
            mapping[i].transform.GetChild(0).GetComponent<LineRenderer>().positionCount = 0;
        }
    }
    private bool IsSelectExitAreaBeforeFullMapping()  //맵핑 6개를 찍기전 탈출구를 선택했으면 true
    {
        if (!isHitAreaEqualExitAreaList()) return false;
        //addExitAreaToMapping();
        return true;
    }
    private bool lastMappingAreaIsExit() //맵핑이 5개일때 이후 맵핑은 반드시 탈출구임을 메세지로 경고
    {
        if (mapping.Count != 5) return false;
        if (isHitAreaEqualExitAreaList()) return false;
        //마지막은 반드시 탈출구여야 한다는 안내문구와 강제로 마지막을 탈출구로 연결
        hitArea = null;
        isActivePannel_SelectAreaPanelWithCancel(false);
        printWarning_MSG();
        return true;
    }
    private bool isHitAreaEqualExitAreaList() //선택한 지역이 탈출지역중 하나라면 true
    {
        for (int i = 0; i < exitAreaList.Length; i++)
        {
            if(exitAreaList[i] == null) continue;
            if (hitArea == exitAreaList[i]) return true;
        }

        return false;
    }
    private bool isLastMappingAreaEqualExitAreaList() //맵핑에 등록된 마지막 지역이 탈출 지역인지 체크해서 맞으면 true 반환
    {
        if (mapping.Count == 0) return false;
        for (int i = 0; i < exitAreaList.Length; i++)
        {
            if (exitAreaList[i] == null) continue;
            if (mapping[mapping.Count - 1] == exitAreaList[i].transform.parent.gameObject) return true;
        }

        return false;
    }
    private void printWarning_MSG()
    {
        ReturnPopup_lastSelectAreaWarringGameObject().SetActive(true);
    }
    private GameObject ReturnPopup_lastSelectAreaWarringGameObject()
    {
        return mainCanvas.transform.GetChild(5).gameObject;
    }
    private void Selected() //모든 지역 선택 완료시 다음 플로팅 화면
    {
        if (!isLastMappingAreaEqualExitAreaList()) return;

        isMove = false;      //화면 움직임 방지
        isSelectedFinish = true; //맵핑지역 클릭 방지
        mainCanvas.transform.GetChild(0).gameObject.SetActive(false); //맵핑 지역이 전투인지 파밍인지 알려주는 게임오브젝트들이 담긴 parent임
        ExitSelectArea.SetActive(false);
        FinishSelected.SetActive(true);

    }
    private bool IsFullMappingList()
    {
        if(mapping.Count == 6) return true;
        return false;
    }
    private void DrawLine()
    {
        if (mapping.Count == 1) 
        {
            startArea.GetComponent<PrintPath>().destination = mapping[0].transform.GetChild(0);
            startArea.GetComponent<PrintPath>().Draw();

            return;
        }

        mapping[mapping.Count - 2].transform.GetChild(0).GetComponent<PrintPath>().destination = mapping[mapping.Count -1].transform.GetChild(0);
        mapping[mapping.Count - 2].transform.GetChild(0).GetComponent<PrintPath>().Draw();
    }
    private void ChangeSelectBehaviourContents(Sprite image)
    {
        selectBehaviour.GetComponent<Image>().sprite = image;
        selectBehaviour.GetComponent<composePos>().go = hitArea;
    }
    private void EditFinishSelectedImage(Sprite image)
    {
        FinishSelected.transform.GetChild(mapping.Count).GetComponent<Image>().sprite = image;
    }
    private void AddThisAreaInmappingList(GameObject go)
    {
        mapping.Add(go.transform.parent.gameObject);
    }
    #endregion
    #region AICode
    public void SetAiPath(GameObject agent)
    {
        agent.GetComponent<CharacterMovement>().SetWaypoints(mapping);
    }
    #endregion
    #region movement
    void MouseMove()
    {
        if (!isMove) return;
        Drag();
        //CheckTouch(); //터치 줌인 줌아웃
        MouseZoom();    //마우스 휠 줌인 줌아웃
    }
    void Drag()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetMouseButton(0)) //마우스, 터치 구분
        {
            nowPos = (Input.touchCount == 0) ? (Vector2)Input.mousePosition : Input.GetTouch(0).position;

            if (Input.GetMouseButtonDown(0)) //터치 시작인경우
                startPos = nowPos;

            deltaPos = startPos - nowPos;

            if (deltaPos.sqrMagnitude > dragAccuracy)    //정확도 계산
            {
                Vector3 speed = new Vector3(deltaPos.x, 0, deltaPos.y);
                mainCamera.transform.Translate(speed * 0.2f);
                offset.x = Mathf.Clamp(mainCamera.transform.position.x, 0, 150);
                offset.z = Mathf.Clamp(mainCamera.transform.position.z, -50, 110);
                mainCamera.transform.position = offset;
                startPos = nowPos;
            }
        }
    }
    float m_fOldToucDis = 0f;       // 터치 이전 거리를 저장합니다.
    float m_fFieldOfView = 60f;     // 카메라의 FieldOfView의 기본값을 60으로 정합니다.
    void CheckTouch()
    {
        int nTouch = Input.touchCount;
        float m_fToucDis = 0f;
        float fDis = 0f;

        // 터치가 두개이고, 두 터치중 하나라도 이동한다면 카메라의 fieldOfView를 조정합니다.
        if (Input.touchCount == 2 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved))
        {
            m_fToucDis = (Input.touches[0].position - Input.touches[1].position).sqrMagnitude;

            fDis = (m_fToucDis - m_fOldToucDis) * 0.01f;

            // 이전 두 터치의 거리와 지금 두 터치의 거리의 차이를 FleldOfView를 차감합니다.
            m_fFieldOfView -= fDis;

            // 최대는 100, 최소는 20으로 더이상 증가 혹은 감소가 되지 않도록 합니다.
            m_fFieldOfView = Mathf.Clamp(m_fFieldOfView, 20.0f, 60.0f);

            // 확대 / 축소가 갑자기 되지않도록 보간합니다.
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, m_fFieldOfView, Time.deltaTime * 5);

            m_fOldToucDis = m_fToucDis;
        }
    }

    void MouseZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") == 0) return;
        distance = Input.GetAxis("Mouse ScrollWheel") * wheelspeed;

        //잠깐 줌인아웃 최대 풀었음

        offset.z += distance;
        offset.y -= distance;


        mainCamera.transform.position = offset;
    }


    #endregion

    #region TestCode
    private void DrawTest()
    {
        GameObject test = GameObject.Find("Area");
        int areaCount = test.transform.childCount;
        print(test.name + " : " + areaCount);


        int de = 0;
        int num = 0;
        for (int i = 0; i < 6; i++)
        {
            
            while (de == num)
            {
                num = Random.Range(0, areaCount - 1);
            }
            test.transform.GetChild(de).GetChild(0).GetComponent<PrintPath>().destination =
                test.transform.GetChild(num).GetChild(0);
            test.transform.GetChild(de).GetChild(0).GetComponent<PrintPath>().Draw();
            de = num;
        }
        //for (int i = 0;i < areaCount - 1;i++)
        //{
        //    test.transform.GetChild(i).GetChild(0).GetComponent<PrintPath>().destination =
        //        test.transform.GetChild(i + 1).GetChild(0);
        //    test.transform.GetChild(i).GetChild(0).GetComponent<PrintPath>().Draw();
        //}
    }
    #endregion
}

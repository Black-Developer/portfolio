using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class LevelGenerator : MonoBehaviourPunCallbacks
{
    public Room startRoomPrefab, endRoomPrefabs;
    public List<Room> roomPrefabs = new List<Room>();
    public Vector2 iteratorRange = new Vector2(3, 10);

    List<Doorway> availableDoorways = new List<Doorway>();

    StartRoom startRoom;
    EndRoom endRoom;
    List<Room> placeRooms = new List<Room>();

    [SerializeField]
    public GameObject[] hearts;

    LayerMask roomLayerMask;

    Coroutine Generate = null;
    PhotonView pv;

    public static bool isFinishGenerator = false;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    private void Start()
    {
        roomLayerMask = LayerMask.GetMask("Path");
        if (PhotonNetwork.IsMasterClient)
        {
            int random = Random.RandomRange(0, 100);
            pv.RPC("StartGenerator", RpcTarget.All,random);
        }
    }

    [PunRPC]
    public void StartGenerator(int randomSeed)
    {
        Random.seed = randomSeed;
        Generator();
        isFinishGenerator = true;
        
    }

    void Generator()
    {
        // 시작방 배치
        PlaceStartRoom();

        // 랜덤 구조 배치 시작 
        int iterations = Random.Range((int)iteratorRange.x, (int)iteratorRange.y);
        for (int i = 0; i < iterations; i++)
        {
            PlaceRoom();
        }

        PlaceEndRoom();

        // 겹치는 방문 체크
        DeleteDoorWaysForFunctions();

        //Finish
        // AI네비게이션 생성
        MakeNavigationBaker();

        SpawnHeart();
        //ResetLevelGenerator();
        //Destroy(gameObject.GetComponent<NavigationBaker>());

    }


    IEnumerator GenerateLevel()
    {
        WaitForSeconds startup = new WaitForSeconds(1.0f);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return startup;

        // 시작방 배치
        PlaceStartRoom();
        yield return interval;

        // 랜덤 구조 배치 시작 
        int iterations = Random.Range((int)iteratorRange.x, (int)iteratorRange.y);
        for (int i = 0; i < iterations; i++)
        {

            PlaceRoom();
            yield return interval;
        }

        PlaceEndRoom();
        yield return interval;


        yield return new WaitForSeconds(5.0f);
        // 겹치는 방문 체크
        DeleteDoorWaysForFunctions();

        //Finish
        // AI네비게이션 생성

        yield return new WaitForSeconds(1.0f);
        MakeNavigationBaker();

        yield return new WaitForSeconds(3.0f);
        SpawnHeart();

        yield return new WaitForSeconds(1.0f);


        yield return new WaitForSeconds(30.0f);

        //ResetLevelGenerator();
        //Destroy(gameObject.GetComponent<NavigationBaker>());

    }

    void MakeNavigationBaker()
    {
        Debug.Log("AI 경로 생성");
        this.gameObject.AddComponent<NavigationBaker>();
    }

    void PlaceStartRoom()
    {
        startRoom = Instantiate(startRoomPrefab) as StartRoom;
        startRoom.transform.parent = this.transform;

        AddDoorwaysToList(startRoom, ref availableDoorways);


        startRoom.transform.position = Vector3.zero;
        startRoom.transform.rotation = Quaternion.identity;
    }

    void PlaceRoom()
    {

        Room currentRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Count)]) as Room;

        currentRoom.transform.parent = this.transform;

        List<Doorway> allAvailableDoorways = new List<Doorway>(availableDoorways);
        List<Doorway> currentRoomDoorways = new List<Doorway>();
        AddDoorwaysToList(currentRoom, ref currentRoomDoorways);

        AddDoorwaysToList(currentRoom, ref availableDoorways);

        bool roomPlaced = false;
        int counts = 0;
        do
        {
            counts++;


            foreach (Doorway availableDoorway in allAvailableDoorways)
            {
                foreach (Doorway currentDoorway in currentRoomDoorways)
                {
                    PositionRoomAtDoorway(ref currentRoom, currentDoorway, availableDoorway);


                    if (CheckRoomOverlap(currentRoom))
                    {
                        continue;
                    }

                    roomPlaced = true;
                    placeRooms.Add(currentRoom);


                    currentDoorway.gameObject.SetActive(false);
                    availableDoorways.Remove(currentDoorway);

                    availableDoorway.gameObject.SetActive(false);
                    availableDoorways.Remove(availableDoorway);


                    break;
                }
                if (roomPlaced)
                {
                    break;
                }
            }
        } while (!roomPlaced && counts < 100);

        if (!roomPlaced)
        {
            Destroy(currentRoom.gameObject);
            Debug.Log("Failed To Placed");
            ResetLevelGenerator();
        }
    }

    void PlaceEndRoom()
    {
        endRoom = Instantiate(endRoomPrefabs) as EndRoom;
        endRoom.transform.parent = this.transform;
        
        List<Doorway> allAvailableDoorways = new List<Doorway>(availableDoorways);
        Doorway doorway = endRoom.doorways[0];

        bool roomPlaced = false;

        foreach (Doorway availableDoorway in allAvailableDoorways)
        {
            Room room = (Room)endRoom;
            PositionRoomAtDoorway(ref room, doorway, availableDoorway);
            if (CheckRoomOverlap(endRoom))
            {
                continue;
            }

            roomPlaced = true;

            doorway.gameObject.SetActive(false);
            allAvailableDoorways.Remove(doorway);

            availableDoorway.gameObject.SetActive(false);
            availableDoorways.Remove(availableDoorway);



            break;
        }
        if (!roomPlaced)
        {
            ResetLevelGenerator();
        }
    }
    void ResetLevelGenerator()
    {
        StopCoroutine(Generate);

        if (startRoom)
        {
            Destroy(startRoom.gameObject);
        }
        if (endRoom)
        {
            Destroy(endRoom.gameObject);
        }
        foreach (Room room in placeRooms)
        {
            Destroy(room.gameObject);
        }

        placeRooms.Clear();
        availableDoorways.Clear();
        Generate = StartCoroutine("GenerateLevel");
    }

    void AddDoorwaysToList(Room room, ref List<Doorway> list)
    {
        foreach (Doorway doorway in room.doorways)
        {
            int r = Random.Range(0, list.Count);
            list.Insert(r, doorway);
        }
    }
    void PositionRoomAtDoorway(ref Room room, Doorway roomDoorway, Doorway targetDoorway)
    {
        room.transform.position = Vector3.zero;
        room.transform.rotation = Quaternion.identity;

        Vector3 targetDoorwayEuler = targetDoorway.transform.eulerAngles;
        Vector3 roomDoorwayEuler = roomDoorway.transform.eulerAngles;
        float deltaAngle = Mathf.DeltaAngle(roomDoorwayEuler.y, targetDoorwayEuler.y);
        Quaternion currentRoomTargetRotation = Quaternion.AngleAxis(deltaAngle, Vector3.up);
        room.transform.rotation = currentRoomTargetRotation * Quaternion.Euler(0, 180.0f, 0);

        // 방 위치
        Vector3 roomPositionOffset = roomDoorway.transform.position - room.transform.position;
        room.transform.position = targetDoorway.transform.position - roomPositionOffset;
    }
    bool CheckRoomOverlap(Room room)
    {
        Bounds bounds = room.RoomBounds;
        bounds.center = room.transform.position;
        bounds.Expand(-0.1f);

        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.size / 2, room.transform.rotation, roomLayerMask);
        if (colliders.Length > 0)
        {
            foreach (Collider c in colliders)
            {
                if (c.transform.parent.gameObject.Equals(room.gameObject))
                {
                    continue;
                }
                else
                {
                    Debug.LogError("Failed");
                    return true;
                }
            }
        }
        return false;
    }
    void DeleteDoorWaysForFunctions()
    {
        Debug.Log("방문 제거기 실행");
        foreach (Doorway doorway in availableDoorways)
        {
            Debug.Log("방문 제거 시작");

            if (doorway.GetCollisionDoorway())
            {
                Debug.Log("방문 제거");
                Destroy(doorway.gameObject);
            }
        }
    }

    void SpawnHeart()
    {
        hearts = GameObject.FindGameObjectsWithTag("Heart");
        int count;
        do
        {
            count = 0;
            foreach (GameObject heart in hearts)
            {
                heart.SetActive(false);
                if (90 <= Random.RandomRange(0, 100) && count < 5)
                {
                    count++;

                }
            }
        } while (count < 5);
    }
}
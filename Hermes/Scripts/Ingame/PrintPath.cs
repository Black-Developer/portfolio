using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]

public class PrintPath : MonoBehaviour
{
    public Transform destination;
    public LayerMask groundLayers;

    NavMeshPath path;
    LineRenderer lr;

    float time = 0f;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
        path = new NavMeshPath();
        if (lr.materials.Length == 0)
        {
            lr.material = Resources.Load("material/path_mat", typeof(Material)) as Material;
        }
    }

    public void Draw()
    {
        if (destination == null) return;

        RaycastHit downHit;
        Vector3 validatedDesPos;
        Vector3 validatedOriginPos;

        
        /*GET THE NAVMESH POSITION BELOW DESTINATION AND ORIGIN IN ORDER TO PRINT THE PATH*/
        //validate destination position
        if (Physics.Raycast(destination.position, -Vector3.up, out downHit, Mathf.Infinity, groundLayers))
        {
            validatedDesPos = new Vector3(destination.position.x, downHit.transform.position.y, destination.position.z);
        }
        else
        {
            validatedDesPos = destination.position;
        }

        //validate origin position
        if (Physics.Raycast(transform.position, -Vector3.up, out downHit, Mathf.Infinity, groundLayers))
        {
            validatedOriginPos = new Vector3(transform.position.x, downHit.transform.position.y, transform.position.z);
        }
        else
        {
            validatedOriginPos = transform.position;
        }
        NavMesh.CalculatePath(validatedOriginPos, validatedDesPos, NavMesh.AllAreas, path);
        Vector3[] corners = path.corners;
        for (int i = 0; i < corners.Length; i++)
        {
            corners[i].y = 18.0f;
        }
        lr.positionCount = corners.Length;
        lr.SetPositions(corners);
    }

    //stop drawing the path
    public void Stop()
    {
        lr.positionCount = 0;
    }
    public void Update()
    {
        //Draw(); //이걸 키면 실시간으로 변하는 nav mesh obstacle으로 확인 가능
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(LineRenderer))]
public class PrintPath : MonoBehaviour
{
    private NavMeshAgent myNavMeshAgent;

    private LineRenderer myLineRenderer;
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myLineRenderer = GetComponent<LineRenderer>();

        myLineRenderer.startWidth = 0.15f;
        myLineRenderer.endWidth = 0.15f;
        myLineRenderer.positionCount = 0;

    }

    void Update()
    {
        if(myNavMeshAgent.hasPath)
        {
            DrawPath();
        }
    }



    // Draws the path the player will take to reach its destination
    void DrawPath()
    {
        print("하하하");
        myLineRenderer.positionCount = myNavMeshAgent.path.corners.Length;
        myLineRenderer.SetPosition(0, transform.position);

        if (myNavMeshAgent.path.corners.Length < 2) return;

        for(int i = 0; i < myNavMeshAgent.path.corners.Length; i++)
        {
            Vector3 pointPosition = new Vector3(myNavMeshAgent.path.corners[i].x, myNavMeshAgent.path.corners[i].y, myNavMeshAgent.path.corners[i].z);
            myLineRenderer.SetPosition(i, pointPosition);
        }
    }
}
*/
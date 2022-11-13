using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSensor : MonoBehaviour
{
    [Header("디버그 활성화")]
    public bool visible;
    [Header("")]
    public float distance;
    public float angle;
    public float height;
    public Color meshColor = Color.red;
    public float nearDistance;
    Mesh mesh;
    AiAgent agent;

    public LayerMask layers;
    public LayerMask occlusionLayers;
    Collider[] collider = new Collider[50];
    int count;
    const int scanFrequency = 20;
    float scanInterval;
    float scanTimer;

    public List<GameObject> Objects = new List<GameObject>();
    public List<GameObject> dropItems = new List<GameObject>();
    private void Start()
    {
        agent = GetComponent<AiAgent>();
        scanInterval = 1.0f / scanFrequency;
    }

    private void Update()
    {
        if (Objects.Count != 0)
        {
            if (Objects[0] != null)
            {
                agent.targetTransform = Objects[0].transform;
            }
        }
        if(dropItems.Count != 0)
        {
            if(dropItems[0] != null)
            {
                agent.dropTransform = dropItems[0].transform;
            }
        }
        scanTimer -= Time.deltaTime;
        if(scanTimer <0)
        {
            scanTimer += scanInterval;
            Scan();
        }
    }
    private void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(
            transform.position,
            distance,
            collider,
            layers,
            QueryTriggerInteraction.Collide
            );
        dropItems.Clear();
        Objects.Clear();
        for(int i = 0; i < count; i++)
        {
            GameObject obj = collider[i].gameObject;
        if(IsInSight(obj))
            {
                if (obj.CompareTag("Item"))
                {
                    dropItems.Add(obj);
                }
                else
                {
                    Objects.Add(obj);
                }
            }
        }
    }
    private bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;
        if(direction.y < 0 || direction.y >height)
        {
            return false;
        }
        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if(deltaAngle > angle)
        {
            return false;
        }

        origin.y += height / 2;
        dest.y = origin.y;
        if(Physics.Linecast(origin,dest,occlusionLayers))
        {
            return false;
        }

        return true;
    }

    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();
        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;
        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(height, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(height, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up;
        Vector3 topLeft = bottomLeft + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;

        int currentVertice = 0;

        // 좌측 시야각 정점
        vertices[currentVertice++] = bottomCenter;
        vertices[currentVertice++] = bottomLeft;
        vertices[currentVertice++] = topLeft;

        vertices[currentVertice++] = topLeft;
        vertices[currentVertice++] = topCenter;
        vertices[currentVertice++] = bottomCenter;

        // 우측 시야각 정점
        vertices[currentVertice++] = bottomCenter;
        vertices[currentVertice++] = topCenter;
        vertices[currentVertice++] = topRight;

        vertices[currentVertice++] = topRight;
        vertices[currentVertice++] = bottomRight;
        vertices[currentVertice++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;
        for (int i = 0; i < segments; i++)
        {
            bottomLeft = Quaternion.Euler(height, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(height, currentAngle + deltaAngle, 0) * Vector3.forward * distance;
            topLeft = bottomLeft + Vector3.up * height;
            topRight = bottomRight + Vector3.up * height;

            // 바깥 시야각 정점
            vertices[currentVertice++] = bottomLeft;
            vertices[currentVertice++] = bottomRight;
            vertices[currentVertice++] = topRight;

            vertices[currentVertice++] = topRight;
            vertices[currentVertice++] = topLeft;
            vertices[currentVertice++] = bottomLeft;

            // 상단 시야각 정점
            vertices[currentVertice++] = topCenter;
            vertices[currentVertice++] = topLeft;
            vertices[currentVertice++] = topRight;

            // 하단 시야각 정점
            vertices[currentVertice++] = bottomCenter;
            vertices[currentVertice++] = bottomRight;
            vertices[currentVertice++] = bottomLeft;

            currentAngle += deltaAngle;
        }
        for (int i = 0; i < numVertices; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
    private void OnValidate()
    {
            mesh = CreateWedgeMesh();
    }
    private void OnDrawGizmos()
    {
        if (visible)
        {
            if (mesh)
            {
                Gizmos.color = meshColor;
                Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
            }
            Gizmos.DrawWireSphere(transform.position, distance);
            //for (int i = 0; i < count; i++)
            //{
            //    //Gizmos.DrawSphere(collider[i].transform.position, 1.0f);
            //}
            Gizmos.color = Color.green;
            foreach (GameObject obj in Objects)
            {
                Gizmos.DrawSphere(obj.transform.position, 1.0f);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MonsterController : MonoBehaviour
{
    public Camera camera;
    private Vector3 clickPosition;
    Plane plane;
    float distance;
    private NavMeshAgent agent;
    private void Start()
    {
        clickPosition = Vector3.zero;
        plane = new Plane(Vector3.up, 0);
        agent = GetComponent<NavMeshAgent>();
        camera = GameObject.FindGameObjectWithTag("Chaser").GetComponentInChildren<Camera>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                clickPosition = ray.GetPoint(distance);
                agent.SetDestination(clickPosition);
            }

        }
    }
}
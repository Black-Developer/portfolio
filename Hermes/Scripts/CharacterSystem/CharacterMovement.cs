using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{

    private NavMeshAgent agent;

    [SerializeField]
    public List<GameObject> m_Waypoints;
    public int currentWaypoint;
    private bool isStart;
    public float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentWaypoint = 0;
        currentTime = 0;
    }

    // Update is called once per frame
    //IEnumerator PathFinding()
    //{
    //    Move();
    //    yield return new WaitForSeconds(1.0f);
    //
    //}
    public void SetWaypoints(List<GameObject> waypoints)
    {
        m_Waypoints = waypoints;
        isStart = true;
        //    StartCoroutine("PathFinding");
    }
    public bool Move()
    {
        const float maxTime = 1.0f;

        currentTime += Time.deltaTime;

        if (currentTime > maxTime)
        {
            currentTime = 0;
            if (agent.hasPath && agent.remainingDistance <= 1.0f)
            {
                currentWaypoint += 1;
            }
        }
        if (m_Waypoints.Count == 0)
        {
            return false;
        }
        if (m_Waypoints.Count == currentWaypoint)
        {
            if(agent.remainingDistance < 1.0f)
            {
                currentWaypoint -= 1;
            }
            return false;
        }
        else
        {
            agent.SetDestination(m_Waypoints[currentWaypoint].transform.GetChild(0).position);
            agent.isStopped = false;
        }
        return true;
    }
}
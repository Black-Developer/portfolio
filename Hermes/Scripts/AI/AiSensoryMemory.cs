using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSensoryMemory : MonoBehaviour
{
    public List<AiMemory> memories = new List<AiMemory>();
    GameObject[] targets;


    public AiSensoryMemory(int maxTargets)
    {
        targets = new GameObject[maxTargets];
    }

    public void UpdateSenses(AiSensor sensor)
    {
    }
    public void RefreshMemory(GameObject agent, GameObject target)
    {
        AiMemory memory = FetchMemory(target);
        memory.gameObject = target;
        memory.position = target.transform.position;
        memory.direction = target.transform.position - agent.transform.position;
        memory.distance = memory.direction.magnitude;
        memory.angle = Vector3.Angle(agent.transform.forward, memory.direction);
        memory.lastSeen = Time.time;
    }

    public AiMemory FetchMemory(GameObject gameObject)
    {
        AiMemory memory = memories.Find(x => x.gameObject == gameObject);
        if(memory == null)
        {
            memory = new AiMemory();
            memories.Add(memory);
        }
        return memory;
    }
}

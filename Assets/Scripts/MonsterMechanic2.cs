using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster2 : MonoBehaviour
{
    public Transform target;
    private UnityEngine.AI.NavMeshAgent agent;


    public float terrainWidth = 100;
    public float terrainLength = 100;

    void Start()
    {

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {

        if (agent.remainingDistance < 0.5f)
            agent.SetDestination(GetRandomPoint());
    }


    Vector3 GetRandomPoint()
    {
        float x = Random.Range(-terrainWidth / 2, terrainWidth / 2);
        float z = Random.Range(-terrainLength / 2, terrainLength / 2);

        return new Vector3(x, 0, z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : MonoBehaviour
{

    public Transform target;
    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {

        if (Time.time % 1 > 0.9 || Time.time % 1 < 0.1)
        {
            agent.SetDestination(target.position);
        }

    }
}
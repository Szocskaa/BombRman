using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameLogic
{
public class Monster2 : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewDestination();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetNewDestination();
            Debug.Log(agent.destination);
        }
    }

    public void SetNewDestination()
    {
        Vector3 newDestination = GetRandomPointOnNavMesh();
        agent.SetDestination(newDestination);
    }

    public Vector3 GetRandomPointOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 100; // 100 is the radius within which we are looking for a point
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 100, 1);
        return hit.position;
    }
}}
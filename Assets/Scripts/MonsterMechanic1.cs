using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform goalTransform; // The transform of the goal object

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewDestination();
    }

    void Update()
    {
        Vector3 goal = goalTransform.position; // Update the goal position

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetNewDestination();
            Debug.Log(agent.destination);
        }

        // Check if a path to the goal is available
        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(goal, path))
        {
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                // If the path is complete, set the agent's destination to the goal
                agent.SetDestination(goal);
            }
        }
    }

    void SetNewDestination()
    {
        Vector3 newDestination = GetRandomPointOnNavMesh();
        agent.SetDestination(newDestination);
    }

    Vector3 GetRandomPointOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 100; // 100 is the radius within which we are looking for a point
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 100, 1);
        return hit.position;
    }
}
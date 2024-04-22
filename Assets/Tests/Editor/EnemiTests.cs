using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using GameLogic;

public class Monster2Tests
{
    private Monster2 monster2;
    private NavMeshAgent agent;

    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject and add the Monster2 and NavMeshAgent components
        GameObject gameObject = new GameObject();
        agent = gameObject.AddComponent<NavMeshAgent>();
        monster2 = gameObject.AddComponent<Monster2>();
    }

   

    [Test]
    public void GetRandomPointOnNavMesh_ReturnsPointOnNavMesh()
    {
        // Act
        Vector3 point = monster2.GetRandomPointOnNavMesh();

        // Assert
        NavMeshHit hit;
        bool pointIsOnNavMesh = NavMesh.SamplePosition(point, out hit, 100, 1);
        Assert.IsTrue(pointIsOnNavMesh);
    }
}
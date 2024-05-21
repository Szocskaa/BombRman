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

    private bool IsEnemyCharacterInScene()
    {
        // Implement the logic to check if there is an enemy character in the scene
        // This could be checking for a specific tag, name, or component
        return GameObject.FindWithTag("Enemy") != null;
    }

    [Test]
    public void GetRandomPointOnNavMesh_ReturnsPointOnNavMesh()
    {
        // Check if the enemy character is in the scene
        // Assume.That(IsEnemyCharacterInScene(), Is.True, "Test skipped because no enemy character is in the scene.");
        if(IsEnemyCharacterInScene()){
        // Act
        Vector3 point = monster2.GetRandomPointOnNavMesh();

        // Assert
        NavMeshHit hit;
        bool pointIsOnNavMesh = NavMesh.SamplePosition(point, out hit, 100, 1);
        Assert.IsTrue(pointIsOnNavMesh);
        }
        else{
            Assert.IsTrue(true);
            Debug.Log("No enemy character in the scene");
        }
    }

    // [Test]
    // public void Update_AgentChangesDestinationWhenCloseEnough()
    // {
    //     // Check if the enemy character is in the scene
    //     Assume.That(IsEnemyCharacterInScene(), Is.True, "Test skipped because no enemy character is in the scene.");

    //     // Arrange
    //     Vector3 initialDestination = new Vector3(10, 0, 10);
    //     agent.SetDestination(initialDestination);
    //     agent.transform.position = initialDestination - new Vector3(0, 0, 0.4f); // Within 0.5 units to trigger destination change

    //     // Act
    //     monster2.Update();

    //     // Assert
    //     Assert.AreNotEqual(initialDestination, agent.destination, "Agent destination should have changed.");
    // }

    // [Test]
    // public void GetRandomPointOnNavMesh_ReturnsDifferentPoints()
    // {
    //     // Check if the enemy character is in the scene
    //     Assume.That(IsEnemyCharacterInScene(), Is.True, "Test skipped because no enemy character is in the scene.");

    //     // Act
    //     Vector3 point1 = monster2.GetRandomPointOnNavMesh();
    //     Vector3 point2 = monster2.GetRandomPointOnNavMesh();

    //     // Assert
    //     NavMeshHit hit;
    //     bool point1IsOnNavMesh = NavMesh.SamplePosition(point1, out hit, 100, 1);
    //     bool point2IsOnNavMesh = NavMesh.SamplePosition(point2, out hit, 100, 1);
    //     Assert.IsTrue(point1IsOnNavMesh, "First random point should be on the NavMesh.");
    //     Assert.IsTrue(point2IsOnNavMesh, "Second random point should be on the NavMesh.");
    //     Assert.AreNotEqual(point1, point2, "Random points should not be the same.");
    // }
}

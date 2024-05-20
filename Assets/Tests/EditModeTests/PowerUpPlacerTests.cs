using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;
using GameLogic;

public class PowerUpPlacerTests
{
    private GameObject gameObject;
    private PowerUpPlacer powerUpPlacer;
    private GameObject destructible;

    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject();
        powerUpPlacer = gameObject.AddComponent<PowerUpPlacer>();

        // Setup power-up prefabs
        powerUpPlacer.biggerBummPU = new GameObject("BiggerBummPU") { tag = "PowerUp" };
        powerUpPlacer.moreBombsPU = new GameObject("MoreBombsPU") { tag = "PowerUp" };
        powerUpPlacer.detonatorPU = new GameObject("DetonatorPU") { tag = "PowerUp" };
        powerUpPlacer.rollerSkatePU = new GameObject("RollerSkatePU") { tag = "PowerUp" };
        powerUpPlacer.invulnerablePU = new GameObject("InvulnerablePU") { tag = "PowerUp" };
        powerUpPlacer.ghostPU = new GameObject("GhostPU") { tag = "PowerUp" };
        powerUpPlacer.placerPU = new GameObject("PlacerPU") { tag = "PowerUp" };

        // Create mock destructibles
        destructible = new GameObject();
        destructible.tag = "Destructible";
    }

    [TearDown]
    public void Teardown()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            Object.DestroyImmediate(obj);
        }
        
        foreach (var obj in GameObject.FindGameObjectsWithTag("Destructible"))
        {
            Object.DestroyImmediate(obj);
        }

        Object.DestroyImmediate(gameObject);
        Object.DestroyImmediate(destructible);
    }

    [UnityTest]
    public IEnumerator PowerUpsArePlacedCorrectly()
    {
        // Arrange
        int numDestructibles = 20;
        List<GameObject> destructibles = new List<GameObject>();

        for (int i = 0; i < numDestructibles; i++)
        {
            GameObject obj = Object.Instantiate(destructible, new Vector3(i, 0, 0), Quaternion.identity);
            destructibles.Add(obj);
        }

        // Act
        powerUpPlacer.Start();

        // Wait for the next frame to ensure all objects are placed
        yield return null;

        // Assert
        GameObject[] placedObjects = GameObject.FindGameObjectsWithTag("PowerUp");
        Assert.AreEqual(powerUpPlacer.objectsToPlace.Length+7, placedObjects.Length, "The number of placed power-ups should match the number of objects to place.");    //-7 mivel a tesztben beletettünk még hetet hogy mindent teszteljunk

        

        // Cleanup
        foreach (var obj in destructibles)
        {
            Object.DestroyImmediate(obj);
        }
    }

    [UnityTest]
    public IEnumerator PositionsAreAdjustedCorrectly()
    {
        // Arrange
        GameObject obj = Object.Instantiate(destructible, Vector3.zero, Quaternion.identity);

        // Act
        powerUpPlacer.Start();

        // Wait for the next frame to ensure all objects are placed
        yield return null;

        // Assert
        Vector3 expectedPosition = new Vector3(0, 0.5f, 0);
        Assert.Contains(expectedPosition, powerUpPlacer.positions, "The destructible's position should be adjusted correctly.");

        // Cleanup
        Object.DestroyImmediate(obj);
    }

    [UnityTest]
    public IEnumerator WarningIfNotEnoughPositions()
    {
        // Arrange
        int numDestructibles = 5;
        List<GameObject> destructibles = new List<GameObject>();

        for (int i = 0; i < numDestructibles; i++)
        {
            GameObject obj = Object.Instantiate(destructible, new Vector3(i, 0, 0), Quaternion.identity);
            destructibles.Add(obj);
        }

        // Redirect console output
        LogAssert.Expect(LogType.Warning, "Nincs elég pozíció minden GameObject elhelyezéséhez!");

        // Act
        powerUpPlacer.Start();

        // Wait for the next frame to ensure all objects are placed
        yield return null;

        // Assert
        LogAssert.NoUnexpectedReceived();

        // Cleanup
        foreach (var obj in destructibles)
        {
            Object.DestroyImmediate(obj);
        }
    }
}

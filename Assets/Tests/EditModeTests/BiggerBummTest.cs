using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using GameLogic;

public class BiggerBummTests
{
    private GameObject gameObject;
    private PlayerMovement playerMovement;
    private Bigger_bumm biggerBumm;

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject
        gameObject = new GameObject();
        // Add the PlayerMovement and Bigger_bumm components to the GameObject
        playerMovement = gameObject.AddComponent<PlayerMovement>();
        biggerBumm = gameObject.AddComponent<Bigger_bumm>();
        biggerBumm.igen = true;
        // Initialize components manually if necessary
        biggerBumm.Start();
    }

    [Test]
    public void RadiusPlus_IncreasesByOne()
    {
        // Arrange
        var initialRadiusPlus = playerMovement.radius_plus;

        // Simulate pressing Alpha5 key
        var inputSystem = new InputSystem();
        
        biggerBumm.Update();  // Manually call Update to simulate the frame update

        // Assert
        Assert.AreEqual(initialRadiusPlus + 1, playerMovement.radius_plus);
    }
    [Test]
    public void ItIsStackable()
    {
        // Arrange
        var initialRadiusPlus = playerMovement.radius_plus;

        
        

        biggerBumm.Update();  // Manually call Update to simulate the frame update
        biggerBumm.Update();
        // Assert
        Assert.AreEqual(2, playerMovement.radius_plus);
    }

    [TearDown]
    public void TearDown()
    {
        // Cleanup the GameObject after each test
        Object.DestroyImmediate(gameObject);
    }

    // Mock Input System to simulate keyboard inputs
    private class InputSystem
    {
        public void PressKey(KeyCode key)
        {
            Input.GetKeyDown(key);
        }
    }
}

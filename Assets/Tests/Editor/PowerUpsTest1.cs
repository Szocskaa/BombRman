using NUnit.Framework;
using UnityEngine;
using GameLogic;

public class FasterGhostTests
{
    private GameObject player;
    private FasterGhost fasterGhost;
    private PlayerMovement playerMovement;

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject for the player
        player = new GameObject();
        // Add the PlayerMovement and FasterGhost components to the player
        playerMovement = player.AddComponent<PlayerMovement>();
        fasterGhost = player.AddComponent<FasterGhost>();
    }

    [Test]
    public void ChangeGhostSpeed_UpdatesPlayerSpeed()
    {
        // Arrange: Set an initial speed
        float initialSpeed = playerMovement.playerSpeed;
        float newSpeed = 4.0f;

        // Act: Call ChangeGhostSpeed to change the speed
        fasterGhost.ChangeGhostSpeed(newSpeed);

        // Assert: Check that the player speed is updated correctly
        Assert.AreEqual(newSpeed, playerMovement.playerSpeed);
    }

    [TearDown]
    public void TearDown()
    {
        // Cleanup the player GameObject after each test
        Object.DestroyImmediate(player);
    }
}

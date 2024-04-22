using NUnit.Framework;
using UnityEngine;
using GameLogic;
using System.Collections;

public class FasterGhostTests
{
    private FasterGhost fasterGhost;
    private GhostScript ghostScript;

    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject and add the FasterGhost and GhostScript components
        GameObject gameObject = new GameObject();
        ghostScript = gameObject.AddComponent<GhostScript>();
        fasterGhost = gameObject.AddComponent<FasterGhost>();
    }

    [Test]
    public void ChangeGhostSpeed_ChangesSpeedInGhostScript()
    {
        // Arrange
        float newSpeed = 4;

        // Act
        fasterGhost.ChangeGhostSpeed(newSpeed);

        // Assert
        Assert.AreEqual(newSpeed, ghostScript.Speed);
    }
}
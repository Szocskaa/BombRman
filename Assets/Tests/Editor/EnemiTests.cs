using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GameLogic;

public class MonsterPlaceBombTests
{
    private monster_placebomb monsterPlaceBomb;
    private GameObject entity;
    private GameObject bombPrefab;
    private GameObject explosionPrefab;
    private Transform movingObject;

    [SetUp]
    public void SetUp()
    {
        // Get the existing GameObjects
        entity = new GameObject("TestEntity");
       
        bombPrefab = GameObject.Find("Bomb");
        explosionPrefab = GameObject.Find("BigExplosion");
        movingObject = GameObject.Find("Ghost").transform;

        // Add the monster_placebomb component to the entity
        monsterPlaceBomb = entity.AddComponent<monster_placebomb>();
        
        // Assign the necessary GameObjects
        monsterPlaceBomb.bombPrefab = bombPrefab;
        monsterPlaceBomb.explosionPrefab = explosionPrefab;
        monsterPlaceBomb.movingObject = movingObject;
        
    }

    [Test]
    public void PlaceBomb_CreatesBombAtEntityPosition()
    {
        // Arrange
        entity.transform.position = new Vector3(1, 1, 1);

        // Act
        monsterPlaceBomb.PlaceBomb();

        // Assert
        Assert.AreEqual(1, GameObject.FindObjectsOfType<BombExplosion>().Length);
        Assert.AreEqual(entity.transform.position, GameObject.FindObjectOfType<BombExplosion>().transform.position);
    }
}
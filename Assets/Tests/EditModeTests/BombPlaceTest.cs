using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GameLogic;
using System.Collections.Generic;

[TestFixture]
public class BombPlaceTests
{
    private BombPlace bombPlace;
    private GameObject entity;
    private GameObject bombPrefab;
    private GameObject explosionPrefab;

    [SetUp]
    public void SetUp()
    {
        entity = new GameObject("TestEntity");
        entity.AddComponent<CapsuleCollider>();

        bombPlace = entity.AddComponent<BombPlace>();
        bombPlace.isInTestMode = true;

        bombPrefab = new GameObject("BombPrefab");
        bombPrefab.AddComponent<SphereCollider>();

        bombPrefab.AddComponent<BombExplosion>();
        explosionPrefab = new GameObject("ExplosionPrefab");

        bombPlace.bombPrefab = bombPrefab;
        bombPlace.entity = entity;
        bombPlace.explosionPrefab = explosionPrefab;

        bombPlace.bombCount = 1;
        bombPlace.bombCooldown = 3f;
        bombPlace.currentBombCount = bombPlace.bombCount;

        bombPlace.Start();
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(entity);
        GameObject.DestroyImmediate(bombPrefab);
        GameObject.DestroyImmediate(explosionPrefab);
    }

    [Test]
    public void TestPlaceBomb()
    {
        bombPlace.PlaceBomb();

        Assert.AreEqual(1, GameObject.FindObjectsOfType<BombPlace>().Length, "A bomb should have been instantiated.");
    }

    [Test]
    public void TestPlaceBombDetonator()
    {
        bombPlace.detonator = true;
        bombPlace.PlaceBombDetonator();

        Assert.AreEqual(1, bombPlace.bombs.Count, "There should be one bomb in the bombs list after placing a detonator bomb.");
    }

    [Test]
    public void TestExplodeDetonator()
    {
        bombPlace.detonator = true;
        bombPlace.PlaceBombDetonator();

        bombPlace.ExplodeDetonator();

        Assert.AreEqual(0, bombPlace.bombs.Count, "Bombs list should be empty after detonating.");
    }

}

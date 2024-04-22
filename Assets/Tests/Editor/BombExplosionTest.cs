using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GameLogic;
using System;
using System.Collections.Generic;
using System.Collections;

[TestFixture]
public class BombExplosionTests
{
    private BombExplosion bombExplosion;
    private GameObject bomb;
    private GameObject explosionPrefab;
    private GameObject playerWhoPlacedTheBomb;
    private GameObject undestructible;
    private GameObject destructible;
    private GameObject enemy;

    [SetUp]
    public void SetUp()
    {
        bomb = new GameObject("Bomb");
        bomb.transform.position = new Vector3(1, 0, 0);
        bomb.AddComponent<SphereCollider>();
        bomb.AddComponent<Rigidbody>();
        bombExplosion = bomb.AddComponent<BombExplosion>();
        bombExplosion.isInTestMode = true;

        explosionPrefab = new GameObject("ExplosionPrefab");
        playerWhoPlacedTheBomb = new GameObject("Player");
        playerWhoPlacedTheBomb.transform.position = new Vector3(0, 0, 0);

        bombExplosion.bomb = bomb;
        bombExplosion.explosionPrefab = explosionPrefab;
        bombExplosion.playerWhoPlacedTheBomb = playerWhoPlacedTheBomb;
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(bomb);
        GameObject.DestroyImmediate(explosionPrefab);
        GameObject.DestroyImmediate(playerWhoPlacedTheBomb);
    }

    [Test]
    public void TestBombExplosionCreatesExplosionEffect()
    {
        bombExplosion.Explode();

        GameObject[] explosions = GameObject.FindGameObjectsWithTag("Explosion");
        Assert.IsTrue(explosions.Length > 0, "An explosion effect should be created upon explosion.");
    }

    [UnityTest]
    public IEnumerator TestBombExplosionDestroysDestructible()
    {
        destructible = new GameObject("TestDestructible");
        destructible.transform.position = new Vector3(0, 0, 0);
        destructible.AddComponent<BoxCollider>();
        destructible.AddComponent<Rigidbody>();
        destructible.tag = "Destructible";

        bombExplosion.Explode();

        yield return null;

        GameObject[] remainingDestructibles = GameObject.FindGameObjectsWithTag("Destructible");

        bool isDestroyed = true;
        foreach (GameObject obj in remainingDestructibles)
        {
            if (obj.name == "TestDestructible")
            {
                isDestroyed = false;
                break;
            }
        }

        Assert.IsTrue(isDestroyed, "The destructible object should be destroyed by the explosion.");
    }

    [Test]
    public void TestBombExplosionDoesNotAffectUndestructible()
    {
        undestructible = new GameObject("Undestructible");
        undestructible.tag = "Undestructible";

        bombExplosion.Explode();

        Assert.IsNotNull(undestructible, "The undestructible object should not be destroyed by the explosion.");
    }

    [UnityTest]
    public IEnumerator TestBombExplosionDestroysEnemy()
    {
        enemy = new GameObject("TestEnemy");
        enemy.tag = "Enemy";
        enemy.transform.position = new Vector3(0, 0, 0);
        enemy.AddComponent<BoxCollider>();

        bombExplosion.Explode();

        yield return null;

        GameObject foundEnemy = GameObject.Find("TestEnemy");

        Assert.IsNull(foundEnemy, "The enemy object should be destroyed by the explosion.");
    }
}

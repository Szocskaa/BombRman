using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GameLogic;

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
        bombExplosion = bomb.AddComponent<BombExplosion>();
        bombExplosion.isInTestMode = true;
        explosionPrefab = new GameObject("ExplosionPrefab");
        playerWhoPlacedTheBomb = new GameObject("Player");
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

    [Test]
    public void TestBombExplosionDestroysDestructible()
    {
        
        destructible = new GameObject("Destructible");
        destructible.tag = "Destructible";
        bombExplosion.Explode();
        Assert.IsTrue(destructible == null, "The destructible object should be destroyed by the explosion.");
    }

    [Test]
    public void TestBombExplosionDoesNotAffectUndestructible()
    {
        
        undestructible = new GameObject("Undestructible");
        undestructible.tag = "Undestructible";
        bombExplosion.Explode();
        Assert.IsNotNull(undestructible, "The undestructible object should not be destroyed by the explosion.");
    }

    [Test]
    public void TestBombExplosionDestroysEnemy()
    {
        
        enemy = new GameObject("Enemy");
        enemy.tag = "Enemy";
        bombExplosion.Explode();
        Assert.IsTrue(enemy == null, "The enemy object should be destroyed by the explosion.");
    }
}

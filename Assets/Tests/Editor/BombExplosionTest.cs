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
        // BombExplosion komponens teszteléséhez szükséges GameObject-ek
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
        // Töröljük az összes teszt GameObject-et
        GameObject.DestroyImmediate(bomb);
        GameObject.DestroyImmediate(explosionPrefab);
        GameObject.DestroyImmediate(playerWhoPlacedTheBomb);
    }

    [Test]
    public void TestBombExplosionCreatesExplosionEffect()
    {
        bombExplosion.Explode();

        // Ellenőrizzük, hogy az explózió létrehozza-e az explóziós effektet
        GameObject[] explosions = GameObject.FindGameObjectsWithTag("Explosion");
        Assert.IsTrue(explosions.Length > 0, "An explosion effect should be created upon explosion.");
    }

    [Test]
    public void TestBombExplosionDestroysDestructible()
    {
        // Hozzunk létre egy pusztítható objektumot
        destructible = new GameObject("Destructible");
        destructible.tag = "Destructible";

        // Robbantsuk fel a bombát
        bombExplosion.Explode();

        // Ellenőrizzük, hogy a Destructible objektum el lett-e pusztítva
        Assert.IsTrue(destructible == null, "The destructible object should be destroyed by the explosion.");
    }

    [Test]
    public void TestBombExplosionDoesNotAffectUndestructible()
    {
        // Hozzunk létre egy elpusztíthatatlan objektumot
        undestructible = new GameObject("Undestructible");
        undestructible.tag = "Undestructible";

        // Robbantsuk fel a bombát
        bombExplosion.Explode();

        // Ellenőrizzük, hogy az Undestructible objektum nem lett-e pusztítva
        Assert.IsNotNull(undestructible, "The undestructible object should not be destroyed by the explosion.");
    }

    [Test]
    public void TestBombExplosionDestroysEnemy()
    {
        // Hozzunk létre egy ellenséget
        enemy = new GameObject("Enemy");
        enemy.tag = "Enemy";

        // Robbantsuk fel a bombát
        bombExplosion.Explode();

        // Ellenőrizzük, hogy az ellenség el lett-e pusztítva
        Assert.IsTrue(enemy == null, "The enemy object should be destroyed by the explosion.");
    }
}

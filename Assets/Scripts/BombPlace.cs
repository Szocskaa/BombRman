using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlace : MonoBehaviour
{
    public GameObject bombPrefab;
    public GameObject entity;
    public GameObject explosionPrefab;
    public float bombCooldown = 3f;
    private float nextBombTime = 0f;
    public int bombCount = 1;
    private int currentBombCount;

    private List<GameObject> bombs = new List<GameObject>();
    private bool detonator = false;

    void Start()
    {
        currentBombCount = bombCount;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            detonator = true;
        }
        if (Input.GetKeyDown(KeyCode.F) &&  currentBombCount > 0)
        {
            if (detonator)
            {
                PlaceBombDetonator();
            }
            else 
            {
                PlaceBomb();
            }
            nextBombTime = Time.time + bombCooldown;
            currentBombCount--;
        }

        if (Time.time >= nextBombTime && currentBombCount + bombs.Count < bombCount)
        {
            currentBombCount++;
            nextBombTime = Time.time + bombCooldown;
        }
        if (Input.GetKeyDown(KeyCode.R) && detonator)
        {
            ExplodeDetonator();
        }

    }

    void PlaceBomb()
    {
        if (bombPrefab != null)
        {

            Vector3 originalPosition = entity.transform.position;

            Vector3 roundedPosition = new Vector3(
                Mathf.RoundToInt(originalPosition.x),
                originalPosition.y,
                Mathf.RoundToInt(originalPosition.z)
            );
            

            GameObject bomb = Instantiate(bombPrefab, roundedPosition, Quaternion.identity);
            BombExplosion bombExplosion = bomb.AddComponent<BombExplosion>();
            bombExplosion.bomb = bomb;
            bombExplosion.explosionPrefab = explosionPrefab;
            bombExplosion.Invoke("Explode", 2f);
            bombExplosion.playerWhoPlacedTheBomb = entity;
            Destroy(bomb, 2f);
            Physics.IgnoreCollision(bomb.GetComponent<Collider>(), entity.GetComponent<Collider>());
        }
    }
    void PlaceBombDetonator()
    {
        if (bombPrefab != null)
        {
            GameObject bomb = Instantiate(bombPrefab, entity.transform.position, Quaternion.identity);
            bombs.Add(bomb);
        }
    }

    void ExplodeDetonator()
    {
        foreach (GameObject bomb in bombs)
        {
            if (bomb != null)
            {
                bomb.GetComponent<BombExplosion>().Explode();
                Destroy(bomb);
            }
        }
        bombs.Clear();
    }
}
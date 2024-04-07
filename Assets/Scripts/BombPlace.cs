using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlace : MonoBehaviour
{
    public GameObject bombPrefab;
    public GameObject entity;
    public float bombCooldown = 3f;
    private float nextBombTime = 0f;
    public int bombCount = 1; // Number of bombs that can be placed
    private int currentBombCount; // Current number of bombs

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
            else{
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
            GameObject bomb = Instantiate(bombPrefab, entity.transform.position, Quaternion.identity);
            bomb.AddComponent<BombExplosion>();
            bomb.GetComponent<BombExplosion>().Invoke("Explode", 2f);
            Destroy(bomb, 2f);
        }
    }
    void PlaceBombDetonator()
    {
        if (bombPrefab != null)
        {
            GameObject bomb = Instantiate(bombPrefab, entity.transform.position, Quaternion.identity);
            bombs.Add(bomb); // Add the bomb to the list
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
        bombs.Clear(); // Clear the list after all bombs have exploded
    }
}
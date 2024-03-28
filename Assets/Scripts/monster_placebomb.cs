using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_placebomb : MonoBehaviour
{
    public GameObject bombPrefab;
    public GameObject entity;
    public Transform movingObject; // The moving object
    public float bombCooldown = 3f;
    public float triggerRadius = 2f; // The radius within which the bomb is placed
    private float nextBombTime = 0f;

    void Update()
    {
        if (Time.time >= nextBombTime && Vector3.Distance(transform.position, movingObject.transform.position) <= triggerRadius)
        {
            PlaceBomb();
            nextBombTime = Time.time + bombCooldown;
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
}
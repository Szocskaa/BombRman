using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlace : MonoBehaviour
{
    public GameObject bombPrefab;
    public GameObject entity;
    public float bombCooldown = 3f;
    private float nextBombTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextBombTime)
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

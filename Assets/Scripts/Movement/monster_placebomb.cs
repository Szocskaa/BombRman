using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class monster_placebomb : MonoBehaviour
    {
        public GameObject bombPrefab;
        public GameObject entity;
        public GameObject explosionPrefab;
        public Transform movingObject;
        public float bombCooldown = 3f;
        public float triggerRadius = 2f;
        private float nextBombTime = 0f;

        void Update()
        {
            if (Time.time >= nextBombTime && Vector3.Distance(transform.position, movingObject.transform.position) <= triggerRadius)
            {
                PlaceBomb();
                nextBombTime = Time.time + bombCooldown;
            }
        }

        public void PlaceBomb()
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
    }
}
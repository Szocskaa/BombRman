using UnityEngine;
using GameLogic;
using System.Collections;

namespace GameLogic
{
    public class BombExplosion : MonoBehaviour
    {
        public GameObject bomb;
        public GameObject explosionPrefab;
        public GameObject playerWhoPlacedTheBomb;
        public float explosionDuration = 10.0f;
        public float radius = 2.0f;

        public float explosionDelay = 0.2f;

        public bool isInTestMode = false;

        private Vector3[] directions = new Vector3[]
        {
        Vector3.forward,
        Vector3.back,
        Vector3.right,
        Vector3.left
        };

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == playerWhoPlacedTheBomb)
            {
                Physics.IgnoreCollision(other.GetComponent<Collider>(), bomb.GetComponent<Collider>(), false);
            }
        }

        public void BeginExplode()
        {
            Invoke("Explode", 2f);
        }

        public void Explode()
        {
            Vector3 explosionPosition = bomb.transform.position;

            GameObject bigExplosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
            if (!isInTestMode)
            {
                Destroy(bigExplosion, explosionDuration);
            }
            else
            {
                DestroyImmediate(bigExplosion);
            }

            
            float nullRadius = 0.2f;

            Collider[] initialHits = Physics.OverlapSphere(explosionPosition, nullRadius);
            foreach (Collider hitCollider in initialHits)
            {
                if (hitCollider.CompareTag("PlayerObject"))
                {
                    if (!isInTestMode)
                    {
                        Destroy(hitCollider.gameObject);
                    }
                    else
                    {
                        DestroyImmediate(hitCollider.gameObject);
                    }
                }
                else if (hitCollider.CompareTag("Enemy"))
                {
                    if (!isInTestMode)
                    {
                        Destroy(hitCollider.gameObject);
                    }
                    else
                    {
                        DestroyImmediate(hitCollider.gameObject);
                    }
                }
            }

            foreach (Vector3 dir in directions)
            {
                StartCoroutine(DelayedExplosion(explosionPosition, dir));
            }
        }

        private IEnumerator DelayedExplosion(Vector3 explosionPosition, Vector3 dir)
        {
            for (float distance = 1.0f; distance <= radius; distance += 1.0f)
            {
                float delay = explosionDelay;
                Vector3 checkPosition = explosionPosition + dir.normalized * distance;

                yield return new WaitForSeconds(delay);

                if (Physics.Raycast(explosionPosition, dir, out RaycastHit hit, distance))
                {
                    Debug.Log(hit.collider.name);
                    if (hit.collider.CompareTag("Undestructible"))
                    {
                        yield break;
                    }
                    else if (hit.collider.CompareTag("Destructible"))
                    {
                        DestroyAndExplode(hit, checkPosition);
                        yield break;
                    }
                    else if (hit.collider.CompareTag("PlayerObject") || hit.collider.CompareTag("Enemy"))
                    {
                        DestroyAndExplode(hit, checkPosition);
                    }
                    else if (hit.collider.CompareTag("Bomb"))
                    {
                        hit.collider.GetComponent<BombExplosion>().Explode(); ;
                        Destroy(hit.collider.gameObject);
                        yield break;
                    }
                }
                else
                {
                    JustExplode(checkPosition);
                }
                
            }
        }

        public void DestroyAndExplode(RaycastHit hit, Vector3 checkPosition)
        {
            if (!isInTestMode)
            {
                Destroy(hit.collider.gameObject);
                GameObject explosionEffect = Instantiate(explosionPrefab, checkPosition, Quaternion.identity);
                Destroy(explosionEffect, explosionDuration);
            }
            else
            {
                DestroyImmediate(hit.collider.gameObject);
                GameObject explosionEffect = Instantiate(explosionPrefab, checkPosition, Quaternion.identity);
                DestroyImmediate(explosionEffect);
            }
        }

        public void JustExplode(Vector3 checkPosition)
        {
            GameObject explosionEffect = Instantiate(explosionPrefab, checkPosition, Quaternion.identity);
            if (!isInTestMode)
            {
                Destroy(explosionEffect, explosionDuration);
            }
            else
            {
                DestroyImmediate(explosionEffect);
            }
        }
    }
}

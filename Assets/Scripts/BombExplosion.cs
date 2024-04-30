using UnityEngine;
using GameLogic;

namespace GameLogic
{
    public class BombExplosion : MonoBehaviour
    {
        public GameObject bomb;
        public GameObject explosionPrefab;
        public GameObject playerWhoPlacedTheBomb;
        public float explosionDuration = 10.0f;
        public float radius = 1.0f;

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

            
            float nullRadius = 0f;

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
                RaycastHit[] hits = Physics.RaycastAll(explosionPosition, dir, radius);

                for (float distance = 1.0f; distance <= radius; distance += 1.0f)
                {
                    Vector3 checkPosition = explosionPosition + dir.normalized * distance;

                    if (Physics.Raycast(explosionPosition, dir, out RaycastHit hit, distance))
                    {
                        if (hit.collider.CompareTag("Undestructible"))
                        {
                            break;
                        }
                        else if (hit.collider.CompareTag("Destructible"))
                        {
                            if (!isInTestMode)
                            {
                                Destroy(hit.collider.gameObject);
                            }
                            else
                            {
                                DestroyImmediate(hit.collider.gameObject);
                            }
                        }
                        else if (hit.collider.CompareTag("PlayerObject"))
                        {
                            if (!isInTestMode)
                            {
                                Destroy(hit.collider.gameObject);
                            }
                            else
                            {
                                DestroyImmediate(hit.collider.gameObject);
                            }
                        }
                        else if (hit.collider.CompareTag("Enemy"))
                        {
                            if (!isInTestMode)
                            {
                                Destroy(hit.collider.gameObject);
                            }
                            else
                            {
                                DestroyImmediate(hit.collider.gameObject);
                            }
                        }
                    }

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


                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.CompareTag("Undestructible"))
                    {
                        break;
                    }
                    else if (hit.collider.CompareTag("Destructible"))
                    {
                        if (!isInTestMode)
                        {
                            Destroy(hit.collider.gameObject);
                        }
                        else
                        {
                            DestroyImmediate(hit.collider.gameObject);
                        }
                    }

                }
            }
        }
    }
}

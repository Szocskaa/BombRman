using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    public GameObject bomb;
    public GameObject explosionPrefab;
    public GameObject playerWhoPlacedTheBomb;
    public float explosionDuration =  1.0f;

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
        Destroy(bigExplosion, explosionDuration);
        float radius = 1.0f;
        float nullRadius = 0f;

        Collider[] initialHits = Physics.OverlapSphere(explosionPosition, nullRadius);
        foreach (Collider hitCollider in initialHits)
        {
            if (hitCollider.CompareTag("PlayerObject"))
            {
                Destroy(hitCollider.gameObject);
            }
            else if (hitCollider.CompareTag("Enemy"))
            {
                Destroy(hitCollider.gameObject);
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
                        Destroy(hit.collider.gameObject);
                    }
                    else if (hit.collider.CompareTag("PlayerObject"))
                    {
                        Destroy(hit.collider.gameObject);
                    }
                    else if (hit.collider.CompareTag("Enemy"))
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }

                GameObject explosionEffect = Instantiate(explosionPrefab, checkPosition, Quaternion.identity);
                Destroy(explosionEffect, explosionDuration);
            }

            
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("Undestructible"))
                {
                    break;
                }
                else if (hit.collider.CompareTag("Destructible"))
                {
                    Destroy(hit.collider.gameObject);
                }

            }
        }
    }
}

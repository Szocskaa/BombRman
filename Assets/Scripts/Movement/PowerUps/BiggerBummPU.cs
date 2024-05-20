using UnityEngine;

public class BiggerBummPU : MonoBehaviour
{
    public float explosionRadiusIncrease = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerObject"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.IncreaseExplosionRadius(explosionRadiusIncrease);
                Destroy(gameObject);
            }
        }
    }
}

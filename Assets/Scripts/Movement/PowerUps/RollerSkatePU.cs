using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RollerSkatePU : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerObject"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            StartCoroutine(RollerSkateCoroutine(player));
            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
        }
    }

    private IEnumerator RollerSkateCoroutine(PlayerMovement player)
    {
        if (!player.hasSpeedBoost)
        {
            player.playerSpeed = 8.0f;
            player.hasSpeedBoost = true;

            yield return new WaitForSeconds(7);

            player.playerSpeed = 4.0f;
            player.hasSpeedBoost = false;
        }
    }
}

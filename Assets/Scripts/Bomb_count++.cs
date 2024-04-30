using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseBombCount : MonoBehaviour
{
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); // Get the PlayerMovement script from the same GameObject
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerMovement.bombCount += 1;
            playerMovement.currentBombCount += 1;
            playerMovement.bombCooldown = 0f;
        }
    }
}
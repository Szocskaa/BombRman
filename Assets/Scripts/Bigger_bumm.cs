using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameLogic{
public class Bigger_bumm : MonoBehaviour
{
    public PlayerMovement playerMovement; // Reference to the BombExplosion script
        public bool igen = false; 
    // Start is called before the first frame update
    public void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); // Get the BombExplosion script from the same GameObject
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5) || igen )
        {
            playerMovement.radius_plus += 1;
        }
    }
}
}
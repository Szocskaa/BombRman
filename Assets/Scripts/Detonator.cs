using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class Detonator : MonoBehaviour
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
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                StartCoroutine(SetDetonatorTrueForSeconds(5));
            }
        }

        private IEnumerator SetDetonatorTrueForSeconds(float seconds)
        {
            playerMovement.detonator = true;
            yield return new WaitForSeconds(seconds);
            
        }
    }
}
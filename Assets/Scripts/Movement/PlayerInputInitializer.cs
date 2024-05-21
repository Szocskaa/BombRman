using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputInitializer : MonoBehaviour
{
    private PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.Log("PlayerInput component is missing on the GameObject.");
            return;
        }

        if (InputManager.Instance == null)
        {
            Debug.Log("InputManager.Instance is null. Ensure InputManager is initialized before PlayerInputInitializer.");
            return;
        }

        if (InputSettings.Instance == null)
        {
            Debug.Log("InputSettings.Instance is null. Ensure InputSettings is initialized before PlayerInputInitializer.");
            return;
        }

        InputManager.Instance.SetInputScheme(InputSettings.Instance.CurrentInputScheme);
        Debug.Log("Player Input initialized.");
    }
}

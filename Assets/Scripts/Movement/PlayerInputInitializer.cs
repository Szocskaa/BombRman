using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputInitializer : MonoBehaviour
{
    private PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if (InputManager.Instance != null)
        {
            InputManager.Instance.SetInputScheme(InputSettings.Instance.CurrentInputScheme);
        }
        Debug.Log("Player Input initialized.");



    }
}

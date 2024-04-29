using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetInputScheme(string scheme)
    {
        PlayerInput[] playerInputs = FindObjectsOfType<PlayerInput>();
        foreach (var playerInput in playerInputs)
        {
            InputDevice[] devices = InputSystem.devices.ToArray();
            playerInput.SwitchCurrentControlScheme(scheme, devices);
        }

        Debug.Log($"All player inputs switched to the scheme: {scheme}");
    }
}

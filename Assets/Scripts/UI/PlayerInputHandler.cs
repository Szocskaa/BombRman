using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig; // A játékos konfiguráció
    private PlayerMovement playerMovement; // A játékos mozgás script
    private PlayerControls controls; // Az Input System vezérlõk

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>(); // PlayerMovement inicializálása
        controls = new PlayerControls(); // PlayerControls példány
    }

    private void OnEnable()
    {
        controls.Player.Enable(); // Player input engedélyezése
    }

    private void OnDisable()
    {
        controls.Player.Disable(); // Player input letiltása
    }

    public void InitializePlayer(PlayerConfiguration config)
    {
        playerConfig = config;
        config.Input.onActionTriggered += OnActionTriggered; // Feliratkozás az akció indítási eseményekre
    }

    public PlayerConfiguration GetPlayerConfig()
    {
        return playerConfig;
    }

    private void OnActionTriggered(CallbackContext context)
    {
        if (context.action.name == "Movement")
        {
            HandleMovement(context); // Kezeljük a mozgást
        }
        else if (context.action.name == "PlaceBomb")
        {
            HandleBombPlace(context); // Kezeljük a bomba elhelyezését
        }
        else if (context.action.name == "Detonate")
        {
            HandleDetonate(context); // Kezeljük a detonációt
        }
    }

    private void HandleMovement(CallbackContext context)
    {
        if (playerMovement != null)
        {
            Vector2 movement = context.ReadValue<Vector2>();
            playerMovement.OnMove(context); // Adjuk át a mozgási bemeneteket

        }
    }

    private void HandleBombPlace(CallbackContext context)
    {
        if (context.performed)
        {
            playerMovement.OnBombPlace(context); // Kezeljük a bomba elhelyezését
        }
    }

    private void HandleDetonate(CallbackContext context)
    {
        if (context.performed)
        {
            playerMovement.OnDetonate(context); // Kezeljük a detonációt
        }
    }
}

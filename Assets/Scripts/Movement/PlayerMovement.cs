using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using GameLogic;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float playerSpeed = 4.0f;  // Speed setting
    private float gravity = -9.81f;  // Gravity settings
    [SerializeField] private Animator animator;  // Animator reference
    [SerializeField] private GameObject bombPrefab;  // Prefab for the bombs
    [SerializeField] public GameObject explosionPrefab;  // Prefab for explosions

    private CharacterController controller;  // CharacterController reference
    private Vector3 playerVelocity = Vector3.zero;  // Player's velocity
    private Vector2 movementInput = Vector2.zero;  // 2D input for movement
    private bool groundedPlayer;  // Check if player is grounded

    public bool hasSpeedBoost = false;
    public float bombCooldown = 2.0f;  // Default bomb cooldown (2 seconds)
    public int maxBombs = 1;  // Maximum bombs that can be placed
    public int availableBombs;  // Current available bombs
    private List<float> bombCooldownTimers = new List<float>();  // List of cooldown timers for bombs
    private List<GameObject> bombs = new List<GameObject>();  // List of placed bombs
    public bool detonator = false;  // Detonator mode
    public float radius_plus = 0f;
    public float destroyTime = 3f;
    private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
    private static readonly int MoveState = Animator.StringToHash("Base Layer.move");

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();  // Initialize CharacterController
        availableBombs = maxBombs;  // Initialize available bombs
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();  // Read 2D input
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    public void OnBombPlace(InputAction.CallbackContext context)
    {
        if (context.performed && availableBombs > 0 && CanPlaceBomb())
        {
            if (detonator)
            {
                PlaceBombDetonator();
            }
            else
            {
                PlaceBomb();
            }
            availableBombs--;  // Decrease available bombs
        }
    }

    public void OnDetonate(InputAction.CallbackContext context)
    {
        if (context.performed && detonator)
        {
            ExplodeDetonator();  // Detonate all placed bombs
        }
    }

    private void PlaceBomb()
    {
        if (bombPrefab != null)
        {
            Vector3 position = new Vector3(
                Mathf.RoundToInt(transform.position.x),
                transform.position.y,
                Mathf.RoundToInt(transform.position.z)
            );
            GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
            bomb.tag = "Bomb";
            BombExplosion bombExplosion = bomb.AddComponent<BombExplosion>();
            bombExplosion.radius += radius_plus;
            bombExplosion.bomb = bomb;
            bombExplosion.explosionPrefab = explosionPrefab;
            bombExplosion.BeginExplode();
            bombExplosion.playerWhoPlacedTheBomb = gameObject;
            Destroy(bomb, destroyTime);
            Physics.IgnoreCollision(bomb.GetComponent<Collider>(), gameObject.GetComponent<Collider>());

            bombs.Add(bomb);  // Add to list of active bombs
            bombCooldownTimers.Add(Time.time + bombCooldown);  // Add cooldown timer
        }
    }

    private void PlaceBombDetonator()
    {
        if (bombPrefab != null)
        {
            Vector3 position = new Vector3(
                Mathf.RoundToInt(transform.position.x),
                transform.position.y,
                Mathf.RoundToInt(transform.position.z)
            );
            GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
            bombs.Add(bomb);  // Add to list for detonation
        }
    }

    private void ExplodeDetonator()
    {
        foreach (GameObject bomb in bombs)
        {
            if (bomb != null)
            {
                bomb.GetComponent<BombExplosion>().Explode();  // Explode each bomb
                Destroy(bomb);
            }
        }
        bombs.Clear();  // Clear the list after detonation
        availableBombs = maxBombs;  // Reset available bombs
        bombCooldownTimers.Clear();  // Clear cooldown timers
    }

    public void IncreaseExplosionRadius(float increaseAmount)
    {
        radius_plus += increaseAmount;
        destroyTime += 0.2f;
        Debug.Log("Explosion radius increased to: " + radius_plus);
    }

    public void IncreaseBombCount()
    {
            maxBombs++;
            availableBombs = maxBombs;  // Update available bombs
            Debug.Log("Max bombs increased to: " + maxBombs);
    }

    private void Update()
    {
        groundedPlayer = controller.isGrounded;  // Check if grounded
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -0.1f;  // Reset vertical velocity
        }

        Vector3 move = Vector3.zero;  // Reset move vector
        if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
        {
            move.x = Mathf.Sign(movementInput.x);  // Horizontal movement
        }
        else if (movementInput != Vector2.zero)
        {
            move.z = Mathf.Sign(movementInput.y);  // Vertical movement
        }

        controller.Move(move * Time.deltaTime * playerSpeed);  // Apply movement

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;  // Face movement direction
            if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash != MoveState)  // Switch to move state
            {
                animator.CrossFade(MoveState, 0.1f);  // Transition to move animation
            }
        }
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash != IdleState)  // Switch to idle state
            {
                animator.CrossFade(IdleState, 0.1f);  // Transition to idle animation
            }
        }

        playerVelocity.y += gravity * Time.deltaTime;  // Apply gravity
        controller.Move(playerVelocity * Time.deltaTime);  // Move with gravity

        // Remove null bombs from the list
        bombs.RemoveAll(bomb => bomb == null);

        // Remove expired cooldowns and update available bombs
        for (int i = bombCooldownTimers.Count - 1; i >= 0; i--)
        {
            if (Time.time >= bombCooldownTimers[i])
            {
                bombCooldownTimers.RemoveAt(i);
                availableBombs++;  // Increment available bombs when cooldown expires
            }
        }
    }

    private bool CanPlaceBomb()
    {
        return bombCooldownTimers.Count < maxBombs;
    }
}
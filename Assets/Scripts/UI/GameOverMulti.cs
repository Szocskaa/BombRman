using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameOverMulti : MonoBehaviour
{
    public GameObject GameOverUI;
    public TextMeshProUGUI PlayerWinsText; // Reference to the TextMeshProUGUI component
    public Image GhostImage; // Reference to the Image component for the ghost image

    private AudioMixer audioMixer;
    private AudioMixerSnapshot unpausedSnapshot;
    private AudioMixerSnapshot gameOverSnapshot;
    private PlayerConfigurationManager playerConfigManager; // Reference to the PlayerConfigurationManager
    private PlayerScoreUIManager playerScoreUIManager; // Reference to the PlayerScoreUIManager

    private bool gameOverTriggered; // Add this boolean to track if the game over has been triggered

    void Start()
    {
        // Betöltjük az AudioMixer-t a Resources mappából
        audioMixer = Resources.Load<AudioMixer>("MainAudioMixer");

        // Lekérjük a Snapshot-okat
        unpausedSnapshot = audioMixer.FindSnapshot("Unpaused");
        gameOverSnapshot = audioMixer.FindSnapshot("Paused");

        playerConfigManager = PlayerConfigurationManager.Instance;
        playerScoreUIManager = FindObjectOfType<PlayerScoreUIManager>();

        InvokeRepeating("CheckPlayerExistence", 1f, 1f);
    }

    public void Restart()
    {
        unpausedSnapshot.TransitionTo(0.6f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    void CheckPlayerExistence()
    {
        if (gameOverTriggered) return; // Exit if game over has already been triggered

        Debug.Log("Checking player existence...");

        var players = GameObject.FindGameObjectsWithTag("PlayerObject");
        Debug.Log($"Number of players found: {players.Length / 2}");

        if (players.Length / 2 == 1)
        {
            Debug.Log("Only one player left, triggering game over.");
            gameOverTriggered = true; // Set the flag to true to prevent further execution

            // Azonosítjuk az utolsó életben maradt játékost
            var lastPlayer = players[0];
            if (lastPlayer == null)
            {
                Debug.LogError("Last player object is null.");
                return;
            }

            var playerInputHandler = lastPlayer.GetComponent<PlayerInputHandler>();
            if (playerInputHandler == null)
            {
                Debug.LogError("PlayerInputHandler component not found on the last player.");
                Debug.LogError($"Object name: {lastPlayer.name}, Object tags: {lastPlayer.tag}");
                return;
            }

            var playerConfig = playerInputHandler.GetPlayerConfig();
            if (playerConfig != null)
            {
                // Frissítjük a GameOver UI-t
                PlayerWinsText.text = $"Player {playerConfig.PlayerIndex + 1} Wins!";

                var colorName = playerConfig.playerMaterial?.name.ToLower().Replace(" ", "-");
                if (colorName != null && playerScoreUIManager.ghostSprites.TryGetValue($"ghost-{colorName}", out var sprite))
                {
                    GhostImage.sprite = sprite;
                }
                else
                {
                    Debug.LogWarning($"Ghost sprite for color '{colorName}' not found.");
                }

                // Növeljük a játékos pontszámát
                int newWins = playerScoreUIManager.GetPlayerScore(playerConfig.PlayerIndex) + 1;
                playerScoreUIManager.UpdatePlayerScore(playerConfig.PlayerIndex, newWins);
                playerConfigManager.UpdatePlayerWins(playerConfig.PlayerIndex, newWins); // Update the static dictionary
            }
            else
            {
                Debug.LogWarning("Player configuration not found for the last player.");
            }

            GameOverUI.SetActive(true);
            Invoke("PauseGame", 0.6f); // Pause the game after showing the game over screen
            gameOverSnapshot.TransitionTo(0.6f);
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Stop the game time
    }
}

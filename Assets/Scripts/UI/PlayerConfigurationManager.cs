using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;
    public static PlayerConfigurationManager Instance { get; private set; }
    public string SelectedLevel { get; private set; }

    private PlayerScoreUIManager playerScoreUIManager;

    // Static dictionary to keep track of player wins across scene loads
    private static Dictionary<int, int> playerWins = new Dictionary<int, int>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[Singleton] Trying to instantiate a second instance of a singleton class.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        playerConfigs = new List<PlayerConfiguration>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == SelectedLevel)
        {
            playerScoreUIManager = FindObjectOfType<PlayerScoreUIManager>();
            if (playerScoreUIManager != null)
            {
                // Initialize the UI elements with the playerConfigs
                playerScoreUIManager.InitializePlayerScores(playerConfigs);

                // Restore player wins from the static dictionary
                foreach (var config in playerConfigs)
                {
                    if (playerWins.ContainsKey(config.PlayerIndex))
                    {
                        playerScoreUIManager.UpdatePlayerScore(config.PlayerIndex, playerWins[config.PlayerIndex]);
                    }
                }
            }
        }
    }

    public void SetSelectedLevel(string levelName)
    {
        SelectedLevel = levelName;
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        if (pi == null)
        {
            Debug.LogError("Invalid PlayerInput");
            return;
        }

        Debug.Log($"Player {pi.playerIndex} joined using {pi.devices[0].displayName}");

        var device = pi.devices.FirstOrDefault();

        if (device != null)
        {
            Debug.Log($"Player {pi.playerIndex} joined using device: {device.displayName}");
        }

        pi.transform.SetParent(transform);

        if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            playerConfigs.Add(new PlayerConfiguration(pi));
        }

        // Debug log to check playerConfigs
        Debug.Log("Current player configurations:");
        foreach (var config in playerConfigs)
        {
            Debug.Log($"PlayerIndex: {config.PlayerIndex}, Device: {config.Input.devices[0].displayName}");
        }
    }

    public void HandlePlayerLeft(PlayerInput playerInput)
    {
        int playerIndex = playerInput.playerIndex;

        var playerConfig = playerConfigs.FirstOrDefault(p => p.PlayerIndex == playerIndex);

        if (playerConfig != null)
        {
            Debug.Log("Player left " + playerIndex);
            playerConfigs.Remove(playerConfig);
            Destroy(playerConfig.Input.gameObject);
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void SetPlayerColor(int index, Material color)
    {
        playerConfigs[index].playerMaterial = color;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].isReady = true;
        if (playerConfigs.All(p => p.isReady))
        {
            SceneManager.LoadScene(SelectedLevel);
        }
    }

    // Method to update player wins in the static dictionary
    public void UpdatePlayerWins(int playerIndex, int wins)
    {
        playerWins[playerIndex] = wins;
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }

    public PlayerInput Input { get; private set; }
    public int PlayerIndex { get; private set; }
    public bool isReady { get; set; }
    public Material playerMaterial { get; set; }
}

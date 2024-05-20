using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerScoreUIManager : MonoBehaviour
{
    public GameObject playerScorePanelPrefab; // A prefab for the player score UI
    public Transform scorePanelParent; // The parent transform for the score panels

    // Add a Dictionary to hold the different colored GhostImages
    private Dictionary<string, Sprite> ghostSprites = new Dictionary<string, Sprite>();

    private List<GameObject> playerScorePanels = new List<GameObject>();

    // Load the sprites in the Awake method
    private void Awake()
    {
        // Assuming the sprites are located in a Resources folder
        // Example: Resources/PlayerColors/ghost-blue, ghost-green, etc.
        var sprites = Resources.LoadAll<Sprite>("PlayerColors");
        foreach (var sprite in sprites)
        {
            ghostSprites[sprite.name] = sprite;
        }
    }

    // Call this method to spawn score panels for all players
    public void InitializePlayerScores(List<PlayerConfiguration> playerConfigs)
    {
        // Debug log to check playerConfigs
        Debug.Log($"Initializing player scores for {playerConfigs.Count} players.");

        // Destroy existing panels if any
        foreach (var panel in playerScorePanels)
        {
            Destroy(panel);
        }
        playerScorePanels.Clear();

        // Instantiate new panels
        foreach (var config in playerConfigs)
        {
            var panel = Instantiate(playerScorePanelPrefab, scorePanelParent);
            var playerNoText = panel.transform.Find("PlayerNo").GetComponent<TextMeshProUGUI>();
            var winsText = panel.transform.Find("Wins").GetComponent<TextMeshProUGUI>();
            var ghostImage = panel.transform.Find("GhostImage").GetComponent<Image>();

            if (playerNoText != null)
            {
                playerNoText.text = $"Player {config.PlayerIndex + 1}";
            }

            if (winsText != null)
            {
                winsText.text = "Wins: 0";
            }

            // Set the ghost image color based on the player's selected material
            if (ghostImage != null && config.playerMaterial != null)
            {
                var colorName = config.playerMaterial.name.ToLower().Replace(" ", "-");
                if (ghostSprites.TryGetValue($"ghost-{colorName}", out var sprite))
                {
                    ghostImage.sprite = sprite;
                }
                else
                {
                    Debug.LogWarning($"Sprite for color '{colorName}' not found.");
                }
            }

            playerScorePanels.Add(panel);

            // Debug log to confirm panel creation
            Debug.Log($"Created panel for Player {config.PlayerIndex + 1}");
        }
    }

    // Call this method to update the score of a specific player
    public void UpdatePlayerScore(int playerIndex, int score)
    {
        if (playerIndex >= 0 && playerIndex < playerScorePanels.Count)
        {
            var panel = playerScorePanels[playerIndex];
            var winsText = panel.transform.Find("Wins").GetComponent<TextMeshProUGUI>();
            if (winsText != null)
            {
                winsText.text = $"Wins: {score}";
            }
        }
    }
}

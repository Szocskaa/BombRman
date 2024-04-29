using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPositions;  // A spawn pozíciók listája
    [SerializeField] private GameObject ghostPrefab;  // A Ghost prefab
    private PlayerConfigurationManager playerConfigManager;  // A PlayerConfigurationManager példánya

    private void Start()
    {
        playerConfigManager = PlayerConfigurationManager.Instance;

        if (playerConfigManager == null)
        {
            Debug.LogError("PlayerConfigurationManager instance not found.");
            return;
        }

        var playerConfigs = playerConfigManager.GetPlayerConfigs();

        if (playerConfigs == null || playerConfigs.Count == 0)
        {
            Debug.LogError("No player configurations found.");
            return;
        }

        if (spawnPositions == null || spawnPositions.Count < playerConfigs.Count)
        {
            Debug.LogError("Not enough spawn positions for players.");
            return;
        }

        for (int i = 0; i < playerConfigs.Count; i++)
        {
            var spawnPosition = spawnPositions[i];
            var playerConfig = playerConfigs[i];

            var ghost = Instantiate(ghostPrefab, spawnPosition.position, spawnPosition.rotation);
            ghost.name = $"Ghost_{i}";

            // Skinned Mesh Renderer ellenõrzése és szín alkalmazása
            var skinnedMeshRenderer = ghost.GetComponent<SkinnedMeshRenderer>();
            if (skinnedMeshRenderer == null)
            {
                Debug.LogError($"Ghost {i} does not have a Skinned Mesh Renderer.");  // Logolj hibaüzenetet
            }
            else
            {
                if (playerConfig.playerMaterial != null)
                {
                    skinnedMeshRenderer.material = playerConfig.playerMaterial;  // Alkalmazd a játékos által kiválasztott színt
                    Debug.Log($"Ghost {i} material set to {playerConfig.playerMaterial.name}");  // Logold a szín alkalmazását
                }
                else
                {
                    Debug.LogError($"Ghost {i} does not have a valid playerMaterial.");  // Logolj hibaüzenetet
                }
            }

            // PlayerInputHandler inicializálása
            var inputHandler = ghost.GetComponent<PlayerInputHandler>();
            if (inputHandler != null)
            {
                inputHandler.InitializePlayer(playerConfig);  // Hozzáadjuk a PlayerConfig-ot a Ghost-hoz
                Debug.Log($"Ghost {i} initialized with PlayerInputHandler");
            }

            Debug.Log($"Ghost {i} spawned with {playerConfig.Input.devices[0].displayName} device.");
        }
    }
}

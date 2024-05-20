using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameLogic{
public class GhostSpawner : MonoBehaviour
{
    [SerializeField] public List<Transform> spawnPositions;  // A spawn poz�ci�k list�ja
    [SerializeField] public GameObject ghostPrefab;  // A Ghost prefab
    public PlayerConfigurationManager playerConfigManager;  // A PlayerConfigurationManager p�ld�nya

    public void Start()
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

            // Skinned Mesh Renderer ellen�rz�se �s sz�n alkalmaz�sa
            var skinnedMeshRenderer = ghost.GetComponent<SkinnedMeshRenderer>();
            if (skinnedMeshRenderer == null)
            {
                Debug.LogError($"Ghost {i} does not have a Skinned Mesh Renderer.");  // Logolj hiba�zenetet
            }
            else
            {
                if (playerConfig.playerMaterial != null)
                {
                    skinnedMeshRenderer.material = playerConfig.playerMaterial;  // Alkalmazd a j�t�kos �ltal kiv�lasztott sz�nt
                    Debug.Log($"Ghost {i} material set to {playerConfig.playerMaterial.name}");  // Logold a sz�n alkalmaz�s�t
                }
                else
                {
                    Debug.LogError($"Ghost {i} does not have a valid playerMaterial.");  // Logolj hiba�zenetet
                }
            }

            // PlayerInputHandler inicializ�l�sa
            var inputHandler = ghost.GetComponent<PlayerInputHandler>();
            if (inputHandler != null)
            {
                inputHandler.InitializePlayer(playerConfig);  // Hozz�adjuk a PlayerConfig-ot a Ghost-hoz
                Debug.Log($"Ghost {i} initialized with PlayerInputHandler");
            }

            Debug.Log($"Ghost {i} spawned with {playerConfig.Input.devices[0].displayName} device.");
        }
    }
}}

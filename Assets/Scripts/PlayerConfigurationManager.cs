using System.Collections;
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


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[Singleton] Trying to instantiate a second instance of a singleton class.");
            Destroy(gameObject);  // Megakad�lyozzuk a m�sodik p�ld�ny l�trej�tt�t
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);  // Biztos�tjuk, hogy a p�ld�ny ne t�nj�n el jelenetv�lt�skor
        playerConfigs = new List<PlayerConfiguration>();
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

        // A csatlakozott eszk�z t�pusa
        var device = pi.devices.FirstOrDefault();

        // Elmentheted az eszk�z nev�t vagy t�pus�t a playerConfig-ban
        if (device != null)
        {
            Debug.Log($"Player {pi.playerIndex} joined using device: {device.displayName}");
        }

        pi.transform.SetParent(transform);

        if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            playerConfigs.Add(new PlayerConfiguration(pi));
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
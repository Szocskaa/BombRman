using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;
    [SerializeField]
    private int MaxPlayers = 4  ;

    public static PlayerConfigurationManager Instance { get; private set; }

    public string SelectedLevel { get; private set; }  // A kiv�lasztott p�lya neve


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[Singleton] Trying to instantiate a seccond instance of a singleton class.");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }

    }

    public void SetSelectedLevel(string levelName)
    {
        SelectedLevel = levelName;  // Be�ll�tja a kiv�lasztott p�ly�t
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player joined " + pi.playerIndex);
        pi.transform.SetParent(transform);

        if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }

    public void HandlePlayerLeft(PlayerInput playerInput)
    {
        // A j�t�kos index�nek lek�rdez�se a PlayerInputb�l
        int playerIndex = playerInput.playerIndex;

        // Keresd meg a megfelel� j�t�kos konfigur�ci�t a playerConfigs list�ban
        var playerConfig = playerConfigs.FirstOrDefault(p => p.PlayerIndex == playerIndex);

        if (playerConfig != null)
        {
            Debug.Log("Player left " + playerIndex);
            playerConfigs.Remove(playerConfig);  // J�t�kos elt�vol�t�sa a list�b�l
            Destroy(playerConfig.Input.gameObject);  // J�t�kos GameObject elt�vol�t�sa
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
            SceneManager.LoadScene(SelectedLevel);  // Bet�lti a kiv�lasztott p�ly�t
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
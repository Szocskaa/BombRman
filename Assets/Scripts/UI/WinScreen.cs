using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class WinScreen : MonoBehaviour
{
    public GameObject WinUI;
    public TextMeshProUGUI WinsText; // Reference to the TextMeshProUGUI component for wins

    private AudioMixer audioMixer;
    private AudioMixerSnapshot unpausedSnapshot;
    private AudioMixerSnapshot winSnapshot;
    public static int totalWins = 0; // Static variable to store total wins

    void Start()
    {
        // Load the AudioMixer from the Resources folder
        audioMixer = Resources.Load<AudioMixer>("MainAudioMixer");

        // Get the snapshots
        unpausedSnapshot = audioMixer.FindSnapshot("Unpaused");
        winSnapshot = audioMixer.FindSnapshot("Paused");

        // Update the WinsText with the current total wins
        UpdateWinsText();

        // Start checking for enemy existence
        InvokeRepeating("CheckEnemyExistence", 1f, 1f);
    }

    public void Restart()
    {
        unpausedSnapshot.TransitionTo(0.6f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    void CheckEnemyExistence()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            totalWins++;
            UpdateWinsText();
            WinUI.SetActive(true);
            winSnapshot.TransitionTo(0.6f);
            CancelInvoke("CheckEnemyExistence"); // Stop checking for enemies
        }
    }

    void UpdateWinsText()
    {
        WinsText.text = $"Number of wins: {totalWins}";
    }

}

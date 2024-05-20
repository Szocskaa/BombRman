using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    private AudioMixer audioMixer;
    private AudioMixerSnapshot unpausedSnapshot;
    private AudioMixerSnapshot pausedSnapshot;

    void Start()
    {
        // Betöltjük az AudioMixer-t a Resources mappából
        audioMixer = Resources.Load<AudioMixer>("MainAudioMixer");

        // Lekérjük a Snapshot-okat
        unpausedSnapshot = audioMixer.FindSnapshot("Unpaused");
        pausedSnapshot = audioMixer.FindSnapshot("Paused");
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                StartCoroutine(PauseWithTransition());
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        unpausedSnapshot.TransitionTo(0.3f); // Átállítjuk a normál Snapshot-ra
    }

    IEnumerator PauseWithTransition()
    {
        pauseMenuUI.SetActive(true);
        pausedSnapshot.TransitionTo(0.3f); // Átállítjuk a szüneteltetett Snapshot-ra
        yield return new WaitForSecondsRealtime(0.3f); // Várunk, hogy az átmenet befejezõdjön
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        var playerConfigManager = FindObjectOfType<PlayerConfigurationManager>();
        if (playerConfigManager != null)
        {
            playerConfigManager.ResetPlayerWins();
            Destroy(playerConfigManager.gameObject);
        }
        SceneManager.LoadScene("MainMenu");
        unpausedSnapshot.TransitionTo(2.0f);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

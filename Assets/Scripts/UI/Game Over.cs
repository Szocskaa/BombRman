using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverUI;

    private AudioMixer audioMixer;
    private AudioMixerSnapshot unpausedSnapshot;
    private AudioMixerSnapshot gameOverSnapshot;

    void Start()
    {
        // Betöltjük az AudioMixer-t a Resources mappából
        audioMixer = Resources.Load<AudioMixer>("MainAudioMixer");

        // Lekérjük a Snapshot-okat
        unpausedSnapshot = audioMixer.FindSnapshot("Unpaused");
        gameOverSnapshot = audioMixer.FindSnapshot("Paused");

        InvokeRepeating("CheckPlayerExistence", 1f, 1f);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        unpausedSnapshot.TransitionTo(0.6f);
    }

    void CheckPlayerExistence()
    {
        GameObject playerObject = GameObject.FindWithTag("PlayerObject");
        if (playerObject == null)
        {
            GameOverUI.SetActive(true);
            gameOverSnapshot.TransitionTo(0.6f);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Start()
    {
        ;
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
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ExitToMainMenu()
    {
        // Id�ar�nyoss�g vissza�ll�t�sa
        Time.timeScale = 1f;

        // Megkeress�k �s megsemmis�tj�k a PlayerConfigurationManager p�ld�nyt
        var playerConfigManager = FindObjectOfType<PlayerConfigurationManager>();
        if (playerConfigManager != null)
        {
            Destroy(playerConfigManager.gameObject);
        }

        // Bet�ltj�k a f�men� jelenetet
        SceneManager.LoadScene("MainMenu");
    }


    public void Quit()
    {
        Application.Quit();
    }
}

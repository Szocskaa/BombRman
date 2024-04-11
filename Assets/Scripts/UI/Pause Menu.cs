using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Krivodeling.UI.Effects;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    UIBlur uiBlurScrpit; 

    // Start is called before the first frame update
    void Start()
    {
        uiBlurScrpit = GameObject.FindGameObjectWithTag("Blur").GetComponent<UIBlur>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
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
        uiBlurScrpit.BeginBlur(4f);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;

    }

    public void Quit()
    {
        Application.Quit();
    }
 

}

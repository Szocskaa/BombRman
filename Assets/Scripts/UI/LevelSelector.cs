using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public Button startButton;
    public TextMeshProUGUI titleText;
    public Image backgroundImage;
    public string[] levelNames;
    public Sprite[] levelBackgrounds;

    private int currentLevelIndex = 0;
    private string selectedLevel;

    void Start()
    {
        UpdateLevelDisplay();
    }
    

    public void ChangeLevel(int change)
    {
        int newLevelIndex = currentLevelIndex + change;

        if (newLevelIndex >= 0 && newLevelIndex < levelBackgrounds.Length)
        {
            currentLevelIndex = newLevelIndex;
            UpdateLevelDisplay();

            Debug.Log($"Current level selected: {levelNames[currentLevelIndex]}");
        }
        else
        {
            Debug.LogWarning("Invalid level index, skipping update.");
        }
    }



    public void StartLevel()
    {
        SceneManager.LoadScene(levelNames[currentLevelIndex]);
    }


    public void SetCurrentLevelInPlayerConfig()
    {
        if (PlayerConfigurationManager.Instance != null)
        {
            PlayerConfigurationManager.Instance.SetSelectedLevel(levelNames[currentLevelIndex]);
            
        }
    }

    private void UpdateLevelDisplay()
    {
        if (currentLevelIndex >= 0 && currentLevelIndex < levelNames.Length && currentLevelIndex < levelBackgrounds.Length)
        {
            titleText.text = levelNames[currentLevelIndex];
            backgroundImage.sprite = levelBackgrounds[currentLevelIndex];

            Debug.Log($"Display updated to level: {levelNames[currentLevelIndex]}");
        }
        else
        {
            Debug.Log("Cannot update level display, invalid level index.");
        }
    }

    public void OnRightButtonPressed()
    {
        ChangeLevel(1);
    }

    public void OnLeftButtonPressed()
    {
        ChangeLevel(-1);
    }
}

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
        // Új index számítása
        int newLevelIndex = currentLevelIndex + change;

        // Az érvényes index ellenõrzése
        if (newLevelIndex >= 0 && newLevelIndex < levelBackgrounds.Length)
        {
            currentLevelIndex = newLevelIndex;  // Ha érvényes, módosítsa az indexet
            UpdateLevelDisplay();  // Frissíti a kijelzõt

            Debug.Log($"Current level selected: {levelNames[currentLevelIndex]}");  // Debug log
        }
        else
        {
            Debug.LogWarning("Invalid level index, skipping update.");  // Figyelmeztetõ üzenet
        }
    }



    public void StartLevel()
    {
        SceneManager.LoadScene(levelNames[currentLevelIndex]);
    }

    public void GoToJoin()
    {
        SetCurrentLevelInPlayerConfig();
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
        titleText.text = levelNames[currentLevelIndex];
        backgroundImage.sprite = levelBackgrounds[currentLevelIndex];

        // Debug.Log hívás minden alkalommal, amikor a kijelzõ frissül
        Debug.Log($"Display updated to level: {levelNames[currentLevelIndex]}");
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

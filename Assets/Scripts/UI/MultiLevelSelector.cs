using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MulitLevelSelector : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public Button startButton;
    public TextMeshProUGUI titleText;
    public Image backgroundImage;
    public string[] multiLevelNames;
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

            Debug.Log($"Current level selected: {multiLevelNames[currentLevelIndex]}");  // Debug log
        }
        else
        {
            Debug.LogWarning("Invalid level index, skipping update.");  // Figyelmeztetõ üzenet
        }
    }



    public void StartLevel()
    {
        SceneManager.LoadScene(multiLevelNames[currentLevelIndex]);
    }


    public void GoToJoin()
    {
        SetCurrentLevelInPlayerConfig();
    }

    public void SetCurrentLevelInPlayerConfig()
    {
        if (PlayerConfigurationManager.Instance != null)
        {
            PlayerConfigurationManager.Instance.SetSelectedLevel(multiLevelNames[currentLevelIndex]);

        }
    }


    private void UpdateLevelDisplay()
    {
        titleText.text = multiLevelNames[currentLevelIndex];
        backgroundImage.sprite = levelBackgrounds[currentLevelIndex];

        // Debug.Log hívás minden alkalommal, amikor a kijelzõ frissül
        Debug.Log($"Display updated to level: {multiLevelNames[currentLevelIndex]}");
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

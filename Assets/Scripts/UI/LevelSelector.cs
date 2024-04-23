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
        // �j index sz�m�t�sa
        int newLevelIndex = currentLevelIndex + change;

        // Az �rv�nyes index ellen�rz�se
        if (newLevelIndex >= 0 && newLevelIndex < levelBackgrounds.Length)
        {
            currentLevelIndex = newLevelIndex;  // Ha �rv�nyes, m�dos�tsa az indexet
            UpdateLevelDisplay();  // Friss�ti a kijelz�t

            Debug.Log($"Current level selected: {levelNames[currentLevelIndex]}");  // Debug log
        }
        else
        {
            Debug.LogWarning("Invalid level index, skipping update.");  // Figyelmeztet� �zenet
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

        // Debug.Log h�v�s minden alkalommal, amikor a kijelz� friss�l
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

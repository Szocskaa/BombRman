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

    void Start()
    {
        UpdateLevelDisplay();
    }

    public void ChangeLevel(int change)
    {
        currentLevelIndex += change;

        if (currentLevelIndex >= levelBackgrounds.Length)
        {
            currentLevelIndex = levelBackgrounds.Length - 1;
            rightButton.interactable = false;
        }
        else if (currentLevelIndex < 0)
        {
            currentLevelIndex = 0;
            leftButton.interactable = false;
        }
        else
        {
            leftButton.interactable = true;
            rightButton.interactable = true;
        }

        UpdateLevelDisplay();
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(levelNames[currentLevelIndex]);
    }

    private void UpdateLevelDisplay()
    {
        titleText.text = levelNames[currentLevelIndex];
        backgroundImage.sprite = levelBackgrounds[currentLevelIndex];
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

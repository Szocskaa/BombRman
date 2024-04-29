using UnityEngine;

public class InputSettings : MonoBehaviour
{
    public static InputSettings Instance { get; private set; }
    public string CurrentInputScheme { get; private set; } = "ControllerStick"; // Default scheme

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetInputScheme(string scheme)
    {
        CurrentInputScheme = scheme;
    }
}

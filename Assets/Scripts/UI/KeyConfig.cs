using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyConfig : MonoBehaviour
{
    public static KeyConfig Instance { get; private set; }

    private string currentControlScheme;

    public Toggle wasdToggle;
    public Toggle arrowKeysToggle;
    public Toggle controllerStickToggle;
    public Toggle controllerDPadToggle;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        wasdToggle.onValueChanged.AddListener(delegate { UpdateInputScheme("KeyboardWASD", wasdToggle.isOn); });
        arrowKeysToggle.onValueChanged.AddListener(delegate { UpdateInputScheme("KeyboardArrows", arrowKeysToggle.isOn); });
        controllerStickToggle.onValueChanged.AddListener(delegate { UpdateInputScheme("ControllerStick", controllerStickToggle.isOn); });
        controllerDPadToggle.onValueChanged.AddListener(delegate { UpdateInputScheme("ControllerDPad", controllerDPadToggle.isOn); });
    }

    void UpdateInputScheme(string scheme, bool isOn)
    {
        if (isOn)
        {
            currentControlScheme = scheme;
            InputSettings.Instance.SetInputScheme(scheme);
            Debug.Log($"Input scheme updated to: {scheme}");
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenuManager : MonoBehaviour
{

    public TMP_Dropdown graphicsDropdown;
    public Slider masterVol, musicVol, sfxVol;
    public AudioMixer mainAudioMixer;
    public Toggle muteToggle;

    public void ChangeGrapihicsQuality()
    {
        if (graphicsDropdown != null)
        {
            QualitySettings.SetQualityLevel(graphicsDropdown.value);
            PlayerPrefs.SetInt("GraphicsQuality", graphicsDropdown.value);
            Debug.Log("The graphics has been set to: " + QualitySettings.GetQualityLevel());
        }
        else
        {
            Debug.LogError("graphicsDropdown is not assigned.");
        }
    }

    public void Mute(bool muted)
    {
        AudioListener.volume = muted ? 0 : 1;
    }

    public void ChangeMasterVolume()
    {
        if (mainAudioMixer != null)
        {
            mainAudioMixer.SetFloat("MasterVolumeMixer", masterVol.value);
        }
        else
        {
            Debug.Log("mainAudioMixer is not assigned.");
        }
    }

    public void ChangeMusicVolume()
    {
        if (mainAudioMixer != null)
        {
            mainAudioMixer.SetFloat("MusicVolumeMixer", musicVol.value);
        }
        else
        {
            Debug.Log("mainAudioMixer is not assigned.");
        }
    }

    public void ChangeSfxVolume()
    {
        if (mainAudioMixer != null)
        {
            mainAudioMixer.SetFloat("SFXVolumeMixer", sfxVol.value);
        }
        else
        {
            Debug.Log("mainAudioMixer is not assigned.");
        }
    }

    private void SaveSettings()
    {
        // Elmentjük a beállításokat
        if (graphicsDropdown != null && masterVol != null && musicVol != null && sfxVol != null && muteToggle != null)
        {
            PlayerPrefs.SetInt("GraphicsQuality", graphicsDropdown.value);
            PlayerPrefs.SetFloat("MasterVolumeMixer", masterVol.value);
            PlayerPrefs.SetFloat("MusicVolumeMixer", musicVol.value);
            PlayerPrefs.SetFloat("SFXVolumeMixer", sfxVol.value);
            PlayerPrefs.SetInt("Muted", muteToggle.isOn ? 1 : 0);
        }
        else
        {
            Debug.Log("One or more UI elements are not assigned.");
        }
    }

    private void LoadSettings()
    {
        // Betöltjük a beállításokat
        if (graphicsDropdown != null && masterVol != null && musicVol != null && sfxVol != null && muteToggle != null)
        {
            graphicsDropdown.value = PlayerPrefs.GetInt("GraphicsQuality", 2);
            masterVol.value = PlayerPrefs.GetFloat("MasterVolumeMixer", 0.75f);
            musicVol.value = PlayerPrefs.GetFloat("MusicVolumeMixer", 0.75f);
            sfxVol.value = PlayerPrefs.GetFloat("SFXVolumeMixer", 0.75f);
            muteToggle.isOn = PlayerPrefs.GetInt("Muted", 0) == 1;

            ChangeGrapihicsQuality();
            ChangeMasterVolume();
            ChangeMusicVolume();
            ChangeSfxVolume();
            Mute(muteToggle.isOn);
        }
        else
        {
            Debug.Log("One or more UI elements are not assigned.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadSettings();
    }

    void OnDisable()
    {
        SaveSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

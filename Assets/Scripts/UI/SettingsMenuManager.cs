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
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
        PlayerPrefs.SetInt("GraphicsQuality", graphicsDropdown.value);
    }

    public void Mute(bool muted)
    {
        AudioListener.volume = muted ? 0 : 1;
    }

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVolumeMixer",masterVol.value);
    }

    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVolumeMixer", musicVol.value);
    }

    public void ChangeSfxVolume()
    {
        mainAudioMixer.SetFloat("SFXVolumeMixer", sfxVol.value);
    }

    private void SaveSettings()
    {
        // Elmentj�k a be�ll�t�sokat
        PlayerPrefs.SetInt("GraphicsQuality", graphicsDropdown.value);
        PlayerPrefs.SetFloat("MasterVolumeMixer", masterVol.value);
        PlayerPrefs.SetFloat("MusicVolumeMixer", musicVol.value);
        PlayerPrefs.SetFloat("SFXVolumeMixer", sfxVol.value);
        PlayerPrefs.SetInt("Muted", muteToggle.isOn ? 1 : 0); // A n�m�t�s �llapot�nak ment�se
    }

    private void LoadSettings()
    {
        // Bet�ltj�k a be�ll�t�sokat
        graphicsDropdown.value = PlayerPrefs.GetInt("GraphicsQuality", 1);
        masterVol.value = PlayerPrefs.GetFloat("MasterVolumeMixer", 0.75f);
        musicVol.value = PlayerPrefs.GetFloat("MusicVolumeMixer", 0.75f);
        sfxVol.value = PlayerPrefs.GetFloat("SFXVolumeMixer", 0.75f);
        muteToggle.isOn = PlayerPrefs.GetInt("Muted", 0) == 1; // A n�m�t�s �llapot�nak bet�lt�se

        // Alkalmazzuk a be�ll�t�sokat
        ChangeGrapihicsQuality();
        ChangeMasterVolume();
        ChangeMusicVolume();
        ChangeSfxVolume();
        Mute(muteToggle.isOn); // A j�t�k n�m�t�s�nak alkalmaz�sa
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

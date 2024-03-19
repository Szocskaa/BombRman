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

    public void ChangeGrapihicsQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }

    public void changeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVolumeMixer",masterVol.value);
    }

    public void changeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVolumeMixer", musicVol.value);
    }

    public void changeSfxVolume()
    {
        mainAudioMixer.SetFloat("SFXVolumeMixer", sfxVol.value);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessProfileSwitcher : MonoBehaviour
{
    public PostProcessVolume postProcessVolume; 
    public PostProcessProfile highProfile; 
    public PostProcessProfile mediumProfile; 
    public PostProcessProfile lowProfile; 

    void Start()
    {
        UpdatePostProcessingProfile();
    }

    void UpdatePostProcessingProfile()
    {
        int qualityLevel = QualitySettings.GetQualityLevel();
        if (qualityLevel == 1) 
        {
            postProcessVolume.profile = mediumProfile;
        }
        else if (qualityLevel == 0) 
        {
            postProcessVolume.profile = lowProfile;
        }
        else 
        {
            postProcessVolume.profile = highProfile;
        }
    }
}


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    public Image imageToFade;

    private void OnEnable()
    {
        ResetFade();
        StartFadeOut(0.8f);
    }

    void ResetFade()
    {
        Color resetColor = imageToFade.color;
        resetColor.a = 1f;
        imageToFade.color = resetColor;
    }

    public void StartFadeOut(float duration)
    {
        StartCoroutine(FadeOutCoroutine(duration));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        float counter = 0;
        Color startColor = imageToFade.color;

        while (counter < duration)
        {
            counter += Time.unscaledDeltaTime;
            float alphaValue = Mathf.Lerp(1, 0, counter / duration);
            imageToFade.color = new Color(startColor.r, startColor.g, startColor.b, alphaValue);
            yield return null;
        }

        imageToFade.color = new Color(startColor.r, startColor.g, startColor.b, 0);
    }
}

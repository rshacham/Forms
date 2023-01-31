using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private float currentFadeTime = 0;
    [SerializeField] private float fadeTime;

    private SpriteRenderer fadeImage;

    [SerializeField] private bool fadeOutAtStart;

    public bool FadeOutAtStart
    {
        get => fadeOutAtStart;
        set => fadeOutAtStart = value;
    }

    private void Start()
    {
        fadeImage = GetComponent<SpriteRenderer>();
    }

    public IEnumerator StartFade(bool fadeIn, int sceneNum = -1, string sceneString = "")
    {
        float startValue = fadeTime;
        float endValue = 0;

        if (fadeIn)
        {
            startValue = 0;
            endValue = fadeTime;
        }

        float currentValue = startValue;

        while (Math.Abs(currentValue - endValue) > 0.01)
        {
            currentFadeTime += 0.02f;
            currentValue = Mathf.InverseLerp(startValue, endValue, currentFadeTime);
            fadeImage.color = new Color(0, 0, 0, currentValue);
            yield return new WaitForSeconds(0.02f);
        }
        
        

        if (sceneString != "" || sceneNum != -1)
        {
            GameManager.Manager.LoadNewScene(sceneString, sceneNum);
        }
    }
}

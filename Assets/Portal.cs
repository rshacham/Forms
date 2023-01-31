using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int sceneNum;
    [SerializeField] private string scene;

    private float currentFadeTime = 0;
    [SerializeField] private float fadeTime;

    [SerializeField] private SpriteRenderer fadeObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(StartFade(true));
    }
    
    IEnumerator StartFade(bool fadeIn)
    {
        var startValue = 1;
        var endValue = 0;

        if (fadeIn)
        {
            startValue = 0;
            endValue = 1;
        }

        float currentValue = startValue;

        while (Math.Abs(currentValue - endValue) > 0.05)
        {
            currentFadeTime += 0.05f;
            currentValue = Mathf.InverseLerp(startValue, endValue, currentFadeTime);
            fadeObject.color = new Color(0, 0, 0, currentValue);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void LoadNewScene()
    {
        if (scene != "")
        {
            SceneManager.LoadScene(scene);
            return;
        }

        SceneManager.LoadScene(sceneNum);
    }
}

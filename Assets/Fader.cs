using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fader : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float elapsedTime = 0;
    private float fadeTime = 5.0f;
    private int upperBound;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        upperBound = Convert.ToInt32(Math.Pow(fadeTime, fadeTime));
        StartCoroutine(DoFadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DoFadeIn()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            while(canvasGroup.alpha < 1)
            {
                elapsedTime += Time.deltaTime;
                // canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeTime);
                // I switched from linear to expotential fade progression
                canvasGroup.alpha = Mathf.Clamp01(Convert.ToSingle((Math.Pow(elapsedTime, fadeTime)))/upperBound);
                yield return null;
            }
            yield return null;
        }
}

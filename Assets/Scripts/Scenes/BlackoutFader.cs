using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlackoutFader : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float elapsedTime = 0;
    private float fadeTime = 3.0f;
    private int upperBound;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        upperBound = Convert.ToInt32(Math.Pow(fadeTime, fadeTime));
        StartCoroutine(DoFadeOut());
    }

    IEnumerator DoFadeOut()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            while(canvasGroup.alpha > 0)
            {
                elapsedTime += Time.deltaTime;
                // canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeTime);   // Linear
                // I switched from linear to expotential fade progression
                canvasGroup.alpha = 1 - Mathf.Clamp01(Convert.ToSingle((Math.Pow(elapsedTime, fadeTime)))/upperBound);  // Exponential
                yield return null;
            }
            yield return null;
        }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BackgroundFader : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float elapsedTime = 0;
    private float fadeTime = 2f;
    private int upperBound;
    private LinkedList<SpriteRenderer> sprites = new LinkedList<SpriteRenderer>();
    // Start is called before the first frame update
    private Animation creditsRoll;
    private Animation creditsButtons;

    void Start()
    {
        Transform transform;
        transform = GetComponent<Transform>();
        upperBound = Convert.ToInt32(Math.Pow(fadeTime, fadeTime));
        grabSprites(transform);
        StartCoroutine(DoFadeIn());
    }

    void grabSprites(Transform objects)
    {
        // Base Case
        if (objects.childCount == 0)
        {
            SpriteRenderer sprite = objects.GetComponent<SpriteRenderer>();
            Color tmp = sprite.color;
            tmp.a = 0;
            sprite.color = tmp;
            sprites.AddLast(sprite);
            // Debug.Log(sprite.name + " - " + sprite.color.a);
        }
        else
        {
            for (int i = 0; i < objects.childCount; i++)
            {
                grabSprites(objects.GetChild(i));
            }
        }
    }

    IEnumerator DoFadeIn()
    {
        LinkedListNode<SpriteRenderer> node = sprites.First;
        // Use first node as check because all others will match
        while(node.Value.color.a < 1)
        {
            elapsedTime += Time.deltaTime;
            while (node.Next != null)
            {
                Color tmp = node.Value.color;
                // I switched from linear to expotential fade progression
                tmp.a = Mathf.Clamp01(Convert.ToSingle((Math.Pow(elapsedTime, fadeTime)))/upperBound);
                node.Value.color = tmp;

                // Debug.Log(node.Value.name + " - " + node.Value.color);

                node = node.Next;
            }
            node = sprites.First;
            yield return null;
        }
        yield return null;
    }

    void StartCreditsScene()
    {
        PlayCreditsRoll();
        PlayButtonFade();
    }

    void PlayButtonFade()
    {
        creditsButtons.Play();
    }

    void PlayCreditsRoll()
    {
        creditsRoll.Play();
    }
}

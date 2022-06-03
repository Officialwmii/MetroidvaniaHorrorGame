using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitlesText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleText = default;

    public static SubtitlesText instance;

    private void Awake()
    {
        instance = this;
        ClearSubtitles();
    }


    public void SetSubtitle(string subtitle)
    {
        subtitleText.text = subtitle;

        StartCoroutine(ClearAfterSeconds());
    }

    public void ClearSubtitles()
    {
        subtitleText.text = "";

        
    }

    private IEnumerator ClearAfterSeconds()
    {
        yield return new WaitForSeconds(25f);
        ClearSubtitles();
    }
}

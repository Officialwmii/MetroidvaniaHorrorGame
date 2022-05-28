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
    }

    public void ClearSubtitles()
    {
        subtitleText.text = "";

        
    }

    private IEnumerator ClearAfterSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        ClearSubtitles();
    }
}

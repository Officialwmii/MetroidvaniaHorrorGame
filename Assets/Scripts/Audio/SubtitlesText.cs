using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitlesText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleText = default;

    public static SubtitlesText instance;
   private string tempsub;
    private int maxlines;
    private float LineDuration;
    private void Awake()
    {
        instance = this;
        ClearSubtitles();
    }

    public void SetSubtitle(string subtitle, float duration)
    { 
        subtitleText.text = subtitle;
            StartCoroutine(ClearAfterSeconds(duration));
    }

    public void SetAudioLogSubtitle(int LogNumber)
    {
        StartCoroutine(CorutineAudioLogs(LogNumber, 0, 10));
    }

    private IEnumerator CorutineAudioLogs(int AudioLogNr, int CurrentLine, int MaxLines) {


        CurrentLine++;
        LineDuration = 1;
        switch (AudioLogNr){
            case 1:
                maxlines =6;
                switch (CurrentLine){
                    case 1: tempsub = "Playback of audiolog #"+ AudioLogNr;  LineDuration = 3.5f; break;
                    case 2: tempsub = "Listen. [pained grunt]"; LineDuration = 3.3f;  break;
                    case 3: tempsub = "Starlight. I… can control it still. I… "; LineDuration = 3.2f; break;
                    case 4: tempsub = "What we found on Vigil Prime cannot leave this vessel."; LineDuration = 3.3f; break;
                    case 5: tempsub = "Find the keys. Initiate the scuttle."; LineDuration = 3.25f; break;
                    case 6: tempsub = "Save… [pained scream]."; LineDuration = 3.4f; break;
                } break;


        }

        subtitleText.text = tempsub;

        yield return new WaitForSeconds(LineDuration);
        if(CurrentLine<=maxlines)StartCoroutine(CorutineAudioLogs(AudioLogNr, CurrentLine , MaxLines));
        else ClearSubtitles();
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

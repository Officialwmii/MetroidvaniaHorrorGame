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
        //change to this function in the pickup class when the duration is implmented.

        StartCoroutine(CorutineAudioLogs(LogNumber, 0, 10));
    }

    private IEnumerator CorutineAudioLogs(int AudioLogNr, int CurrentLine, int MaxLines) {


        CurrentLine++;
        LineDuration = 1;
        switch (AudioLogNr){
            case 1:
                maxlines =6;
                switch (CurrentLine){
                    case 1: tempsub = "Playback of audiolog #"+ AudioLogNr;  LineDuration = 4f; break;
                    case 2: tempsub = "Listen. [pained grunt]"; LineDuration = 2.5f;  break;
                    case 3: tempsub = "Starlight. I... can control it still. I..."; LineDuration = 3.2f; break;
                    case 4: tempsub = "What we found on Vigil Prime cannot leave this vessel."; LineDuration = 3.3f; break;
                    case 5: tempsub = "Find the keys. Initiate the scuttle."; LineDuration = 3.25f; break;
                    case 6: tempsub = "Save… [pained scream]."; LineDuration = 3.4f; break;
                } break;
            case 2:
                maxlines = 5;
                switch (CurrentLine)
                {
                    case 1: tempsub = "Playback of audiolog #" + AudioLogNr; LineDuration = 4f; break;
                    case 2: tempsub = "Crissa, Starlight, I need you to understand why I did what I did."; LineDuration = 3f; break;
                    case 3: tempsub = "I had no choice. This whole thing is a nightmare and..."; LineDuration = 4.5f; break;
                    case 4: tempsub = "and I have no one else to talk to. No one left to listen."; LineDuration = 7f; break;
                    case 5: tempsub = "So, lend me your ear."; LineDuration = 2f; break;
                }
                break;
            case 3:
                maxlines = 7;
                switch (CurrentLine)
                {
                    case 1: tempsub = "Playback of audiolog #" + AudioLogNr; LineDuration = 4f; break;
                    case 2: tempsub = "So much blood. They..."; LineDuration = 2f; break;
                    case 3: tempsub = "The quarantine didn’t help."; LineDuration = 3f; break;
                    case 4: tempsub = "They ripped Kris to shreds. "; LineDuration = 4f; break;
                    case 5: tempsub = "So we shot them. I shot them."; LineDuration = 2f; break;
                    case 6: tempsub = "Starlight I hope you never see this."; LineDuration = 2f; break;
                    case 7: tempsub = "I hope you never hear this."; LineDuration = 2f; break;
                }
                break;
            case 4:
                maxlines = 7;
                switch (CurrentLine)
                {
                    case 1: tempsub = "Playback of audiolog #" + AudioLogNr; LineDuration = 4f; break;
                    case 2: tempsub = "I... I forget how many sectors we have on this vessel."; LineDuration = 3.5f; break;
                    case 3: tempsub = "I’ve forgotten the faces of the ones I killed."; LineDuration = 2.5f; break;
                    case 4: tempsub = "But not their screams."; LineDuration = 3.5f; break;
                    case 5: tempsub = "What is? Why did..."; LineDuration = 2.5f; break;
                    case 6: tempsub = "What.. What happened?"; LineDuration = 2.5f; break;
                    case 7: tempsub = "No, Starlight. I must focus."; LineDuration = 5f; break;
                }
                break;
            case 5:
                maxlines = 7;
                switch (CurrentLine)
                {
                    case 1: tempsub = "Playback of audiolog #" + AudioLogNr; LineDuration = 4f; break;
                    case 2: tempsub = "I drift in and out of existence."; LineDuration = 2.5f; break;
                    case 3: tempsub = "I walk through the corridors and I do not know why."; LineDuration = 3.5f; break;
                    case 4: tempsub = "But I know you, Starlight."; LineDuration = 2.5f; break;
                    case 5: tempsub = "And I know there is no other option."; LineDuration = 2f; break;
                    case 6: tempsub = "Me, the bosun, the chief engineer, we are compromised."; LineDuration = 4f; break;
                    case 7: tempsub = "That’s why the keys... [recording abruptly ends]"; LineDuration = 2f; break;
                }
                break;
            case 6:
                maxlines = 6;
                switch (CurrentLine)
                {
                    case 1: tempsub = "Playback of audiolog #" + AudioLogNr; LineDuration = 4f; break;
                    case 2: tempsub = "It speaks to me, Crissa."; LineDuration = 2f; break;
                    case 3: tempsub = "To us, Crissa."; LineDuration = 2.5f; break;
                    case 4: tempsub = "It speaks to us -- to me -- to us."; LineDuration = 1.5f; break;
                    case 5: tempsub = "It beckons and its message is crystal in my ear and it is sweet. "; LineDuration = 3.5f; break;
                    case 6: tempsub = "We must wake up. We must wake up. We must wake up."; LineDuration = 4.5f; break;
                }
                break;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public AK.Wwise.Event audioLog1;
    public AK.Wwise.Event audioLog2;
    public AK.Wwise.Event audioLog3;
    public AK.Wwise.Event audioLog4;
    public AK.Wwise.Event audioLog5;
    public AK.Wwise.Event audioLog6;
    public GameObject Log1;
    public GameObject Log2;
    public GameObject Log3;
    public GameObject Log4;
    public GameObject Log5;
    public GameObject Log6;
    public static DialogueSystem instance;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    private void Start()
    {
        // Wwise event might go here goes here
    }

    public void PlayAudioLog(int log)
    {
        if(log == 1)
        {
            audioLog1.Post(Log1);
        }
        else if(log == 2)
        {
            audioLog2.Post(Log2);
        }
        else if (log == 3)
        {
            audioLog3.Post(Log3);
        }
        else if (log == 4)
        {
            audioLog4.Post(Log4);
        }
        else if (log == 5)
        {
            audioLog5.Post(Log5);
        }
        else if (log == 6)
        {
            audioLog6.Post(Log6);
        }
    }
}

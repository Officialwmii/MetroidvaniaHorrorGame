using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    public AK.Wwise.Event select;
    public AK.Wwise.Event pause;
    public AK.Wwise.Event back;

    public GameObject button;

    public void SelectSound()
    {
        select.Post(button);
    }

    public void PauseSound()
    {
        pause.Post(button);
    }

    public void BackSound()
    {
        back.Post(button);
    }
}

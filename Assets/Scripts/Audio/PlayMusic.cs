using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AK.Wwise.Event playMusic;

    public GameObject player;

    void Start()
    {
        PlayGameplayMusic();
    }

    public void PlayGameplayMusic()
    {
        playMusic.Post(player);
    }
    void OnDestroy() { StopGameplayMusic(); }

    public void StopGameplayMusic()
    {
        Debug.Log("stopmusci");
        playMusic.Stop(player);
    }

}


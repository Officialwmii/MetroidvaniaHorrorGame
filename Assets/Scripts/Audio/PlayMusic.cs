using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AK.Wwise.Event playMusic;

    public GameObject player;

    void Start()
    {
        
    }

    public void PlayGameplayMusic()
    {
        playMusic.Post(player);
    }
    void OnDestroy()
    {
        StopGameplayMusic();
    }

    public void StopGameplayMusic()
    {
        Debug.Log("stopmusic");
        playMusic.Stop(player);
    }

    public void StartBaseMusic()
    {
        AkSoundEngine.SetState("Player_State", "Alive");
        AkSoundEngine.SetState("Music_State", "Gameplay");
        AkSoundEngine.SetState("Music_Segments", "Start");
    }

}
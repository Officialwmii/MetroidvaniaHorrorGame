using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnnouncements : MonoBehaviour
{
    public string location;
    public AudioNode subtitles;
    public AK.Wwise.Event shipAIAnnouncement;
    private float duration = 0;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")&& duration<=0)
        {
            if (location == "BossRoom") {
                if (EventManager.ConstalationsKeysAcquired < 3)
                    PlayAudio(col);
            }
            else {
                PlayAudio(col);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (duration > 0) {
            duration = duration - Time.deltaTime;
        }
    }


     void PlayAudio(Collider2D col) {
        shipAIAnnouncement.Post(col.gameObject);
        SubtitlesText.instance.SetSubtitle(subtitles.subtitle, subtitles.duration);
        duration = 10;
    }

}

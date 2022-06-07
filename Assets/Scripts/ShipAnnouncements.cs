using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnnouncements : MonoBehaviour
{
    public string location;
    public AudioNode subtitles;
    public AK.Wwise.Event shipAIAnnouncement;


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            shipAIAnnouncement.Post(col.gameObject);
            SubtitlesText.instance.SetSubtitle(subtitles.subtitle, subtitles.duration);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceAreaBoundary : MonoBehaviour
{
    public string areaName;

    private GameObject player;

    public void PlayAreaAmbience(string area)
    {
        areaName = area;
        if(area == "Stairway")
        {
            AkSoundEngine.SetSwitch("Player_Location", "Hallway", GameObject.Find("Wwise_Parameters"));
            // Play Ambience Switch Container
        }
        else if( area == "Ship_Hull")
        {
            AkSoundEngine.SetSwitch("Player_Location", "Ship_Hull", GameObject.Find("Wwise_Parameters"));
        }
        else if (area == "Escape_Pod")
        {
            AkSoundEngine.SetSwitch("Player_Location", "Escape_Pod", GameObject.Find("Wwise_Parameters"));
        }
        else if (area == "Medbay")
        {
            AkSoundEngine.SetSwitch("Player_Location", "Medbay", GameObject.Find("Wwise_Parameters"));
        }
        else if (area == "Warehouse")
        {
            AkSoundEngine.SetSwitch("Player_Location", "Warehouse", GameObject.Find("Wwise_Parameters"));
        }
        else if (area == "Bridge")
        {
            AkSoundEngine.SetSwitch("Player_Location", "Bridge", GameObject.Find("Wwise_Parameters"));
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        PlayAreaAmbience(areaName);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // 
            Debug.Log("Ambience Switch");
            PlayAreaAmbience(areaName);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            
        }
    }
}

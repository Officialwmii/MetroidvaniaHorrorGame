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
        if(area == "escapePod")
        {
            // Switch Ambience Switch to Ship hull
            // Play Ambience Switch Container
        }
        else if( area == "shipHull")
        {

        }
        else if (area == "shipHull")
        {

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
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
            AkSoundEngine.PostEvent("Stop_Ambience", player);
        }
    }
}

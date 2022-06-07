using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceAreaBoundary : MonoBehaviour
{
    public string areaName;

    private GameObject player;
    public GameObject wwiseParaments;

    public void PlayAreaAmbience(string area)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

}

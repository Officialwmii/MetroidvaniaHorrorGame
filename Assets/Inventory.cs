using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{

    private GameObject Health;
    private GameObject Fuel;
    private GameObject Cryo;
    private GameObject Logs;
    private GameObject Artifacts;
    private GameObject Keys;
    private GameObject Stun;
    private GameObject Refill;

    private GameObject Key1;
    private GameObject Key2;
    private GameObject Key3;

    private GameObject Artifact1;
    private GameObject Artifact2;
    private GameObject Artifact3;
    private GameObject Artifact4;
    private GameObject Artifact5;
    private GameObject SuitGUI;
    private GameObject Jetpack1;
    private GameObject Jetpack2;
    private GameObject SunGunUI;

    private GameObject Time;
    private GameObject Map;

    void Start()
    {
        Health = GameObject.Find("Health");
        Fuel = GameObject.Find("Fuel");
        Cryo = GameObject.Find("Cryo");
        Logs = GameObject.Find("Logs");
        Artifacts = GameObject.Find("Artifacts");
        Keys = GameObject.Find("Keys");
        Stun = GameObject.Find("Stun");
        Refill = GameObject.Find("Refill");

        Key1 = GameObject.Find("Key1");
        Key2 = GameObject.Find("Key2");
        Key3 = GameObject.Find("Key3");

        Artifact1 = GameObject.Find("Artifact1");
        Artifact2 = GameObject.Find("Artifact2");
        Artifact3 = GameObject.Find("Artifact3");
        Artifact4 = GameObject.Find("Artifact4");
        Artifact5 = GameObject.Find("Artifact5");

        SuitGUI = GameObject.Find("SuitGUI");
        Jetpack1 = GameObject.Find("Jetpack1");
        Jetpack2 = GameObject.Find("Jetpack2");
        SunGunUI = GameObject.Find("SunGunUI");

        Time = GameObject.Find("Time");
        Map = GameObject.Find("Map");


        OnEnable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log("Gets active.");
        Health.GetComponent<TMP_Text>().text = "" + ((EventManager.MaxLives - 4) * 3 + EventManager.LifeShards) + "/15 " + (EventManager.MaxLives) + " HP";
        Fuel.GetComponent<TMP_Text>().text = "" + ((EventManager.MaxFuel - 50)/10) + "/10 " + (EventManager.MaxFuel) + " Fuel";
        Cryo.GetComponent<TMP_Text>().text = "" + ((EventManager.MaxGrenades-2)) + "/3 Cryo G." ;
        Logs.GetComponent<TMP_Text>().text = "" + ((EventManager.AudioLog )) + "/5 " + " Logs";
        Artifacts.GetComponent<TMP_Text>().text = "" + ((EventManager.Collectables-EventManager.AudioLog)) + "/5 " + " Alien Artifacts";
        Keys.GetComponent<TMP_Text>().text = "" + (EventManager.ConstalationsKeysAcquired) + "/3 " + " Con. P.";
        Stun.GetComponent<TMP_Text>().text = "" + ((EventManager.stunUpgrade-1 )) + "/5 " +EventManager.CooldownTime +"s Stun";
        Refill.GetComponent<TMP_Text>().text = "" + (EventManager.FuelRefillNumberOfUpgrades-1)  + "/5 +" +  " Refill";


        //Keys
        Key1.SetActive(false);
        Key2.SetActive(false);
        Key3.SetActive(false);

        if (EventManager.ConstalationsKeysAcquired >= 1) Key1.SetActive(true);
        if (EventManager.ConstalationsKeysAcquired >= 2) Key2.SetActive(true);
        if (EventManager.ConstalationsKeysAcquired >= 3) Key3.SetActive(true);

        Artifact1.SetActive(false);
        Artifact2.SetActive(false);
        Artifact3.SetActive(false);
        Artifact4.SetActive(false);
        Artifact5.SetActive(false);

        if (EventManager.Collectables >= 1) Artifact1.SetActive(true);
        if (EventManager.Collectables >= 2) Artifact2.SetActive(true);
        if (EventManager.Collectables >= 3) Artifact3.SetActive(true);
        if (EventManager.Collectables >= 4) Artifact4.SetActive(true);
        if (EventManager.Collectables == 5) Artifact5.SetActive(true);

        SuitGUI.SetActive(false);
        Jetpack1.SetActive(false);
        Jetpack2.SetActive(false);
        SunGunUI.SetActive(false);

        if (EventManager.HasArmour) SuitGUI.SetActive(true);
        if (EventManager.HasJetpack) Jetpack1.SetActive(true);
        if (EventManager.HasDoubleJetpack) Jetpack2.SetActive(true);
        if (EventManager.HasRocketLauncher) SunGunUI.SetActive(true);

        Time.GetComponent<TMP_Text>().text = 
                Mathf.FloorToInt(EventManager.Timer / (60*60)).ToString("D2") + ":" +
                Mathf.FloorToInt(EventManager.Timer / 60).ToString("D2") + ":" +
                Mathf.FloorToInt(EventManager.Timer % 60).ToString("D2");

        Map.GetComponent<TMP_Text>().text = "Map: "+ ((float)Mathf.FloorToInt(EventManager.MapProgress/396)*100)+"%";
        Debug.Log(EventManager.MapProgress);

    }

}

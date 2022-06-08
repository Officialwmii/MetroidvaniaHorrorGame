using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;
    public GameObject CheatPlayerSpawn;


    void Start()
    {
        Player = GameObject.Find("Player");
        CheatPlayerSpawn = GameObject.Find("CheatPlayerSpawn");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8) && Input.GetKeyDown(KeyCode.Alpha9) && Input.GetKeyDown(KeyCode.Alpha0))
        {
            Player.transform.position = CheatPlayerSpawn.transform.position;
            EventManager.HasArmour = true;
            EventManager.HasDoubleJetpack = true;
            EventManager.HasJetpack = true;
            EventManager.HasRocketLauncher = true;
            EventManager.FuelRefillNumberOfUpgrades = 4;
            EventManager.ConstalationsKeysAcquired = 3;

        }

        if (Input.GetKeyDown(KeyCode.Alpha5) && Input.GetKeyDown(KeyCode.Alpha6) && Input.GetKeyDown(KeyCode.Alpha7)) {
            EventManager.GetAllUpgrades();



        }

    }
}

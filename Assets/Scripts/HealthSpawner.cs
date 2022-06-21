using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class HealthSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spawner_1;
    public GameObject spawner_2;
    public GameObject spawner_3;
    public GameObject spawner_4;
    public GameObject spawner_5;
    public GameObject health_pickup;
    public GameObject grenade_pickup;
    public GameObject fuel_pickup;
    public GameObject pickupParent;
    private GameObject player;
    public bool pickupsPresent = false;
    public bool playerFullHealth = true;
    [Header("Timer")]
    public float phase_timer = 60f;
    public float timer;
    public float trigger_time = 5f;


    // Update is called once per frame
    private void Awake()
    {
        timer = phase_timer;
        spawnPickups();
        player = GameObject.FindWithTag("Player");
        pickupsPresent = true;
        //Debug.Log(player.GetComponent<CharacterController2D>().life);
        //Debug.Log("AYYYOOOO");
        if (player.GetComponent<CharacterController2D>().life < 4)
        {
            playerFullHealth = false;
        }
        if (player.GetComponent<CharacterController2D>().life == 4)
        {
            playerFullHealth = true;
        }
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (player.GetComponent<CharacterController2D>().life == 4)
        {
            playerFullHealth = true;
        }
        if (player.GetComponent<CharacterController2D>().life < 4)
        {
            playerFullHealth = false;
        }
        if (timer <= trigger_time)
        {
            checkPickups(pickupParent.transform);
            if (pickupsPresent == false && playerFullHealth == false)
            {
                spawnPickups();
                pickupsPresent = true;
                timer = phase_timer;
            }
            
        }
    }

    void spawnPickups()
    {
        Instantiate(health_pickup, spawner_1.transform.position, spawner_1.transform.rotation, pickupParent.transform);
        Instantiate(health_pickup, spawner_2.transform.position, spawner_2.transform.rotation, pickupParent.transform);
        Instantiate(health_pickup, spawner_3.transform.position, spawner_3.transform.rotation, pickupParent.transform);
        Instantiate(health_pickup, spawner_4.transform.position, spawner_4.transform.rotation, pickupParent.transform);
        Instantiate(health_pickup, spawner_5.transform.position, spawner_4.transform.rotation, pickupParent.transform);
    }

    void checkPickups(Transform spawnParent)
    {
        if (spawnParent.childCount == 0)
        {
            pickupsPresent = false;
        }
    }
}

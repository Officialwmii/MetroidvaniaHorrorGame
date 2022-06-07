using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEntrance : MonoBehaviour
{

    public GameObject EndDestination;
    public GameObject player;

    public bool goingUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {

            col.transform.position = EndDestination.transform.position;
            //Debug.Log("Elevator activated");
            if (goingUp)
            {
                AkSoundEngine.PostEvent("Elevator_Up", player);
                AkSoundEngine.PostEvent("SHIP_SYSTEM_ANNOUNCEMENT_4", player);
            }
            else
            {
                AkSoundEngine.PostEvent("Elevator_Down", player);
                AkSoundEngine.PostEvent("SHIP_SYSTEM_ANNOUNCEMENT_5", player);
            }
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEntrance : MonoBehaviour
{

    public GameObject EndDestination;

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
        }
    }
}


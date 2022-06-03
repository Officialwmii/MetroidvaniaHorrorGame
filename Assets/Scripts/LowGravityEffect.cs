using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowGravityEffect : MonoBehaviour
{
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
            Debug.Log("Low gravity");
            col.GetComponent<PlayerMovement>().variableGravity= 0.25f;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerMovement>().variableGravity = 2f;



        }
    }

}

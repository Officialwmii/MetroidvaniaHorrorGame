using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{

    private bool active = false; 

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Renderer>().enabled = false;
        //EventManager.AddMapSegment();
        //Debug.Log(EventManager.MapProgress);
    }

    // Update is called once per frame
    void Update()
    {
        if ((ShowMap.showMap && active)||EventManager.CheatMapVisibility)
        { this.GetComponent<Renderer>().enabled = true; }
        
        else 
        { this.GetComponent<Renderer>().enabled = false; }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        if (col.CompareTag("Player") && active == false)
        { active = true; EventManager.AddMapSegment(); } 
    
    }
}

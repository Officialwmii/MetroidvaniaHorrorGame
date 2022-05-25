using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMap : MonoBehaviour
{

    static public bool showMap = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ShowMap")) {

            if (showMap == true)
            {
                Debug.Log("press map show");

                showMap = false;
            }
            else {
                Debug.Log("press map hide");

                showMap = true;
            }



        }
    }
}

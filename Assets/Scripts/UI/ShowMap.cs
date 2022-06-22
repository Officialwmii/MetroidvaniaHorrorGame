using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMap : MonoBehaviour
{

    static public bool showMap = true;
    public GameObject BossArenaBounds;
    void Start()
    {
        BossArenaBounds = GameObject.Find("BossArenaBounds");
    }
    void Update()
    {
        if (BossArenaBounds.GetComponent<BossArena>().PlayerInArena == true)
        {
            showMap = false;
        }

        if (BossArenaBounds.GetComponent<BossArena>().PlayerInArena == false)
        {
            if (Input.GetButtonDown("ShowMap"))
            {

                if (showMap == true)
                {
                    //Debug.Log("press map show");

                    showMap = false;
                }
                else
                {
                    //Debug.Log("press map hide");

                    showMap = true;
                }
            }

        }
    }
}

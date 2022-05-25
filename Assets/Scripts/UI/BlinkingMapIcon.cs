using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingMapIcon : MonoBehaviour
{

    private float blinkingTimer = 0;
    private GameObject PlayerPosition;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPosition= GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        blinkingTimer = blinkingTimer +1 * Time.deltaTime;
        if (blinkingTimer > 0.5f && blinkingTimer < 1f && ShowMap.showMap) {

            this.GetComponent<SpriteRenderer>().enabled = true;
        }
         else  {
            this.GetComponent<SpriteRenderer>().enabled = false;
            
        }


        if (blinkingTimer > 1) { blinkingTimer = 0f; }

        this.transform.position = PlayerPosition.transform.position;

    }
}

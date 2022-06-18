using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasDamage : MonoBehaviour
{

    private bool PlayerIsOverlapping = false;
    private float OverlappingTimer=0;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerIsOverlapping) {

            OverlappingTimer = OverlappingTimer + Time.deltaTime;

            if (OverlappingTimer >= 0.35f) {

                player.GetComponent<CharacterController2D>().ApplyDamage(-1);
                OverlappingTimer = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && EventManager.HasArmour==false)
        {
            player = col.gameObject;
            PlayerIsOverlapping = true;
            OverlappingTimer = 0;
        }
        else if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "tilemap" || col.gameObject.tag == "Jumpthrough")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }

    void OnTriggerExit2D(Collider2D col) {

        if (col.CompareTag("Player"))
        {
            PlayerIsOverlapping = false;
        }



    }


}

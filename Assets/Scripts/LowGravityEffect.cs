using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowGravityEffect : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip SFXEnter;

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
            Debug.Log("Enter Low gravity");
            col.GetComponent<PlayerMovement>().variableGravity= 0.25f;
            AudioSource.PlayClipAtPoint(SFXEnter, col.gameObject.transform.position);
            EventManager.inLowGravityZone = true;

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Exits Low gravity");
            col.GetComponent<PlayerMovement>().variableGravity = 2f;
            //AudioSource.PlayClipAtPoint(SFXExits, col.gameObject.transform.position);
            EventManager.inLowGravityZone = false;

        }
    }

}

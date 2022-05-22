using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeMovement : MonoBehaviour
{
    private float length, objstartpos, startpos;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        objstartpos = transform.position.x;
        startpos = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        startpos += 1;
        float dist = -(startpos * movementSpeed);

        transform.position = new Vector3(objstartpos + dist, transform.position.y, transform.position.z);
        // Debug.Log("Startpos: " + startpos + " transform pos: " + transform.position.x);
        if (transform.position.x < -13.59) startpos = 0;
    }
}

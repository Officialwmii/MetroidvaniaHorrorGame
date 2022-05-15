using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeArea : MonoBehaviour
{

    private float timer=0.5f;

    // Start is called before the first frame update
    void Awake()
    {

    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);

        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.gameObject.SendMessage("Frozen");


        }

    }
}
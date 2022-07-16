using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionVisible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {

            Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a = 0.25f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;

        }
    }


}

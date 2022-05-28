using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
        
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

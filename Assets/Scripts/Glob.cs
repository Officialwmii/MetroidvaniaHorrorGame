using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glob : MonoBehaviour
{

    public float damageAmount = 1;
    public float life = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && life > 0)
        {
            collision.gameObject.GetComponent<CharacterController2D>().ApplyDamage(-damageAmount);

        }
    }
}

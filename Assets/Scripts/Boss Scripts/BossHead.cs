using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHead : MonoBehaviour
{
    // Start is called before the first frame update
    public float damageAmount = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            
            collision.gameObject.SendMessage("ApplyDamage", -damageAmount);
            //Debug.Log("I hurt");

        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "tilemap" || collision.gameObject.tag == "Jumpthrough")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }


    }
}

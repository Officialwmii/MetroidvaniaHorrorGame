using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGasDamage : MonoBehaviour
{
    // Start is called before the first frame update
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
            col.gameObject.GetComponent<CharacterController2D>().ApplyDamage(-1);
        }
        else if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "tilemap" || col.gameObject.tag == "Jumpthrough")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }


}

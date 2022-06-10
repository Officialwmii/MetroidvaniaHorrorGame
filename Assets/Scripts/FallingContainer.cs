using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingContainer : MonoBehaviour
{

    private GameObject BrokenCreate;

    // Start is called before the first frame update
    void Start()
    {
        BrokenCreate = GameObject.Find("BrokenCreate");
        BrokenCreate.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        
        }

        if (collision.gameObject.tag == "Crate")
        { Destroy(gameObject);
            BrokenCreate.SetActive(true);
            BrokenCreate.GetComponent<AudioSource>().Play();
        }
    }

}

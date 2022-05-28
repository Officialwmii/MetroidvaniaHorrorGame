using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BossStuff;
    void Start()
    {
        BossStuff.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {

            BossStuff.SetActive(true);
            Debug.Log("I kill.");

        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATotallyNormalScript : MonoBehaviour
{

    public GameObject toActivate;
    public float delayTimer;
    public AudioSource jumpScare;

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
            Debug.Log("Enter");

            StartCoroutine(SpawnEnemy());
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Exit");

            this.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(DeSpawnEnemy());

        }
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(delayTimer);

        jumpScare.Play();
        toActivate.SetActive(true);
        //Destroy(gameObject);

    }

    IEnumerator DeSpawnEnemy()
    {
        yield return new WaitForSeconds(delayTimer);

        toActivate.SetActive(false);
        //Destroy(gameObject);

    }


}

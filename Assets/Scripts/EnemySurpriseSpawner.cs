using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySurpriseSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject enemy;
    public GameObject SpawnPosition;
    public float delayTimer;
    public AudioClip JumpScare;
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

            this.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(SpawnEnemy());

        }
    }
    IEnumerator SpawnEnemy() {
        yield return new WaitForSeconds(delayTimer);

        GameObject EnemySpawned = Instantiate(enemy, SpawnPosition.transform.position, Quaternion.identity);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(JumpScare, gameObject.transform.position, 0.1f);
    }
}

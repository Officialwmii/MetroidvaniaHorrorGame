using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spawner_1;
    public GameObject spawner_2;
    public GameObject spawner_3;
    public GameObject spawner_4;
    public GameObject spawner_5;
    public GameObject liptank;
    public GameObject infected;
    public GameObject spawnedEnemyParent;
    public bool enemiesPresent = false;
    [Header("Timer")]
    public float phase_timer = 20f;
    public float timer;
    public float trigger_time = 5f;


    // Update is called once per frame
    private void Start()
    {
        timer = phase_timer;
        spawnMonsters();
        enemiesPresent = true;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= trigger_time)
        {
            checkEnemies(spawnedEnemyParent.transform);
            if (enemiesPresent == false)
            {
                spawnMonsters();
                enemiesPresent = true;
            }
        }
    }
    void OnDisable()
    {

    }

    public void spawnMonsters()
    {
        Instantiate(infected, spawner_1.transform.position,spawner_1.transform.rotation, spawnedEnemyParent.transform);
        Instantiate(infected, spawner_2.transform.position, spawner_2.transform.rotation, spawnedEnemyParent.transform);
        Instantiate(infected, spawner_3.transform.position, spawner_3.transform.rotation, spawnedEnemyParent.transform);
        Instantiate(liptank, spawner_4.transform.position, spawner_4.transform.rotation, spawnedEnemyParent.transform);
        Instantiate(liptank, spawner_5.transform.position, spawner_5.transform.rotation, spawnedEnemyParent.transform);
    }



    void checkEnemies(Transform spawnParent)
    {
        if (spawnParent.childCount == 0)
        {
            enemiesPresent = false;
        }
    }
}

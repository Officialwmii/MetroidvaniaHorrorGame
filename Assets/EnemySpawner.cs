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

    void Awake()
    {
        spawnMonsters();

    }

    // Update is called once per frame

    void OnDisable()
    {
        killMonsters(spawnedEnemyParent.transform);
    }

    void spawnMonsters()
    {
        Instantiate(infected, spawner_1.transform.position,spawner_1.transform.rotation, spawnedEnemyParent.transform);
        Instantiate(infected, spawner_2.transform.position, spawner_2.transform.rotation, spawnedEnemyParent.transform);
        Instantiate(infected, spawner_3.transform.position, spawner_3.transform.rotation, spawnedEnemyParent.transform);
        Instantiate(liptank, spawner_4.transform.position, spawner_4.transform.rotation, spawnedEnemyParent.transform);
        Instantiate(infected, spawner_5.transform.position, spawner_5.transform.rotation, spawnedEnemyParent.transform);
    }

    void killMonsters(Transform spawnParent)
    {
        foreach(GameObject child in spawnParent)
        {
            Destroy(child);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2 : MonoBehaviour
{
    [Header("Head Objects")]
    public GameObject Head_Left;
    public GameObject Head_Right;

    [Header("Tell Objects")]
    public GameObject Gas_Tell;

    [Header("Gas Objects")]
    public GameObject Gas_Object;

    [Header("Floats")]
    static public float phase2_timer = 30f;
    private float timer = phase2_timer;

    [Header("Enemy Prefab")]
    public GameObject enemy;
    public GameObject spawnLocations;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        int childCount = Gas_Object.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Gas_Object.transform.GetChild(i).gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;
        int maxEnemyCount = 3;
        int currEnemyCount = 0;
        int current_Random = Random.Range(0, Gas_Object.transform.childCount);

        if (timer <= 17)
        {
            current_Random = Random.Range(0, Gas_Object.transform.childCount);
            Debug.Log(current_Random);
            Gas_Object.transform.GetChild(current_Random).gameObject.SetActive(true);


        }
        else if (timer <= 16)
        {


        }
        else if (timer <= 15)
        {
            current_Random = Random.Range(0, Gas_Object.transform.childCount);
            Debug.Log(current_Random);
            Gas_Object.transform.GetChild(current_Random).gameObject.SetActive(true);

        }
        else if (timer <= 14)
        {


        }
        else if (timer <= 13)
        {
            current_Random = Random.Range(0, Gas_Object.transform.childCount);
            Debug.Log(current_Random);
            Gas_Object.transform.GetChild(current_Random).gameObject.SetActive(true);

        }
        else if (timer <= 12)
        {


        }
        else if (timer <= 11)
        {
            current_Random = Random.Range(0, Gas_Object.transform.childCount);
            Debug.Log(current_Random);
            Gas_Object.transform.GetChild(current_Random).gameObject.SetActive(true);

        }
        else if (timer <= 10)
        {

        }
        else if (timer <= 9)
        {
            current_Random = Random.Range(0, Gas_Object.transform.childCount);
            Debug.Log(current_Random);
            Gas_Object.transform.GetChild(current_Random).gameObject.SetActive(true);
        }
        else if (timer <= 8 && timer >= 7.8)
        {
            if (currEnemyCount < maxEnemyCount)
            {
                currEnemyCount++;
                Debug.Log(currEnemyCount);
               //SpawnEnemy(enemy);
                
            }
        }
        else if (timer <= 7)
        {
            current_Random = Random.Range(0, Gas_Object.transform.childCount);
            Debug.Log(current_Random);
            Gas_Object.transform.GetChild(current_Random).gameObject.SetActive(true);
        }
        else if (timer <= 6)
        {

        }
        else if (timer <= 5)
        {
            current_Random = Random.Range(0, Gas_Object.transform.childCount);
            Debug.Log(current_Random);
            Gas_Object.transform.GetChild(current_Random).gameObject.SetActive(true);
        }
        else if (timer <= 0)
        {
            timer = phase2_timer;
            for (int i = 0; i < Gas_Object.transform.childCount; i++)
            {
                Gas_Object.transform.GetChild(i).gameObject.SetActive(false);
            }
        }


    }

    void SpawnEnemy(GameObject enemy)
    {
        int i = Random.Range(0, spawnLocations.transform.childCount);
        float x = spawnLocations.transform.GetChild(i).gameObject.transform.position.x;
        float y = spawnLocations.transform.GetChild(i).gameObject.transform.position.y;

        Instantiate(enemy, new Vector3(x, y, 0), Quaternion.identity);
        Debug.Log("rawr");

    }
}

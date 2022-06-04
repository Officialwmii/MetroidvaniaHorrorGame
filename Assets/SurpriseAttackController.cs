using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseAttackController : MonoBehaviour
{
    public GameObject player;
    public GameObject surpriseAttackPrefab;
    public GameObject surpriseParent;
    public float phase_timer = 20f;
    public float timer;
    public float trigger_time = 5f;

    private GameObject bossAttack;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        surpriseParent = GameObject.Find("surpriseParent");
    }
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
 

        if (EventManager.CurrentDangerLevel > 1 && surpriseParent.transform.childCount <= 0)
        {
            spawnAttack();
            
        }





       
    }

    void spawnAttack()
    {
        bossAttack = (GameObject)Instantiate(surpriseAttackPrefab, player.transform.position, player.transform.rotation, surpriseParent.transform);
        bossAttack.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Destroy(bossAttack, 5);
    }
}

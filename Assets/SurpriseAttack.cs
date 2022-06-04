using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SurpriseAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector director;
    public GameObject player;
    public float phase_timer = 20f;
    public float timer;
    public float trigger_time = 5f;
    void Awake()
    {
        Debug.Log(director);
        timer = phase_timer;
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (EventManager.CurrentDangerLevel > 1)
        {
            StartAttack();
        }
    }

    void StartAttack()
    {
        timer -= Time.deltaTime;

        if (timer <= trigger_time)
        {
            director.Play();
            timer = phase_timer;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossPhaseTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector director;
    public float phase_timer = 20f;
    public float timer;
    public float trigger_time = 5f;
    void Start()
    {
        Debug.Log(director);
        timer = phase_timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= trigger_time)
        {
            director.Play();
            timer = phase_timer;
        }
    }
}

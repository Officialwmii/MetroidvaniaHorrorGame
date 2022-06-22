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
    void Awake()
    {
        //Debug.Log(director);
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

            // testing rotating, and it doesn't work that well
           // var euler = transform.eulerAngles;
           // euler.z = Random.Range(0.0f, 360.0f);
           // transform.eulerAngles = euler;

        }
    }
}

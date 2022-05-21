using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Head_1;
    public GameObject Head_2;
    public GameObject Head_3;
    public GameObject Head_4;
    private int check;
    static public float base_timer = 20f;
    public float timer = base_timer;
    private Vector2 Head_1_position;
    private Vector2 Head_2_position;
    private Vector2 Head_3_position;
    private Vector2 Head_4_position;
    public float head_speed = 30f;

    void Start()
    {
        Head_1_position = GetPosition(Head_1);
        Head_2_position = GetPosition(Head_2);
        Head_3_position = GetPosition(Head_3);
        Head_4_position = GetPosition(Head_4);
    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;

        if (timer <= 15) {

            HeadSlam(Head_1, "down");

            
            
        }
        if (timer <= 14)
        {
            HeadSlam(Head_2, "up");

            
        }
        if (timer <= 13)
        {
            HeadSlam(Head_3, "right");

            
        }
        if (timer <= 12)
        {
            HeadSlam(Head_4, "left");

            
            
        }
        if (timer <= 0)
        {
            ResetPosition(Head_1, Head_1_position);
            ResetPosition(Head_2, Head_2_position);
            ResetPosition(Head_3, Head_3_position);
            ResetPosition(Head_4, Head_4_position);
            timer = base_timer;
        }


    }

    void HeadSlam(GameObject head, string dir)
    {
        if (dir == "up")
        {
            head.transform.Translate(Vector3.up * Time.deltaTime * head_speed);

        }
        else if (dir == "down")
        {
            head.transform.Translate(Vector3.down * Time.deltaTime * head_speed);

        }
        else if (dir == "left")
        {
            head.transform.Translate(Vector3.down * Time.deltaTime * head_speed);
            Debug.Log("Going left!");
        }
        else if (dir == "right")
        {
            head.transform.Translate(Vector3.up * Time.deltaTime * head_speed);
            Debug.Log("Going right!");
        }
        
        head.transform.Translate(Vector3.forward * Time.deltaTime * 5);
        Debug.Log("I smash head.");

    }
    
    Vector2 GetPosition(GameObject head)
    {
        Vector2 position = head.transform.position;
        return position;
    }
    void ResetPosition(GameObject head, Vector2 pos)
    {
        head.transform.position = pos;
    }


}


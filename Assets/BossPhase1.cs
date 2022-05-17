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
    static public float base_timer = 10f;
    public float timer = base_timer;
    private Vector2 Head_1_position;
    private Vector2 Head_2_position;
    private Vector2 Head_3_position;
    private Vector2 Head_4_position;

    void Start()
    {
        Head_1_position = GetPosition(Head_1);
        Head_2_position = GetPosition(Head_2);
        Head_3_position = GetPosition(Head_3);
        Head_4_position = GetPosition(Head_4);
        Debug.Log(Head_1_position);
    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;

        if (timer <= 5) {

            HeadSlam(Head_1, 0, -30);
            ResetPosition(Head_1, Head_1_position);
            
            
        }
        if (timer <= 4)
        {
            HeadSlam(Head_2, 0, 30);
            ResetPosition(Head_2, Head_2_position);
            
        }
        if (timer <= 3)
        {
            HeadSlam(Head_3, -30, 0);
            ResetPosition(Head_3, Head_3_position);
            
        }
        if (timer <= 2)
        {
            HeadSlam(Head_4, 30, 0);
            ResetPosition(Head_4, Head_4_position);
            
            timer = base_timer;
        }


    }

    void HeadSlam(GameObject head, float x_speed, float y_speed)
    {
        head.GetComponent<Rigidbody2D>().velocity = new Vector2(x_speed, y_speed);
        //Debug.Log("I smash head.");

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


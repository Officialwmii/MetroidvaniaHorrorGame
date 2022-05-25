using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Phase_1;
    public GameObject Phase_2;
    public GameObject End_Phase;
    public GameObject The_Bossman;
    public bool DEBUG_End_Phase_1 = false;
    public bool DEBUG_End_Phase_2 = false;
    
    void Start()
    {
        Phase_2.SetActive(false);
        End_Phase.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Phase_1.SetActive(true);

        if (The_Bossman.GetComponent<Bossman>().life <= 10 || DEBUG_End_Phase_1)
        {
            Phase_1.SetActive(false);
            Phase_2.SetActive(true);
        }

        if (The_Bossman.GetComponent<Bossman>().life <= 0 || DEBUG_End_Phase_2)
        {
            Phase_2.SetActive(false);
            End_Phase.SetActive(true);
        }

    }
}
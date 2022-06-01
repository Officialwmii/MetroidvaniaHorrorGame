using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossFight : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Phase_1;
    public GameObject Phase_2;
    public GameObject Phase_3;
    public GameObject Phase_4;
    public GameObject Phase_5;
    public GameObject Phase_6;
    public GameObject End_Phase;
    public GameObject The_Bossman;


    public GameObject BossmanHP;

    void Start()
    {
        Phase_1.SetActive(false);
        Phase_2.SetActive(false);
        Phase_3.SetActive(false);
        Phase_4.SetActive(false);
        Phase_5.SetActive(false);
        Phase_6.SetActive(false);
        End_Phase.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBossHealthMeter();

        if (The_Bossman.GetComponent<Bossman>().life <= 180)
        {
            
            Phase_1.SetActive(true);
        }
        
        
        if (The_Bossman.GetComponent<Bossman>().life <= 160)
        {
            Phase_1.SetActive(false);
            Phase_2.SetActive(true);
    
        }

        if (The_Bossman.GetComponent<Bossman>().life <= 100)
        {
            Phase_2.SetActive(false);
            Phase_3.SetActive(true);
            Phase_4.SetActive(true);

        }

        if (The_Bossman.GetComponent<Bossman>().life <= 60)
        {
            Phase_5.SetActive(true);
            Phase_6.SetActive(true);
            Phase_3.SetActive(false);
            Phase_4.SetActive(false);
        }

        if (The_Bossman.GetComponent<Bossman>().life <= 0)
        {
            Phase_1.SetActive(false);
            Phase_2.SetActive(false);
            Phase_3.SetActive(false);
            Phase_4.SetActive(false);
            Phase_5.SetActive(false);
            Phase_6.SetActive(false);
            End_Phase.SetActive(true);
        }

    }

    void UpdateBossHealthMeter()
    {
        BossmanHP.GetComponent<UnityEngine.UI.Slider>().value = The_Bossman.GetComponent<Bossman>().life / The_Bossman.GetComponent<Bossman>().maxLife;
    }
}

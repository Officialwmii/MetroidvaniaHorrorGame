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

    [Header("Debug")]
    public bool SET_BOSS_HP_400;
    public bool SET_BOSS_HP_380;
    public bool SET_BOSS_HP_300;
    public bool SET_BOSS_HP_200;
    public bool SET_BOSS_HP_100;
    public bool SET_BOSS_HP_0;


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
        DebugPhases();
        if (The_Bossman.GetComponent<Bossman>().life == 400)
        {
            Phase_1.SetActive(false);
            Phase_2.SetActive(false);
            Phase_3.SetActive(false);
            Phase_4.SetActive(false);
            Phase_5.SetActive(false);
            Phase_6.SetActive(false);
            End_Phase.SetActive(false);
        }
        
        if (The_Bossman.GetComponent<Bossman>().life <= 380)
        {
            
            Phase_1.SetActive(true);
        }
        
        
        if (The_Bossman.GetComponent<Bossman>().life <= 300)
        {
            Phase_1.SetActive(false);
            Phase_2.SetActive(true);
    
        }

        if (The_Bossman.GetComponent<Bossman>().life <= 200)
        {
            Phase_2.SetActive(false);
            Phase_3.SetActive(true);
            Phase_4.SetActive(true);

        }

        if (The_Bossman.GetComponent<Bossman>().life <= 100)
        {
            Phase_4.SetActive(false);
            Phase_5.SetActive(true);

            
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
            // Play Audio for Ship AI
            EventManager.OnBossCompleted();


        }

    }

    void UpdateBossHealthMeter()
    {
        BossmanHP.GetComponent<UnityEngine.UI.Slider>().value = The_Bossman.GetComponent<Bossman>().life / The_Bossman.GetComponent<Bossman>().maxLife;
    }

    void DebugPhases()
    {
        if (SET_BOSS_HP_400)
        {
            The_Bossman.GetComponent<Bossman>().life = 400;
            SET_BOSS_HP_380 = false;
            SET_BOSS_HP_300 = false;
            SET_BOSS_HP_200 = false;
            SET_BOSS_HP_100 = false;
            SET_BOSS_HP_0 = false;
        }
        if (SET_BOSS_HP_380)
        {
            The_Bossman.GetComponent<Bossman>().life = 380;
            SET_BOSS_HP_400 = false;
            SET_BOSS_HP_300 = false;
            SET_BOSS_HP_200 = false;
            SET_BOSS_HP_100 = false;
            SET_BOSS_HP_0 = false;
        }
        if (SET_BOSS_HP_300)
        {
            The_Bossman.GetComponent<Bossman>().life = 300;
            SET_BOSS_HP_400 = false;
            SET_BOSS_HP_380 = false;
            SET_BOSS_HP_200 = false;
            SET_BOSS_HP_100 = false;
            SET_BOSS_HP_0 = false;
        }
        if (SET_BOSS_HP_200)
        {
            The_Bossman.GetComponent<Bossman>().life = 200;
            SET_BOSS_HP_400 = false;
            SET_BOSS_HP_380 = false;
            SET_BOSS_HP_300 = false;
            SET_BOSS_HP_100 = false;
            SET_BOSS_HP_0 = false;
        }
        if (SET_BOSS_HP_100)
        {
            The_Bossman.GetComponent<Bossman>().life = 100;
            SET_BOSS_HP_400 = false;
            SET_BOSS_HP_380 = false;
            SET_BOSS_HP_300 = false;
            SET_BOSS_HP_200 = false;
            SET_BOSS_HP_0 = false;
        }
        if (SET_BOSS_HP_0)
        {
            The_Bossman.GetComponent<Bossman>().life = 0;
            SET_BOSS_HP_400 = false;
            SET_BOSS_HP_380 = false;
            SET_BOSS_HP_300 = false;
            SET_BOSS_HP_200 = false;
            SET_BOSS_HP_100 = false;
        }



    }
}

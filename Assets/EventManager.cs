using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    static public int Lives = 3;
    static private GameObject life1;
    static private GameObject life2;
    static private GameObject life3;

    static public float StunCooldown = 100;
    static public float CooldownTime = 15;
    static public bool canUseStun = true;
    static private GameObject cooldown;
    static public int stunUpgrade = 1;
    public bool EnemiesAlerted = false;


    // Start is called before the first frame update
    void Start()
    {
        life1 = GameObject.Find("HP1");
        life2 = GameObject.Find("HP2");
        life3 = GameObject.Find("HP3");
        UpdateLives();

        cooldown = GameObject.Find("Cooldown");

        UpdateStunGunCooldown();

    }

    // Update is called once per frame
    void Update()
    {
        if (StunCooldown <= 100* CooldownTime)
        {
            StunCooldown = StunCooldown + Time.deltaTime*100;
            UpdateCooldown();
            //Debug.Log(StunCooldown);
        }
        else { canUseStun = true;//
            //Debug.Log("Cooldown reset");
        }
    }


    static public void UpgradeStunGun() {

        stunUpgrade = stunUpgrade + 1;
        UpdateStunGunCooldown();
    }
    static public void UpdateStunGunCooldown() {
        if (stunUpgrade == 1) { CooldownTime = 9; }
        if (stunUpgrade == 2) { CooldownTime = 8; }
        if (stunUpgrade == 3) { CooldownTime = 6; }
        if (stunUpgrade == 4) { CooldownTime = 5; }
        if (stunUpgrade >= 5) { CooldownTime = 4; }

    }


    static public void UpdateLives()
    {

        if (Lives == 3)
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(true);

        }

        if (Lives == 2)
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(false);
        }

        if (Lives == 1)
        {
            life1.SetActive(true);
            life2.SetActive(false);
            life3.SetActive(false);
        }

        if (Lives == 0)
        {
            life1.SetActive(false);
            life2.SetActive(false);
            life3.SetActive(false);
        }

    }

    static public void ReduceHP() {

        Lives = Lives - 1;
        UpdateLives();

    }

    static public void StartCooldown()
    {
        StunCooldown = 0f;
        canUseStun = false;
        UpdateCooldown();
    }
    static public void UpdateCooldown() {
        cooldown.GetComponent<UnityEngine.UI.Image>().fillAmount = StunCooldown/100/CooldownTime;
    }
    
    public void Alert() {
        EnemiesAlerted = true;
        Debug.Log("Help!");
    }

    public void Calm()
    {
        EnemiesAlerted = false;
        Debug.Log("Phew, false alarm.");
    }


}

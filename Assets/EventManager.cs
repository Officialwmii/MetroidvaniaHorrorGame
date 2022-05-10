using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    static public int Lives = 3;
    static private GameObject life1;
    static private GameObject life2;
    static private GameObject life3;

    static public int grenades = 2;
    static public int MaxGrenades = 2;

    static public bool canUseGrenades = true;
    static private GameObject grenade1;
    static private GameObject grenade2;
    static private GameObject grenade3;
    static private GameObject MaxGrenade1;
    static private GameObject MaxGrenade2;
    static private GameObject MaxGrenade3;

    static public float StunCooldown = 100;
    static public float CooldownTime = 15;
    static public bool canUseStun = true;
    static private GameObject Cooldown;
    static public int stunUpgrade = 1;
   
    public bool EnemiesAlerted = false;
    static public float CurrentDanger = 15;
    static private GameObject DangerMeter;



    static public float Fuel = 100;
    static public float MaxFuel = 100;
    static public float DashFuelCost = 15;
    static public float EMPFuelCost = 15;
    static public bool canUseDash = true;

    static private GameObject fuelBar;


    // Start is called before the first frame update
    void Start()
    {
        life1 = GameObject.Find("HP1");
        life2 = GameObject.Find("HP2");
        life3 = GameObject.Find("HP3");
        UpdateLives();

        grenade1 = GameObject.Find("grenade1");
        grenade2 = GameObject.Find("grenade2");
        grenade3 = GameObject.Find("grenade3");

        MaxGrenade1 = GameObject.Find("MaxGrenade1");
        MaxGrenade2 = GameObject.Find("MaxGrenade2");
        MaxGrenade3 = GameObject.Find("MaxGrenade3");

        UpdateGrenades();

        Cooldown = GameObject.Find("Cooldown");

        UpdateStunGunCooldown();
        UpdateCooldown();



        fuelBar = GameObject.Find("fuelBar");
        UpdateFuel();

        DangerMeter = GameObject.Find("DangerMeter");
        UpdateDangerMeter();

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
        else { canUseStun = true;
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

    static public void UpdateGrenades()
    {

        MaxGrenade1.SetActive(false);
        MaxGrenade2.SetActive(false);
        MaxGrenade3.SetActive(false);

        if (MaxGrenades >= 1) { MaxGrenade1.SetActive(true); } 
        if (MaxGrenades >= 2) { MaxGrenade2.SetActive(true); }
        if (MaxGrenades >= 3) { MaxGrenade3.SetActive(true); }


        if (grenades == 3)
        {
            grenade1.SetActive(true);
            grenade2.SetActive(true);
            grenade3.SetActive(true);

        }

        if (grenades == 2)
        {
            grenade1.SetActive(true);
            grenade2.SetActive(true);
            grenade3.SetActive(false);
        }

        if (grenades == 1)
        {
            grenade1.SetActive(true);
            grenade2.SetActive(false);
            grenade3.SetActive(false);
        }

        if (grenades == 0)
        {
            grenade1.SetActive(false);
            grenade2.SetActive(false);
            grenade3.SetActive(false);
        }

    }

    static public void UseGrenades() {

        grenades = grenades - 1;
        if (grenades <= 0)
        {
            grenades = 0;
            canUseGrenades = false;
        }

        UpdateGrenades();

    }

    static public void GrenadePickup()
    {
        grenades = grenades + 1;
        canUseGrenades = true;
        if (grenades >= MaxGrenades)
        {
            grenades = MaxGrenades;
        }

        UpdateGrenades();
    }


    static public void StartCooldown()
    {
        StunCooldown = 0f;
        canUseStun = false;
        UpdateCooldown();
    }
    static public void UpdateCooldown() {
          Cooldown.GetComponent<UnityEngine.UI.Image>().fillAmount = StunCooldown/100/CooldownTime;
       // string test;
        //test = Cooldown.name;



    }

    static public void UseDash() {

        Fuel = Fuel - DashFuelCost;
        if (Fuel <= 0) {
            Fuel = 0;
            canUseDash = false;
        }

        UpdateFuel();
    }

    static public void FuelPickup() {


        Fuel = Fuel + 50;
        canUseDash = true;
        if (Fuel >= MaxFuel)
            {
                Fuel = MaxFuel; 
        }
        
        UpdateFuel();

    }



    static public void UpdateFuel() {

        fuelBar.GetComponent<UnityEngine.UI.Slider>().value = Fuel/100;

    }


    static public void AddDanger()
    {
        CurrentDanger = CurrentDanger + 15;
        if (CurrentDanger >= 100) { CurrentDanger = 100;}

        UpdateDangerMeter();
    }

    static public void ReduceDanger()
    {
        CurrentDanger = CurrentDanger - 25;
        if (CurrentDanger <= 0) { CurrentDanger = 0;}

        UpdateDangerMeter();
    }

    static public void UpdateDangerMeter()
    {

        DangerMeter.GetComponent<UnityEngine.UI.Slider>().value = CurrentDanger / 100;

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

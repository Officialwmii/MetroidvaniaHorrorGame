using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    static public int Lives = 4;
    static public int MaxLives = 4;
    static public int LifeShards = 0;

    static private GameObject life1;
    static private GameObject life2;
    static private GameObject life3;
    static private GameObject life4;
    static private GameObject life5;
    static private GameObject life6;
    static private GameObject life7;

    static public int grenades = 1;
    static public int MaxGrenades = 2;

    static public bool canUseGrenades = true;
    static private GameObject grenade1;
    static private GameObject grenade2;
    static private GameObject grenade3;
    static private GameObject grenade4;
    static private GameObject grenade5;
    static private GameObject MaxGrenade1;
    static private GameObject MaxGrenade2;
    static private GameObject MaxGrenade3;
    static private GameObject MaxGrenade4;
    static private GameObject MaxGrenade5;

    static public float StunCooldown = 100;
    static public float CooldownTime = 15;
    static public bool canUseStun = true;
    static private GameObject Cooldown;
    static public int stunUpgrade = 1;

    static public int Collectables = 0;
    static public int AudioLog = 0;

    static public bool EnemiesAlerted = false;
    static public float CurrentDanger = 15;
    static public float CurrentDangerLevel = 0;

    static private GameObject DangerMeter;
    static private GameObject DangerLevel1Layer;
    static private GameObject DangerLevel2Layer;
    static private GameObject DangerLevel3Layer;

    static public float Fuel = 100;
    static public float MaxFuel = 50;
    static public float DashFuelCost = 15;
    static public float EMPFuelCost = 15;
    static public bool canUseDash = true;
    static public float JetpackFuelCost = 0.9f;
    static public bool canUseJetpack = true;

    static private GameObject fuelBar;

    static public bool HasJetpack = false;
    static public bool HasDoubleJetpack = false;
    static public bool HasArmour = false;
    static public bool HasRocketLauncher = false;
    static public bool HasFuelRefill = true;
    static public int FuelRefillNumberOfUpgrades = 1;
    static public float FuelRefillTreshold = 0.25f;
    static public float FuelRefillSpeed = 2.5f;

    static public int MainUpgradesAcquired= 0;
    static public int ConstalationsKeysAcquired = 0;

    static private GameObject player;
    static private GameObject StartPosition;

    // Start is called before the first frame update
    void Start()
    {
        life1 = GameObject.Find("HP1");
        life2 = GameObject.Find("HP2");
        life3 = GameObject.Find("HP3");
        life4 = GameObject.Find("HP4");
        life5 = GameObject.Find("HP5");
        life6 = GameObject.Find("HP6");
        life7 = GameObject.Find("HP7");
        UpdateLives();

        grenade1 = GameObject.Find("grenade1");
        grenade2 = GameObject.Find("grenade2");
        grenade3 = GameObject.Find("grenade3");
        grenade5 = GameObject.Find("grenade4");
        grenade4 = GameObject.Find("grenade5");

        MaxGrenade1 = GameObject.Find("MaxGrenade1");
        MaxGrenade2 = GameObject.Find("MaxGrenade2");
        MaxGrenade3 = GameObject.Find("MaxGrenade3");
        MaxGrenade4 = GameObject.Find("MaxGrenade4");
        MaxGrenade5 = GameObject.Find("MaxGrenade5");

        UpdateGrenades();

        Cooldown = GameObject.Find("Cooldown");
        UpdateStunGunCooldown();
        UpdateCooldown();

        fuelBar = GameObject.Find("fuelBar");
        UpdateFuel();

        DangerMeter = GameObject.Find("DangerMeter");
        UpdateDangerMeter();

        DangerLevel1Layer = GameObject.Find("DangerLevel1");
        DangerLevel2Layer = GameObject.Find("DangerLevel2");
        DangerLevel3Layer = GameObject.Find("DangerLevel3");
        UpdateDangerLevel();

        player = GameObject.Find("Player");
        StartPosition = GameObject.Find("PlayerRespawn");

    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;

        //stun cooldown
        if (StunCooldown <= 100* CooldownTime)
        {
            StunCooldown = StunCooldown + Time.deltaTime*100;
            UpdateCooldown();
            //Debug.Log(StunCooldown);
        }
        else { canUseStun = true;
            //Debug.Log("Cooldown reset");
        }

        //Danger meter debug
        if (Input.GetKeyDown(KeyCode.PageUp)) { AddDanger();}

        if (Input.GetKeyDown(KeyCode.PageDown)){ ReduceDanger(); }

        //fuel refill upgrade
        if (HasFuelRefill && (Fuel < MaxFuel * FuelRefillTreshold || Fuel<15) && CharacterController2D.m_Grounded==true) {
            Fuel = Fuel + Time.deltaTime * FuelRefillSpeed;
            
            UpdateFuel();
        }

        AlertAddingDanger();

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
        life1.SetActive(false);
        life2.SetActive(false);
        life3.SetActive(false);
        life4.SetActive(false);
        life5.SetActive(false);
        life6.SetActive(false);
        life7.SetActive(false);

        if (Lives >= 7) { life7.SetActive(true); }
        if (Lives == 6) { life6.SetActive(true); }
        if (Lives == 5) { life5.SetActive(true); }
        if (Lives == 4) { life4.SetActive(true); }
        if (Lives == 3) { life3.SetActive(true); }
        if (Lives == 2) { life2.SetActive(true); }
        if (Lives == 1) { life1.SetActive(true); }
        if (Lives == 0) {        }
    }


    static public void SetHP(float HP){
        Lives = (int) HP;
        UpdateLives();
    }

    static public void ReduceHP() {
        Lives = Lives - 1;
        UpdateLives();
    }

    static public void HealthPickup(){

        LifeShards = LifeShards + 1;
        if (LifeShards >= 3) { LifeShards = 0; MaxLives++; }

        Lives = Lives + 1;
        if (Lives >= MaxLives) { Lives = MaxLives; }

        UpdateLives();
    }

    static public void UpdateGrenades(){
        MaxGrenade1.SetActive(false);
        MaxGrenade2.SetActive(false);
        MaxGrenade3.SetActive(false);
        MaxGrenade4.SetActive(false);
        MaxGrenade5.SetActive(false);

        if (MaxGrenades >= 1) { MaxGrenade1.SetActive(true); } 
        if (MaxGrenades >= 2) { MaxGrenade2.SetActive(true); }
        if (MaxGrenades >= 3) { MaxGrenade3.SetActive(true); }
        if (MaxGrenades >= 4) { MaxGrenade4.SetActive(true); }
        if (MaxGrenades >= 5) { MaxGrenade5.SetActive(true); }

        grenade1.SetActive(false);
        grenade2.SetActive(false);
        grenade3.SetActive(false);
        grenade4.SetActive(false);
        grenade5.SetActive(false);

        if (grenades >= 1) { grenade1.SetActive(true); }
        if (grenades >= 2) { grenade2.SetActive(true); }
        if (grenades >= 3) { grenade3.SetActive(true); }
        if (grenades >= 4) { grenade4.SetActive(true); }
        if (grenades >= 5) { grenade5.SetActive(true); }

    }

    static public void UseGrenades() {

        grenades = grenades - 1;
        if (grenades <= 0)
        {
            grenades = 0;
            canUseGrenades = false;
        }

        UpdateGrenades();
        RefillFuel();
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
    static public void UpgradeMaxGrenade() {
        MaxGrenades++;
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
    }

    static public void UseDash() {

        Fuel = Fuel - DashFuelCost;
        if (Fuel <= DashFuelCost) {
            Fuel = 0;
            canUseDash = false;
        }

        UpdateFuel();
    }
    
    static public void UseJetpack()
    {
        Fuel = Fuel - JetpackFuelCost;
        if (Fuel <= JetpackFuelCost)
        {
            Fuel = 0;
            canUseJetpack = false;
        }
        UpdateFuel();
    }

    static public void FuelPickup() {

        Fuel = Fuel + 50;
        MaxFuel = MaxFuel + 10;

        canUseDash = true;
        canUseJetpack = true;
        if (Fuel >= MaxFuel)
            {
                Fuel = MaxFuel; 
        }
        
        UpdateFuel();
    }

    static public void RefillFuel(){
        Fuel = MaxFuel;
        UpdateFuel();
    }

    static public void UpdateFuel() {

        if (Fuel >= DashFuelCost) { canUseDash = true; }
        if (Fuel >= DashFuelCost) { canUseJetpack = true; }

        fuelBar.GetComponent<UnityEngine.UI.Slider>().value = Fuel/ MaxFuel; }
    static public void CollectablePickup(){ Collectables++; }
    static public void AudioLogPickup()
    {
        if(AudioLog == 0)
        {
            AudioLog++;
            AkSoundEngine.PostEvent("Audio_Log_1", player);
        }
        else if(AudioLog == 1)
        {
            AudioLog++;
            AkSoundEngine.PostEvent("Audio_Log_2", player);
        }
        else if (AudioLog == 2)
        {
            AudioLog++;
            AkSoundEngine.PostEvent("Audio_Log_3", player);
        }
        else if (AudioLog == 3)
        {
            AudioLog++;
            AkSoundEngine.PostEvent("Audio_Log_4", player);
        }
        else if (AudioLog == 4)
        {
            AudioLog++;
            AkSoundEngine.PostEvent("Audio_Log_5", player);
        }
        else
        {
            AudioLog++;
            AkSoundEngine.PostEvent("Audio_Log_6", player);
        }
    }

    static public void GainAbilityJetpack() {

        if (HasJetpack == false) { HasJetpack = true; }
        else { HasDoubleJetpack = true; }
         MainUpgradesAcquired++; }

    static public void GainAbilityArmour() { HasArmour = true; MainUpgradesAcquired++; }
    static public void GainAbilityRocketLauncher() { HasRocketLauncher = true; MainUpgradesAcquired++; }
    static public void GainAbilityFuelRefill() { 
        HasFuelRefill = true;
        FuelRefillNumberOfUpgrades++;
        if (FuelRefillNumberOfUpgrades == 1) { FuelRefillTreshold = 0.25f; FuelRefillSpeed = 2.5f; }
        if (FuelRefillNumberOfUpgrades == 2) { FuelRefillTreshold = 0.33f; FuelRefillSpeed = 3.3f; }
        if (FuelRefillNumberOfUpgrades == 3) { FuelRefillTreshold = 0.5f; FuelRefillSpeed = 5; }
        if (FuelRefillNumberOfUpgrades == 4) { FuelRefillTreshold = 0.75f; FuelRefillSpeed = 7.5f; }
        if (FuelRefillNumberOfUpgrades == 5) { FuelRefillTreshold = 1f; FuelRefillSpeed = 10; }
        if (FuelRefillNumberOfUpgrades >= 6) { FuelRefillTreshold = 1f; FuelRefillSpeed = 25; }

    }
    static public void GainConstalationKey() { ConstalationsKeysAcquired++; }
    static public void GoToCredits() { SceneManager.LoadScene("Credits"); }

    static public void OnRespawning() {

        if (grenades <= 0) { grenades = 1; UpdateGrenades(); } 
        Fuel = MaxFuel; UpdateFuel();
        player.GetComponent<CharacterController2D>().ResetHealth();
        player.transform.position = StartPosition.transform.position;
        player.GetComponent<Animator>().SetBool("IsDead", false);

    }


    //Danger

    static public void AddDanger() {

        CurrentDanger = CurrentDanger + 5;
        if (CurrentDanger >= 100) { CurrentDanger = 100;}

        UpdateDangerMeter();
        UpdateDangerLevel();
    }


    static public void ReduceDanger()
    {
        CurrentDanger = CurrentDanger - 25;
        if (CurrentDanger <= 0) { CurrentDanger = 0;}

        UpdateDangerMeter();
        UpdateDangerLevel();
    }

    static public void UpdateDangerMeter()
    {
        DangerMeter.GetComponent<UnityEngine.UI.Slider>().value = CurrentDanger / 100;
    }

    static public void UpdateDangerLevel()
    {
        CurrentDangerLevel = 0;
       
        if (CurrentDanger >= 50) CurrentDangerLevel = 1;
        if (CurrentDanger >= 75) CurrentDangerLevel = 2;
        if (CurrentDanger >= 85) CurrentDangerLevel = 3;
       //if (CurrentDanger >= 100) SceneManager.LoadScene("Scenes/" + SceneManager.GetActiveScene().name);

        DangerLevel1Layer.SetActive(false);
        DangerLevel2Layer.SetActive(false);
        DangerLevel3Layer.SetActive(false);

        if (CurrentDangerLevel >= 1) DangerLevel1Layer.SetActive(true);
        if (CurrentDangerLevel >= 2) DangerLevel2Layer.SetActive(true);
        if (CurrentDangerLevel >= 3) DangerLevel3Layer.SetActive(true);

    }

    static public void Alert() {
        EnemiesAlerted = true;
        Debug.Log("Help!");
    }

    static public void Calm()
    {
        EnemiesAlerted = false;
       // Debug.Log("Phew, false alarm.");
    }

    static public void AlertAddingDanger() {

        if (EnemiesAlerted) {

            CurrentDanger = CurrentDanger + 0.25f * Time.deltaTime;

            UpdateDangerMeter();
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class EventManager : MonoBehaviour
{
    static public int Lives = 4;
    static public int MaxLives = 4;
    static public int LifeShards = 0;
    static public int DeathCounter = 0;

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
    static public float DangerMultiplier = 1;

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
    static private GameObject fuelBarArea;
    static private GameObject fuelBarFrame;

    static public bool HasJetpack = false;
    static public bool HasDoubleJetpack = false;
    static public bool HasArmour = false;
    static public bool HasRocketLauncher = false;
    static public bool HasFuelRefill = true;
    static public int FuelRefillNumberOfUpgrades = 1;
    static public float FuelRefillTreshold = 0.25f;
    static public float FuelRefillSpeed = 2.5f;

    static public int MainUpgradesAcquired = 0;
    static public int ConstalationsKeysAcquired = 0;

    static private GameObject player;
    static private GameObject StartPosition;
    static private GameObject AlertWarning;
    public static bool EscapeSequence = false;
    static private GameObject Elevator;
    static private GameObject StartPositionBoss;
    static private GameObject EscapePodDoor;
    static private GameObject AlertTimerFont;
    static private GameObject boss;

    private static float AlertTimer = 180;
    private static float alienTimer = 0;

    public static int MapProgress = 0;
    public static float Timer = 0;

    static private GameObject ThisGameObject;

    static private bool TriggerCooldownSoundOnce = false;

    static private GameObject AlertSound;

    static private bool AudioPlayEscapePodInitiated = false;
    static public bool CheatMapVisibility = false;

    public static bool inLowGravityZone = false; 


    public static GameObject KeyUnlocked1;
    public static GameObject KeyUnlocked2;
    public static GameObject KeyUnlocked3;
    public static GameObject GrenadeCircle; 
    public static float GrenadeCountdown = 0;
    public static bool CanThrowGrenade = true;

    static private float randomAlienMonolouge = 0;



    void OnEnable() {
        //Resetting all variables when playing the game again.
        Lives = 4;
        MaxLives = 4;
        LifeShards = 0;
        DeathCounter = 0;
        grenades = 1;
        MaxGrenades = 2;
        CooldownTime = 15;
        canUseStun = true;
        stunUpgrade = 1;
        Collectables = 0;
        AudioLog = 0;
        EnemiesAlerted = false;
        CurrentDanger = 15;
        CurrentDangerLevel = 0;
        DangerMultiplier = 1;
        Fuel = 100;
        MaxFuel = 50;
        DashFuelCost = 15;
        EMPFuelCost = 15;
        canUseDash = true;
        JetpackFuelCost = 0.9f;
        canUseJetpack = true;

        HasJetpack = false;
        HasDoubleJetpack = false;
        HasArmour = false;
        HasRocketLauncher = false;
        HasFuelRefill = true;
        FuelRefillNumberOfUpgrades = 1;
        FuelRefillTreshold = 0.25f;
        FuelRefillSpeed = 2.5f;
        MainUpgradesAcquired = 0;
        ConstalationsKeysAcquired = 0;
        EscapeSequence = false;

        AlertTimer = 180;
        alienTimer = 0;
        MapProgress = 0;
        Timer = 0;
        AudioPlayEscapePodInitiated = false;
        CheatMapVisibility = false;
        inLowGravityZone = false;
        GrenadeCountdown = 0;
        CanThrowGrenade = true;

        UpdateMaxFuleGUI();


    }

    void Start(){
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
        grenade4 = GameObject.Find("grenade4");
        grenade5 = GameObject.Find("grenade5");

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
        fuelBarArea = GameObject.Find("Fuel Fill Area");
        fuelBarFrame = GameObject.Find("FuelFrame");

        UpdateFuel();

        DangerMeter = GameObject.Find("DangerMeter");
        UpdateDangerMeter();

        DangerLevel1Layer = GameObject.Find("DangerLevel1");
        DangerLevel2Layer = GameObject.Find("DangerLevel2");
        DangerLevel3Layer = GameObject.Find("DangerLevel3");
        UpdateDangerLevel();

        player = GameObject.Find("Player");
        StartPosition = GameObject.Find("PlayerRespawn");

        AlertWarning = GameObject.Find("Alert");
        AlertWarning.SetActive(false);
        Elevator = GameObject.Find("ElevatorGroup");
        StartPositionBoss = GameObject.Find("PlayerRespawn2");
        EscapePodDoor = GameObject.Find("EscapePodDoor");
        AlertTimerFont = GameObject.Find("AlertTimerFont");
        AlertTimerFont.SetActive(false);

        boss = GameObject.Find("Boss");
        ThisGameObject = GameObject.Find("EventManager");
        AlertSound = GameObject.Find("AlertSound");
        AlertSound.SetActive(false);

        KeyUnlocked1 = GameObject.Find("Keys_0");
        KeyUnlocked1.SetActive(false);
        KeyUnlocked2 = GameObject.Find("Keys_1");
        KeyUnlocked2.SetActive(false);
        KeyUnlocked3 = GameObject.Find("Keys_2");
        KeyUnlocked3.SetActive(false);

        GrenadeCircle = GameObject.Find("GrenadeCircle");
        GrenadeCircle.SetActive(false);

        UpdateMaxFuleGUI();


    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;

        //stun cooldown
        if (StunCooldown <= 100 * CooldownTime)
        {
            StunCooldown = StunCooldown + Time.deltaTime * 100;

            UpdateCooldown();
            //Debug.Log(StunCooldown);
        }
        else { canUseStun = true;
            //Debug.Log("Cooldown reset");
        }

        //Danger meter debug
        //if (Input.GetKeyDown(KeyCode.PageUp)) { AddDangerEveryTick();}


        //fuel refill upgrade
        if (HasFuelRefill && (Fuel < MaxFuel * FuelRefillTreshold || Fuel < 15) && CharacterController2D.m_Grounded == true)
        {
            Fuel = Fuel + Time.deltaTime * FuelRefillSpeed;
            UpdateFuel();

            if (TriggerCooldownSoundOnce == false)
            {
                //disabeling temp recharge sound
                //AkSoundEngine.PostEvent("Jetpack_Upgrade", player);
                TriggerCooldownSoundOnce = true;
            }
        }
        else { TriggerCooldownSoundOnce = false; }



        //AlertAddingDanger();
        AutomaticallyReduceDanger();

        //if (Input.GetKeyDown(KeyCode.PageUp)) { OnBossCompleted(); }
        if (EscapeSequence)
        {
            AlertTimer = AlertTimer - Time.deltaTime;
            AlertTimerFont.GetComponent<TMP_Text>().text =
                Mathf.FloorToInt(AlertTimer / 60).ToString("D2") + ":" +
                Mathf.FloorToInt(AlertTimer % 60).ToString("D2") + ":" +
                (Mathf.FloorToInt((AlertTimer % 1) * 100)).ToString("D2");
            if (AlertTimer <= 0) { SceneManager.LoadScene("Credits"); ; EscapeSequence = false; }

            if (AlertTimer <= (180 - 3) && AudioPlayEscapePodInitiated == false)
            {
                AkSoundEngine.PostEvent("SHIP_SYSTEM_ANNOUNCEMENT_11", boss);
                sub("WARNING INITIATING SCUTTLE PROTOCOL", 2f);
                AudioPlayEscapePodInitiated = true;
            }

            if (AlertTimer <= 180 - 6)
            {
                AlertSound.SetActive(true);
            }
        }

        //debugging Xeno Lines
        if(Input.GetKeyDown(KeyCode.PageDown)) { randomAlienMonolouge++; AlienInnerMonologue(); }

        if (CurrentDangerLevel == 0) alienTimer = alienTimer + Time.deltaTime;

        if (alienTimer >= 1) {

            alienTimer = 0;

            float randomNumber = Random.Range(0f, 300f);

            //Debug.Log(randomNumber);
            //randomNumber = 1;

            if (Mathf.RoundToInt(randomNumber) == 1) {

                AlienInnerMonologue();

            }

        }

        Timer = Timer + Time.deltaTime;


    }

    public static void AddMapSegment() { MapProgress++; }

    // public static void PlayDenialSound() {

    //   ThisGameObject.GetComponent<AudioSource>.play();

    //}

    static public void UpdateGrenadeCountdown() {
       
        if (CanThrowGrenade) {
            GrenadeCircle.SetActive(true);

            GrenadeCountdown = GrenadeCountdown + Time.deltaTime;
            GrenadeCircle.GetComponent<Transform>().localScale = new Vector3(GrenadeCountdown * 4, GrenadeCountdown * 4, 0);

            if (GrenadeCountdown >= 1.0f)
            {
                player.GetComponent<Attack>().ThrowGrenade();
                GrenadeCountdown = 0;
                GrenadeCircle.SetActive(false);
                CanThrowGrenade = false; 
            }



        }

    }



    static public void AlienInnerMonologue() {

        float RandomLine = (int)Random.Range(1f, 16f);
        RandomLine = randomAlienMonolouge;
        switch (Mathf.RoundToInt(RandomLine))
        {
            case 1:
                sub("O to drift across the endless night hopping from star to star never look down and never look up a thousand eyes forever affixed to the abyss.", 5f);
                AkSoundEngine.PostEvent("Xeno_Monologue_01", player);
                break;
            case 2: sub("Loneliness is the cancer that grows in the hearts of all brains.", 5f);
                AkSoundEngine.PostEvent("Xeno_Monologue_02", player);
                break;
            case 3: sub("Dare to dream a nightmare from which you wouldn?t want to wake up.", 5f);
                AkSoundEngine.PostEvent("Xeno_Monologue_03", player);
                break;
            case 4: sub("Feed your own tail to your own mouth again and again and again and again.", 5f);
                AkSoundEngine.PostEvent("Xeno_Monologue_04", player);
                break;
            case 5: sub("We are what we are and what we are is the cosmic recursion.", 5f);
                AkSoundEngine.PostEvent("Xeno_Monologue_05", player);
                break;
            case 6: sub("Even the smallest speck plays its part.", 5f);
                AkSoundEngine.PostEvent("Xeno_Monologue_06", player);
                break;
            case 7: sub("They trusted you.", 1f);
                AkSoundEngine.PostEvent("Xeno_Monologue_07", player);
                break;
            case 8: sub("In the eternal dark scream your screams and we will whisper you to sleep.", 5f);
                AkSoundEngine.PostEvent("Xeno_Monologue_08", player);
                break;
            case 9: sub("That which does not end cannot end that which does not end.", 5f);
                AkSoundEngine.PostEvent("Xeno_Monologue_09", player);
                break;
            case 10: sub("Open your eyes to find that your eyes were already open.", 5f);
                AkSoundEngine.PostEvent("Xeno_Monologue_10", player);
                break;
            case 11: sub("You cannot wake up you are not asleep.", 2f);
                AkSoundEngine.PostEvent("Xeno_Monologue_11", player);
                break;
            case 12:
                sub("Nightmares are not real but we are.", 2f);
                AkSoundEngine.PostEvent("Xeno_Monologue_12", player);
                break;
            case 13:
                sub("The fruit of the tree that grew in the heart of the dark garden which spreads through the bowels of the cosmos holds the seed to salvation/damnation.", 2f);
                AkSoundEngine.PostEvent("Xeno_Monologue_13", player);
                break;
            case 14: sub("O the curse of recursion!", 2f);
                AkSoundEngine.PostEvent("Xeno_Monologue_14", player);
                break;
            case 15: sub("Void your shells. Cease your lives.", 2f);
                AkSoundEngine.PostEvent("Xeno_Monologue_15", player);
                break;
        }
    }

    static public void sub(string text, float duration) {

        SubtitlesText.instance.SetSubtitle(text, duration);
    }

    static public void OnBossCompleted() {

        if (EscapeSequence == false) {
            Debug.Log("escape sequence");
            EscapeSequence = true;
            AlertWarning.SetActive(true);
            Elevator.SetActive(false);
            AlertTimerFont.SetActive(true);
            AkSoundEngine.PostEvent("Xeno_Death", boss);

            StartPosition = StartPositionBoss;

            Destroy(EscapePodDoor);

        }
    }

    static public void GetAllUpgrades()
    {
        for (int i = 0; i < 5; i++) UpgradeStunGun();
        for (int i = 0; i < 5; i++) GainAbilityFuelRefill();
        for (int i = 0; i < 3; i++) UpgradeMaxGrenade();
        for (int i = 0; i < 10; i++) HealthPickup();
        for (int i = 0; i < 10; i++) GrenadePickup();
        for (int i = 0; i < 10; i++) FuelPickup();
        for (int i = 0; i < 3; i++) GainConstalationKey();
        GainAbilityJetpack(1);
        GainAbilityJetpack(2);
        GainAbilityArmour();
        GainAbilityRocketLauncher();
    }

    static public void cheatShowAllTheMap() {

        if (!CheatMapVisibility) { CheatMapVisibility = true; }
        else { CheatMapVisibility = false; }

    }

    static public void UpgradeStunGun() {
        AkSoundEngine.PostEvent("Stun_Gun_Upgrade", player);
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
        AkSoundEngine.PostEvent("Grenade_Upgrade", player);
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
        AkSoundEngine.PostEvent("Stun_Gun_Cooldown", player);
        canUseStun = false;
        UpdateCooldown();
    }
    static public void UpdateCooldown() {
         Cooldown.GetComponent<UnityEngine.UI.Image>().fillAmount = StunCooldown/100/CooldownTime;
    }

    static public void UseDash() {

        if (inLowGravityZone) { Fuel = Fuel - DashFuelCost / 10; }
        else { Fuel = Fuel - DashFuelCost; }

        if (Fuel <= DashFuelCost) {
            Fuel = 0;
            canUseDash = false;
        }

        UpdateFuel();
    }
    
    static public void UseJetpack()
    {
        if (inLowGravityZone) { Fuel = Fuel - JetpackFuelCost*Time.deltaTime *60 / 10; }
        else { Fuel = Fuel - JetpackFuelCost * Time.deltaTime * 60; }

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



        UpdateMaxFuleGUI();
        UpdateFuel();
    }


    static public void UpdateMaxFuleGUI() {
        fuelBarArea.GetComponent<RectTransform>().sizeDelta = new Vector2(100 * MaxFuel / 100, 7);
        fuelBarFrame.GetComponent<RectTransform>().sizeDelta = new Vector2(115 * MaxFuel / 100, 12);

    }

    static public void RefillFuel(){
        Fuel = MaxFuel;
        UpdateFuel();
    }

    static public void CheckpoiontReached(GameObject CheckpointPosition) {

        StartPosition.GetComponent<Transform>().position = CheckpointPosition.GetComponent<Transform>().position;
    }


    static public void StartingRoom() {

        player.GetComponent<CharacterController2D>().ResetHealth();
        Fuel = MaxFuel;
        UpdateFuel();
        CurrentDanger = 0; UpdateDangerMeter(); UpdateDangerLevel();
        StunCooldown = 100 * CooldownTime; UpdateCooldown();
    }

    static public void UpdateFuel() {

        if (Fuel >= DashFuelCost) { canUseDash = true; }
        if (Fuel >= DashFuelCost) { canUseJetpack = true; }

        fuelBar.GetComponent<UnityEngine.UI.Slider>().value = Fuel/ MaxFuel; }
    static public void CollectablePickup()
    {
        Collectables++;
        AkSoundEngine.PostEvent("Alien_Artifact", player);

        if (Collectables == 1) DangerMultiplier = 1.25f;
        if (Collectables == 2) DangerMultiplier = 1.5f;
        if (Collectables == 3) DangerMultiplier = 1.75f;
        if (Collectables == 4) DangerMultiplier = 2f;
        if (Collectables >= 5) DangerMultiplier = 2.5f;

        AddDanger(25);
    }

    static public void GainAbilityJetpack(int variable) {

        if (HasJetpack == false) { 
            
            HasJetpack = true;
            
            if (variable == 1) {
                GameObject Text = GameObject.Find("Tutorial Text Jetpack 2");
                Text.GetComponent<TMP_Text>().text =
                    "Full Jetpack Acquired -  \n Hold L1 or W to fly.  \n Flying in gravity bounded areas  \n consumes a large amount of fuel.";
            }
            if (variable == 2){
                GameObject Text = GameObject.Find("Tutorial Text Jetpack 1");
                Text.GetComponent<TMP_Text>().text =
                    "Full Jetpack Acquired -  \n Hold L1 or W to fly.  \n Flying in gravity bounded areas  \n consumes a large amount of fuel.";
            }

        }
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
    static public void GainConstalationKey() { 
        
        ConstalationsKeysAcquired++;

        string KeyText = "";

        if (ConstalationsKeysAcquired == 1) { KeyUnlocked1.SetActive(true); 
            KeyText = "Constellation Protocol Acquired - \n 1 found, 2 remaining to \n access the Control Bridge.";  }
        if (ConstalationsKeysAcquired == 2) { KeyUnlocked2.SetActive(true);
            KeyText = "Constellation Protocol Acquired - \n 2 found, 1 final remaining to \n access the Control Bridge.";}
        if (ConstalationsKeysAcquired == 3) { KeyUnlocked3.SetActive(true);
            KeyText = "Constellation Protocol Acquired - \n All found! Access the \n Control Bridge and initiate the scuttle protocol.";}

        GameObject KeyText1 = GameObject.Find("Key Text 1"); KeyText1.GetComponent<TMP_Text>().text = KeyText;
        GameObject KeyText2 = GameObject.Find("Key Text 2"); KeyText2.GetComponent<TMP_Text>().text = KeyText;
        GameObject KeyText3 = GameObject.Find("Key Text 3"); KeyText3.GetComponent<TMP_Text>().text = KeyText;

    }
    static public void GoToCredits() { SceneManager.LoadScene("EndSequence"); }
    static public void OnRespawning() {

        if (grenades <= 0) { grenades = 1; UpdateGrenades(); } 
        Fuel = MaxFuel; UpdateFuel();
        AkSoundEngine.SetState("Player_State", "Alive");
        player.GetComponent<CharacterController2D>().ResetHealth();
        player.transform.position = StartPosition.transform.position;
        player.GetComponent<Animator>().SetBool("IsDead", false);
        
        CurrentDanger = 0; UpdateDangerMeter(); UpdateDangerLevel();
        DeathCounter++;
    }

    static public void AudioLogPickup(AudioNode subtitles){
       
        AudioLog++;

        if (AudioLog == 1){
            AkSoundEngine.PostEvent("Audio_Log_1", player);
            SubtitlesText.instance.SetAudioLogSubtitle(AudioLog);
            //SubtitlesText.instance.SetSubtitle(subtitles.subtitle, subtitles.duration);
        }
        else if (AudioLog == 2){
            AkSoundEngine.PostEvent("Audio_Log_2", player);
            SubtitlesText.instance.SetAudioLogSubtitle(AudioLog);
            //SubtitlesText.instance.SetSubtitle(subtitles.subtitle, subtitles.duration);
        }
        else if (AudioLog == 3){
            AkSoundEngine.PostEvent("Audio_Log_3", player);
            SubtitlesText.instance.SetAudioLogSubtitle(AudioLog);
            //SubtitlesText.instance.SetSubtitle(subtitles.subtitle, subtitles.duration);
        }
        else if (AudioLog == 4){
            AkSoundEngine.PostEvent("Audio_Log_4", player);
            SubtitlesText.instance.SetAudioLogSubtitle(AudioLog);
            //SubtitlesText.instance.SetSubtitle(subtitles.subtitle, subtitles.duration);
        }
        else if (AudioLog == 5){
            AkSoundEngine.PostEvent("Audio_Log_5", player);
            SubtitlesText.instance.SetAudioLogSubtitle(AudioLog);
            //SubtitlesText.instance.SetSubtitle(subtitles.subtitle, subtitles.duration);
        }
        else{
            AkSoundEngine.PostEvent("Audio_Log_6", player);
            SubtitlesText.instance.SetAudioLogSubtitle(AudioLog);
            //SubtitlesText.instance.SetSubtitle(subtitles.subtitle, subtitles.duration);
        }
    }






    //Danger

    static public void AddDangerEveryTick(float ratio) {

        CurrentDanger = CurrentDanger + (5+2.5f) / 5 * 5 * DangerMultiplier * Time.deltaTime* ratio;
        if (CurrentDanger >= 100) { CurrentDanger = 100;}

        UpdateDangerMeter();
        UpdateDangerLevel();
    }

    static public void AutomaticallyReduceDanger() {
        CurrentDanger = CurrentDanger - 2.5f/5 * (5/ DangerMultiplier) * Time.deltaTime;
        if (CurrentDanger <= 0) { CurrentDanger = 0; }

        UpdateDangerMeter();
        UpdateDangerLevel();

    }

    static public void AddDanger(float addition) {

        CurrentDanger = CurrentDanger + addition*DangerMultiplier;
        if (CurrentDanger >= 100) { CurrentDanger = 100; }
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
       
        if (CurrentDanger >= 25) CurrentDangerLevel = 1;
        if (CurrentDanger >= 50) CurrentDangerLevel = 2;
        if (CurrentDanger >= 75) CurrentDangerLevel = 3;
        if (CurrentDanger >= 99) CurrentDangerLevel = 4;
        //SceneManager.LoadScene("Scenes/" + SceneManager.GetActiveScene().name);

        DangerLevel1Layer.SetActive(false);
        DangerLevel2Layer.SetActive(false);
        DangerLevel3Layer.SetActive(false);

        if (CurrentDangerLevel >= 1) DangerLevel1Layer.SetActive(true);
        if (CurrentDangerLevel >= 2) DangerLevel2Layer.SetActive(true);
        if (CurrentDangerLevel >= 3) DangerLevel3Layer.SetActive(true);
        if (CurrentDangerLevel >= 4) player.GetComponent<CharacterController2D>().ApplyDamage(-200);
    }

    static public void Alert() {
        EnemiesAlerted = true;
        //Debug.Log("Help!");

    }

    static public void Calm()
    {
        EnemiesAlerted = false;
       // Debug.Log("Phew, false alarm.");
    }

    static public void AlertAddingDanger() {

        if (EnemiesAlerted) {

           // CurrentDanger = CurrentDanger + 0.25f * Time.deltaTime;

            UpdateDangerMeter();
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParameter : MonoBehaviour
{
    public static List<AK.Wwise.State> ListOfStates = new List<AK.Wwise.State>();

    public AK.Wwise.RTPC noiseMeter;
    public AK.Wwise.RTPC jetpackFuel;
    public AK.Wwise.RTPC healthBar;

    public AK.Wwise.State playerState;
    public AK.Wwise.State musicState;
    public AK.Wwise.State musicSegment;
    public AK.Wwise.Switch noiseMeterLevel;

    public bool timeAttack = false;
    public bool escaped = false;
    public bool alive = true;
    public bool stateCalled = true;
    private bool noiseUpdated = false;

    private CharacterController2D player;
    public GameObject fuel;
    public GameObject dangerMeter;
    private GameObject character;
    public GameObject wwiseParameterSilent;
    public GameObject wwiseParameter1;
    public GameObject wwiseParameter2;
    public GameObject wwiseParameter3;

    // Start is called before the first frame update
    void Start()
    {
        //SetGameState();
        character = GameObject.Find("Player");
        player = GameObject.Find("Player").GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        noiseMeter.SetGlobalValue(dangerMeter.GetComponent<UnityEngine.UI.Slider>().value);
        healthBar.SetGlobalValue(player.life);
        jetpackFuel.SetGlobalValue(fuel.GetComponent<UnityEngine.UI.Slider>().value);

        
        if (dangerMeter.GetComponent<UnityEngine.UI.Slider>().value < .20f && noiseUpdated == true)
        {
            noiseUpdated = false;
            UpdateNoise();
        }
        else if (dangerMeter.GetComponent<UnityEngine.UI.Slider>().value >= .20f && dangerMeter.GetComponent<UnityEngine.UI.Slider>().value < .40 && noiseUpdated == true)
        {
            noiseUpdated = false;
            UpdateNoise();
        }
        else if (dangerMeter.GetComponent<UnityEngine.UI.Slider>().value >= .40f && dangerMeter.GetComponent<UnityEngine.UI.Slider>().value < .60 && noiseUpdated == true)
        {
            noiseUpdated = false;
            UpdateNoise();
        }
        else if (dangerMeter.GetComponent<UnityEngine.UI.Slider>().value >= .60f && dangerMeter.GetComponent<UnityEngine.UI.Slider>().value <= 1 && noiseUpdated == true)
        {
            noiseUpdated = false;
            UpdateNoise();
        }

        if (alive == false)
        {
            AkSoundEngine.SetState("Player_State", "Dead");
            alive = true;
            stateCalled = false;
        }


        if (stateCalled == false)
        {
            if (EventManager.ConstalationsKeysAcquired == 0 && alive == true)
            {
                AkSoundEngine.SetState("Music_Segments", "Start");
                stateCalled = true;
            }
            else if (EventManager.ConstalationsKeysAcquired == 1 && alive == true)
            {
                AkSoundEngine.SetState("Music_Segments", "Upgrade_1");
                stateCalled = true;
            }
            else if (EventManager.ConstalationsKeysAcquired == 2 && alive == true)
            {
                AkSoundEngine.SetState("Music_Segments", "Upgrade_2");
                stateCalled = true;
            }
            else if (EventManager.ConstalationsKeysAcquired == 3 && alive == true)
            {
                AkSoundEngine.SetState("Music_Segments", "Upgrade_3");
                stateCalled = true;
            }
            else if (timeAttack && alive == true)
            {
                AkSoundEngine.SetState("Music_Segments", "Upgrade_4");
                stateCalled = true;
            }
            else if (escaped && alive == true)
            {
                timeAttack = false;
                AkSoundEngine.SetState("Music_Segments", "Escaped");
                stateCalled = true;
            }
        }
        UpdateState();
    }

    public void UpdateState()
    {
        if (EventManager.ConstalationsKeysAcquired == 1)
            stateCalled = true;
        if (EventManager.ConstalationsKeysAcquired == 2)
            stateCalled = true;
        if (EventManager.ConstalationsKeysAcquired == 3)
            stateCalled = true;
        if (timeAttack)
            stateCalled = true;
    }

    public void SetGameState()
    {
        playerState.SetValue();
        AkSoundEngine.SetState("Player_State", "Alive");
        AkSoundEngine.SetState("Music_State", "Gameplay");
        AkSoundEngine.SetState("Music_Segments", "Start");
        stateCalled = false;
    }


    public void UpdateNoise()
    {
        if (dangerMeter.GetComponent<UnityEngine.UI.Slider>().value < .20f)
        {
            noiseMeterLevel.SetValue(wwiseParameterSilent);
        }
        else if (dangerMeter.GetComponent<UnityEngine.UI.Slider>().value >= .20f && dangerMeter.GetComponent<UnityEngine.UI.Slider>().value < .40)
        {
            noiseMeterLevel.SetValue(wwiseParameter1);
        }
        else if (dangerMeter.GetComponent<UnityEngine.UI.Slider>().value >= .40f && dangerMeter.GetComponent<UnityEngine.UI.Slider>().value < .60)
        {
            noiseMeterLevel.SetValue(wwiseParameter2);
        }
        else if (dangerMeter.GetComponent<UnityEngine.UI.Slider>().value >= .60f && dangerMeter.GetComponent<UnityEngine.UI.Slider>().value <= 1)
        {
            noiseMeterLevel.SetValue(wwiseParameter3);
        }
        else
        {
            noiseMeterLevel.SetValue(wwiseParameterSilent);
        }
        noiseUpdated = true;
    }
}
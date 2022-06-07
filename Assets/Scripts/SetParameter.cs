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

    public bool timeAttack = false;
    public bool escaped = false;
    public bool alive = true;
    public bool stateCalled = true;

    public CharacterController2D player;
    public GameObject fuel;
    public GameObject dangerMeter;
    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        //SetGameState();
        character = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        noiseMeter.SetGlobalValue(dangerMeter.GetComponent<UnityEngine.UI.Slider>().value);
        healthBar.SetGlobalValue(player.life);
        jetpackFuel.SetGlobalValue(fuel.GetComponent<UnityEngine.UI.Slider>().value);


        /*if(stateCalled == false)
        {
            if (EventManager.ConstalationsKeysAcquired == 0 && alive == true)
            {
                AkSoundEngine.SetState("Music_Segment", "Start");
                stateCalled = true;
            }
            else if (EventManager.ConstalationsKeysAcquired == 1 && alive == true)
            {
                AkSoundEngine.SetState("Music_Segment", "Upgrade_1");
                stateCalled = true;
            }
            else if (EventManager.ConstalationsKeysAcquired == 2 && alive == true)
            {
                AkSoundEngine.SetState("Music_Segment", "Upgrade_2");
                stateCalled = true;
            }
            else if (EventManager.ConstalationsKeysAcquired == 3 && alive == true)
            {
                AkSoundEngine.SetState("Music_Segment", "Upgrade_3");
                stateCalled = true;
            }
            else if (timeAttack && alive == true)
            {
                AkSoundEngine.SetState("Music_Segment", "Upgrade_4");
                stateCalled = true;
            }
            else if (escaped && alive == true)
            {
                timeAttack = false;
                AkSoundEngine.SetState("Music_Segment", "Escaped");
                stateCalled = true;
            }
            else if (alive == false)
            {
                AkSoundEngine.SetState("Player_State", "Dead");
                alive = true;
                stateCalled = true;
            }
        }
        UpdateState();*/
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
        AkSoundEngine.SetState("Music_Segment", "Start");
        stateCalled = false;
    }

}
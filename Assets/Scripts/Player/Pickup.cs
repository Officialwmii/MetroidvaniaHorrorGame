using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public AudioNode subtitles;

    public enum UpgradeType {

        //Resources
        FuelResource, GrenadeResource, HealthPack,
        
        //Numberical upgrades
        StunCooldown, MaxFuel, MaxGranade,  
        
        //Collectables
        CollectablePickup, AudioLogPickup,
        
        //Ability upgrades
        GainAbilityJetpack, GainAbilityArmour, GainAbilityRocketLauncher, GainAbilityFuelRefill,
        
        ConstalationKey,

        //Don't destroy
        RefillStation, StartingRoom
    };
    public UpgradeType upgradeType;
    private bool DestroyObject;
    private GameObject particles;
    public AudioClip SFXDenial;

    public int Variable;

    // Start is called before the first frame update
    void Start()
    {

        particles = (GameObject)Resources.Load("prefabs/UpgradeParticles", typeof(GameObject));


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) {

           // Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            DestroyObject = true;
            EventManager.RefillFuel();

            switch (upgradeType)
            {
                //Resource pickups
                case UpgradeType.FuelResource:
                    AkSoundEngine.PostEvent("Jetpack_Refill", col.gameObject);
                    EventManager.FuelPickup();
                    
                    EventManager.sub("Fuel container - Recover 100% fuel, +10 max fuel capacity.",3f);
                    break;
                case UpgradeType.GrenadeResource:
                    if (EventManager.grenades == EventManager.MaxGrenades) {
                        EventManager.sub("No room for any more Cryo grenades.", 2f);
                        DestroyObject = false;
                        AudioSource.PlayClipAtPoint(SFXDenial, gameObject.transform.position, 0.1f);
                    }
                    else
                    {EventManager.sub("Cryo Grenade - +1 grenade. Freezes unprotected lifeforms.", 3f); EventManager.GrenadePickup(); }
                    

                    break;
                case UpgradeType.HealthPack:
                    string temptext = "Health pack - Recover 1 health. " +(EventManager.LifeShards+1) +" / 3 rations.";
                    if (EventManager.LifeShards == 2) temptext = temptext + " +1 health point.";
                    EventManager.sub(temptext, 3f);
                    AkSoundEngine.PostEvent("Health_Pickup", col.gameObject);
                    EventManager.HealthPickup();
                    break;
                    
                //Numericall upgrades
                case UpgradeType.StunCooldown:
                    EventManager.UpgradeStunGun();
                    AkSoundEngine.PostEvent("Jetpack_Refill", col.gameObject);
                    EventManager.sub("Stun Repluser Expansion - Energy Cooldown Reduction #"+ EventManager.stunUpgrade+", duration " + EventManager.CooldownTime + "s.", 3f);
                    break;
                case UpgradeType.MaxFuel: // Not used
                    EventManager.UpgradeStunGun();
                    break;
                case UpgradeType.MaxGranade:
                    EventManager.UpgradeMaxGrenade();
                    AkSoundEngine.PostEvent("Alien_Artifact", col.gameObject);
                    EventManager.sub("Grenade slot - Increase maximum cryo grenade capacity +1.", 3f);
                    break;

                //Collectable 
                case UpgradeType.CollectablePickup:
                    EventManager.CollectablePickup();
                    EventManager.sub("Alien artifact acquired - Artifact found on Virgil Prime, "+EventManager.Collectables+"/ 5.", 3f);
                    break;

                case UpgradeType.AudioLogPickup:
                    EventManager.AudioLogPickup(subtitles);
                    break;

                // Ability upgrades
                case UpgradeType.GainAbilityJetpack:
                    AkSoundEngine.PostEvent("Grenade_Upgrade", col.gameObject);
                    EventManager.GainAbilityJetpack(Variable);
                    if(EventManager.HasJetpack && !EventManager.HasDoubleJetpack) 
                        EventManager.sub("Damaged Jetpack Acquired! - Use to dash short distances.", 3f);
                    if (EventManager.HasJetpack && EventManager.HasDoubleJetpack)
                        EventManager.sub("Full Jetpack Acquired - Hold L1 or W to fly. \n Flying in gravity bounded areas consumes a large amount of fuel.", 3f);
                    break;
                case UpgradeType.GainAbilityArmour:
                    AkSoundEngine.PostEvent("Grenade_Upgrade", col.gameObject);
                    EventManager.GainAbilityArmour();
                    EventManager.sub("EVA Suit Acquired! - Protects against gas and allows transport in oxygen free zones.", 3f);


                    break;
                case UpgradeType.GainAbilityRocketLauncher:
                    AkSoundEngine.PostEvent("Grenade_Upgrade", col.gameObject);
                    EventManager.GainAbilityRocketLauncher();
                    EventManager.sub("Rocket Launcher Acquired! - Destroys unprotected lifeforms.", 3f);

                    break;
                case UpgradeType.GainAbilityFuelRefill:
                    EventManager.GainAbilityFuelRefill();
                    AkSoundEngine.PostEvent("Jetpack_Refill", col.gameObject);
                    EventManager.sub("Fuel Recovery Module Acquired! - Regenerates fuel on the ground.", 3f);
                    break;
                case UpgradeType.ConstalationKey:
                    EventManager.GainConstalationKey();
                    AkSoundEngine.PostEvent("Constalation_Key", col.gameObject);
                    EventManager.sub("Constellation Protocol Acquired! - " + EventManager.ConstalationsKeysAcquired+"/3 to unlock the Control Bridge.", 3f);

                    break;
                case UpgradeType.RefillStation:
                    EventManager.RefillFuel(); DestroyObject = false;
                    EventManager.CheckpoiontReached(gameObject);
                    EventManager.sub("Checkpoint Reached! - Fuel Restored.", 1f);
                    AkSoundEngine.PostEvent("Jetpack_Refill", col.gameObject);
                    break;
                case UpgradeType.StartingRoom:
                    EventManager.StartingRoom(); DestroyObject = false;
                    EventManager.sub("Health Restored", 1f);
                    break;
            }

            if (DestroyObject) { Destroy(gameObject); 
                GameObject NewParticle = Instantiate(particles, gameObject.transform.position, Quaternion.identity); }

        }

    }

}

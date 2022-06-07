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
                        DestroyObject = false;}
                    else
                    {EventManager.sub("Cryo Grenade - +1 grenade. Freezes unprotected lifeforms.", 3f);}
                    
                    EventManager.GrenadePickup();
                    break;
                case UpgradeType.HealthPack:
                    string temptext = "Health pack - Recover 1 health. " +(EventManager.LifeShards+1) +" / 3 rations.";
                    if (EventManager.LifeShards == 2) temptext = temptext + "+1 health point.";
                    EventManager.sub(temptext, 3f);
                    AkSoundEngine.PostEvent("Health_Pickup", col.gameObject);
                    EventManager.HealthPickup();
                    break;
                    
                //Numericall upgrades
                case UpgradeType.StunCooldown:
                    EventManager.UpgradeStunGun();

                    EventManager.sub("Stun Repluser Expansion - Energy Cooldown Reduction #"+ EventManager.stunUpgrade+", duration " + EventManager.CooldownTime + "s.", 3f);
                    break;
                case UpgradeType.MaxFuel: // Not used
                    EventManager.UpgradeStunGun();
                    break;
                case UpgradeType.MaxGranade:
                    EventManager.UpgradeMaxGrenade();

                    EventManager.sub("Grenade slot -Increase maximum cryo grenade capacity +1.", 3f);
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
                    EventManager.GainAbilityJetpack();
                    break;
                case UpgradeType.GainAbilityArmour:
                    EventManager.GainAbilityArmour();
                    break;
                case UpgradeType.GainAbilityRocketLauncher:
                    EventManager.GainAbilityRocketLauncher();
                    break;
                case UpgradeType.GainAbilityFuelRefill:
                    EventManager.GainAbilityFuelRefill();
                    EventManager.sub("Fuel recovery module acquired - Regenerates fuel on the ground.", 3f);
                    break;
                case UpgradeType.ConstalationKey:
                    EventManager.GainConstalationKey();
                    break;
                case UpgradeType.RefillStation:
                    EventManager.RefillFuel(); DestroyObject = false;
                    EventManager.sub("Fuel Restored", 1f);
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

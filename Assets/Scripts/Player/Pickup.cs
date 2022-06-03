using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum UpgradeType {

        //Resources
        FuelResource, GrenadeResource, HealthPack,
        
        //Numberical upgrades
        StunCooldown, MaxFuel, MaxGranade,  
        
        //Collectables
        CollectablePickup, AudioLogPickup,
        
        //Ability upgrades
        GainAbilityJetpack, GainAbilityArmour, GainAbilityRocketLauncher, GainAbilityFuelRefill,
        
        ConstalationKey,RefillStation
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
                    EventManager.FuelPickup();
                    break;
                case UpgradeType.GrenadeResource:
                    if (EventManager.grenades == EventManager.MaxGrenades) DestroyObject = false;
                    EventManager.GrenadePickup();
                    break;
                case UpgradeType.HealthPack:
                    EventManager.HealthPickup();
                    break;
                    
                //Numericall upgrades
                case UpgradeType.StunCooldown:
                    EventManager.UpgradeStunGun();
                    break;
                case UpgradeType.MaxFuel: // Not used
                    EventManager.UpgradeStunGun();
                    break;
                case UpgradeType.MaxGranade:
                    EventManager.UpgradeMaxGrenade();
                    break;

                //Collectable 
                case UpgradeType.CollectablePickup:
                    EventManager.CollectablePickup();
                    break;
                case UpgradeType.AudioLogPickup:
                    EventManager.AudioLogPickup();
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
                    break;

                case UpgradeType.ConstalationKey:
                    EventManager.GainConstalationKey();
                    break;
                case UpgradeType.RefillStation:
                    EventManager.RefillFuel(); DestroyObject = false;
                    break;
            }

            if (DestroyObject) { Destroy(gameObject); 
                GameObject NewParticle = Instantiate(particles, gameObject.transform.position, Quaternion.identity); }

        }

    }

}

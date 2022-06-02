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
        
        ConstalationKey
    };
    public UpgradeType upgradeType;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) {

           // Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            Destroy(gameObject);

            switch (upgradeType)
            {
                //Resource pickups
                case UpgradeType.FuelResource:
                    EventManager.FuelPickup();
                    break;
                case UpgradeType.GrenadeResource:
                    EventManager.GrenadePickup();
                    break;
                case UpgradeType.HealthPack:
                    EventManager.HealthPickup();
                    break;
                    
                //Numericall upgrades
                case UpgradeType.StunCooldown:
                    EventManager.UpgradeStunGun();
                    break;
                case UpgradeType.MaxFuel:
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

            }
        



        }

    }

}

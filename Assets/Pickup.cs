using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum UpgradeType { StunCooldown, MaxFuel, MaxGranade };
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

            Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            Destroy(gameObject);

            switch (upgradeType)
            {
                case UpgradeType.StunCooldown:
                    EventManager.UpgradeStunGun();
                    break;
                case UpgradeType.MaxFuel:
                    EventManager.UpgradeStunGun();
                    break;
                case UpgradeType.MaxGranade:
                    EventManager.UpgradeStunGun();
                    break;
            }
        



        }

    }

}

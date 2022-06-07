using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSidedDoor : MonoBehaviour
{

    public GameObject Door;
    
    public  enum DoorType
    {

        //Resources
        OneWayDoor, ZeroGravity, ConstalationKey, Credits
    };
    public DoorType doorType;

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
        if (col.CompareTag("Player"))
        {
            switch (doorType)
            {
                case DoorType.OneWayDoor:
                    Destroy(gameObject);
                    Door.GetComponent<Animator>().SetBool("Opening", true);
                    Debug.Log("Open door");
                    Door.GetComponent<AudioSource>().Play();

                    //Unlock();
                    break;

                case DoorType.ZeroGravity:
                    if (EventManager.HasArmour){ Unlock(); }
                    break;
                case DoorType.ConstalationKey:
                    if (EventManager.ConstalationsKeysAcquired>=3) { Unlock(); }
                    break;

                case DoorType.Credits:
                    AkSoundEngine.PostEvent("Escape_Pod_Sequence", col.gameObject);
                    EventManager.GoToCredits();
                    break;
                    
            }

        }
    }


    private void Unlock() {

        Destroy(Door);
        Destroy(gameObject);
        //Debug.Log("Open door");

    }
}

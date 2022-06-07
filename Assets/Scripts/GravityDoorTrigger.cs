using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDoorTrigger : MonoBehaviour
{
    public GameObject animatorObject;
    private Animator animator;

    public AK.Wwise.Event shipAIAnnouncementClosed;
    public AK.Wwise.Event shipAIAnnouncementOpened;
    public AudioNode doorClosed;
    public AudioNode doorOpened;

    public AudioClip SFXOpen;
    public AudioClip SFXClose;
    public AudioClip SFXCantEnter;

    public  enum DoorType
    {

        //Resources
        Sector, ZeroGravity
    };
    public DoorType doorType;

    // Start is called before the first frame update
    void Start()
    {
        animator = animatorObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") ) {
           
            if (doorType==DoorType.ZeroGravity && EventManager.HasArmour) animator.SetBool("Opening", true);
            if (doorType == DoorType.Sector)
            {
                animator.SetBool("Opening", true);

                GetComponent<AudioSource>().clip = SFXOpen;
                GetComponent<AudioSource>().Play();

            }
            if (doorType == DoorType.ZeroGravity && EventManager.HasArmour)
            {
                shipAIAnnouncementOpened.Post(col.gameObject);
                SubtitlesText.instance.SetSubtitle(doorOpened.subtitle, doorOpened.duration);
                GetComponent<AudioSource>().clip = SFXOpen;
                GetComponent<AudioSource>().Play();

            }
            if (doorType == DoorType.ZeroGravity && EventManager.HasArmour == false)
            {
                shipAIAnnouncementClosed.Post(col.gameObject);
                SubtitlesText.instance.SetSubtitle(doorClosed.subtitle, doorClosed.duration);
                GetComponent<AudioSource>().clip = SFXCantEnter;
                GetComponent<AudioSource>().Play();  
            }
            //Debug.Log("open door");

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
           animator.SetBool("Opening", false);

            if(doorType == DoorType.ZeroGravity && EventManager.HasArmour)
            {
                GetComponent<AudioSource>().clip = SFXClose;
                GetComponent<AudioSource>().Play();

            }
            if (doorType == DoorType.Sector)
            {
                GetComponent<AudioSource>().clip = SFXClose;
                GetComponent<AudioSource>().Play();

            }

        }
    }


}

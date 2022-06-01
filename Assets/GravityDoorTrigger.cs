using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDoorTrigger : MonoBehaviour
{
    public GameObject animatorObject;
    private Animator animator;

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
        if (col.CompareTag("Player")&& EventManager.HasArmour) {
            animator.SetBool("Opening", true);
            Debug.Log("open door");

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
           animator.SetBool("Opening", false);


        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AK.Wwise.Event footsteps;
    public GameObject player;

    public void PlayFootstepSound()
    {
        footsteps.Post(player);
    }

   
}

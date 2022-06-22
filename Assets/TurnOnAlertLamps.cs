using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnAlertLamps : MonoBehaviour
{
    // Start is called before the first frame update

    private bool IsActive = false;
    private GameObject Light; 


    void Start()
    {
        Light = gameObject.transform.Find("Light").gameObject;
        Light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive == false && EventManager.EscapeSequence) {

            Light.SetActive(true);
            IsActive = true;


        }
    }
}

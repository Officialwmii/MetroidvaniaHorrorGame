using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuitCredits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Cursor.visible = false;

        if (Input.GetButtonDown("Submit")) { SceneManager.LoadScene("Main Menu"); }

    }
}

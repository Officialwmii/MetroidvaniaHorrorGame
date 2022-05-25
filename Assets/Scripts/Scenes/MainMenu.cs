using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame ()
    {
        // Use this when scenes are added to the queue in the build settings
        SceneManager.LoadScene(1);
        // SceneManager.LoadScene("Christoffer");
    }

    public void BackToMenu()
    {
        // Use this when scenes are added to the queue in the build settings
        SceneManager.LoadScene(0);
        // SceneManager.LoadScene("Main Menu");
    }

    public void Credits()
    {
        // Use this when scenes are added to the queue in the build settings
        SceneManager.LoadScene(2);
        // SceneManager.LoadScene("Credits");
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}

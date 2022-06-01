using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] GameObject pauseMenuUI;
    private GameObject resumeButton;
    private GameObject eventSystemObject;
    StandaloneInputModule SIAMComp;
    MenuInputModule MIAMComp;
    EventSystem eSystemComponent;

    void Start()
    {
        eventSystemObject = GameObject.Find("EventSystem");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Paus"))
            //Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        refreshInput();
        Destroy(MIAMComp);
        SIAMComp.enabled = true;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        refreshInput();
        if (SIAMComp.enabled)
        {
            SIAMComp.enabled = false;
        }
        eventSystemObject.AddComponent<MenuInputModule>();
        
        pauseMenuUI.SetActive(true);
        resumeButton = GameObject.Find("ResumeButton");
        eSystemComponent.firstSelectedGameObject = resumeButton;
        eSystemComponent.SetSelectedGameObject(resumeButton);

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void refreshInput()
    {
        eSystemComponent = eventSystemObject.GetComponent<EventSystem>();
        SIAMComp = eventSystemObject.GetComponent<StandaloneInputModule>();
        MIAMComp = eventSystemObject.GetComponent<MenuInputModule>();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }
}

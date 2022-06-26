using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] GameObject pauseMenuUI;
    private GameObject resumeButton;
    private GameObject eventSystemObject;
    StandaloneInputModule SIAMComp;
    MenuInputModule MIAMComp;
    EventSystem eSystemComponent;

    public UISounds pauseMenuSounds;

    public GameObject Inventory;
    public GameObject Controls;
    private bool ViewingControls = false;
    public GameObject ControlText;

    private bool SureYouWantToQuit = false;
    public GameObject QuitText;


    void Start()
    {
        eventSystemObject = GameObject.Find("EventSystem");
    }


    private void OnEnabled()
    {
       // Inventory = GameObject.Find("Inventory");
       // Controls = GameObject.Find("Controls");
        if (ViewingControls) { Controls.SetActive(true); Inventory.SetActive(false); }
        else { Controls.SetActive(false); Inventory.SetActive(false); }    
        //ControlText = GameObject.Find("Control Text (TMP)");
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Paus"))
        {
            if (GameIsPaused)
            {
                Resume();
                pauseMenuSounds.BackSound();
            }
            else
            {
                Pause();
                pauseMenuSounds.PauseSound();
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
        QuitText.GetComponent<TMP_Text>().text = "Quit";
        SureYouWantToQuit = false;

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
        Debug.Log("Show Controls");

        if (ViewingControls)
        {

            ViewingControls = false;
            Controls.SetActive(false);
            Inventory.SetActive(true);
            ControlText.GetComponent<TextMeshPro>().text = "CONTROLS";

        }
        else
        {

            ViewingControls = true;
            Controls.SetActive(true);
           Inventory.SetActive(false);
            ControlText.GetComponent<TMP_Text>().text = "INVENTORY";

        }


        // SceneManager.LoadScene("Main Menu");
        //Time.timeScale = 1f;
    }

    public void QuitGame()
    {



        if (SureYouWantToQuit)
        {
            Application.Quit();
            Debug.Log("QUIT");
        }
        else { SureYouWantToQuit = true;

            QuitText.GetComponent<TMP_Text>().text = "Confirm?";
        }



    }

    public void Respawn() {
        
        Resume();
        EventManager.OnRespawning();

    }

}

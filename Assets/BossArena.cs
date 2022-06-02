using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossArena : MonoBehaviour
{
    // Start is called before the first frame update
    public bool PlayerInArena = false;
    public GameObject bossCamera;
    public GameObject playerCamera;
    public GameObject bossStuff;
    public GameObject BossmanHP;
    public PlayableDirector director;
    public GameObject BossHider;
    public GameObject vignette;


    void Start()
    {
        BossHider.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInArena)
        {
            director.Play();
            BossHider.SetActive(true);
            bossCamera.SetActive(true);
            bossStuff.SetActive(true);
            BossmanHP.SetActive(true);
            
            vignette.SetActive(false);
            playerCamera.SetActive(false);
        }

        if (!PlayerInArena)
        {
            bossCamera.SetActive(false);
            bossStuff.SetActive(false);
            BossmanHP.SetActive(false);
            BossHider.SetActive(false);

            playerCamera.SetActive(true);
            vignette.SetActive(true);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            PlayerInArena = true;           
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInArena = false;
        }
    }
}

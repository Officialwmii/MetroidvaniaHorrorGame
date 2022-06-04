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
    public GameObject ArenaLockLeft;
    public GameObject ArenaLockRight;
    public GameObject Boss;
    public bool WasInArenaLastFrame = false;

    void Start()
    {
        BossHider.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (WasInArenaLastFrame == false)
        {
            Boss.GetComponent<Bossman>().life = 400;
        }
        
        if (Boss.GetComponent<Bossman>().life <= 0)
        {
            ArenaLockLeft.SetActive(false);
            ArenaLockRight.SetActive(false);
        }
        
        if (PlayerInArena)
        {
            WasInArenaLastFrame = true;
            director.Play();
            BossHider.SetActive(true);
            bossCamera.SetActive(true);
            bossStuff.SetActive(true);
            BossmanHP.SetActive(true);
            if (Boss.GetComponent<Bossman>().life > 0)
            {
                ArenaLockLeft.SetActive(true);
                ArenaLockRight.SetActive(true);
            }
            
            vignette.SetActive(false);
            playerCamera.SetActive(false);
        }

        if (!PlayerInArena)
        {
            bossCamera.SetActive(false);
            bossStuff.SetActive(false);
            BossmanHP.SetActive(false);
            BossHider.SetActive(false);
            if (Boss.GetComponent<Bossman>().life > 0)
            {
                ArenaLockLeft.SetActive(false);
                ArenaLockRight.SetActive(false);
                WasInArenaLastFrame = false;
            }
            playerCamera.SetActive(true);
            vignette.SetActive(true);

            if (Boss.GetComponent<Bossman>().life <= 0)
            {
                ArenaLockLeft.SetActive(true);
                ArenaLockRight.SetActive(true);
                WasInArenaLastFrame = true;
            }
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

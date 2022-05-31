using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArena : MonoBehaviour
{
    // Start is called before the first frame update
    public bool PlayerInArena = false;
    public GameObject bossCamera;
    public GameObject playerCamera;
    public GameObject bossStuff;
    public GameObject BossmanHP;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInArena)
        {
            bossCamera.SetActive(true);
            bossStuff.SetActive(true);
            BossmanHP.SetActive(true);

            playerCamera.SetActive(false);
        }

        if (!PlayerInArena)
        {
            bossCamera.SetActive(false);
            bossStuff.SetActive(false);
            BossmanHP.SetActive(false);

            playerCamera.SetActive(true);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            PlayerInArena = true;           
        }

        else
        {
            PlayerInArena = false;
        }
    }
}

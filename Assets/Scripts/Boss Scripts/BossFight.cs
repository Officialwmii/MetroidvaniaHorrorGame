using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossFight : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Phase_1;
    public GameObject Phase_2;
    public GameObject Phase_3;
    public GameObject Phase_4;
    public GameObject Phase_5;
    public GameObject Phase_6;
    public GameObject End_Phase;
    public GameObject The_Bossman;

    public GameObject Attack1v1;
    public GameObject Attack1v2;
    public GameObject Attack1v3;
    public GameObject Attack1v4;


    public GameObject BossmanHP;

    public int RandomBossAttack = 1;
    public int RandomBossAttackVariant = 1;

    public float bossAttackTimer = 0;

    public GameObject Player;

    public float BossHP;

    [Header("Debug")]
    public bool SET_BOSS_HP_400;
    public bool SET_BOSS_HP_380;
    public bool SET_BOSS_HP_300;
    public bool SET_BOSS_HP_200;
    public bool SET_BOSS_HP_100;
    public bool SET_BOSS_HP_0;


    void OnEnable() { 

        Player = GameObject.Find("Player");

        Phase_1.SetActive(false);
        Phase_1.GetComponent<PlayableDirector>().Stop();
        Phase_1.GetComponent<PlayableDirector>().time = 0;

        Phase_2.SetActive(false);
        Phase_2.GetComponent<PlayableDirector>().Stop();
        Phase_2.GetComponent<PlayableDirector>().time = 0;

        Phase_3.SetActive(false);
        Phase_3.GetComponent<PlayableDirector>().Stop();
        Phase_3.GetComponent<PlayableDirector>().time = 0;

        Phase_4.SetActive(false);
        Phase_5.SetActive(false);
        Phase_6.SetActive(false);
        End_Phase.SetActive(false);

        Attack1v1.SetActive(true);
        Attack1v1.GetComponent<PlayableDirector>().Play();
        Attack1v1.GetComponent<PlayableDirector>().time = Attack1v1.GetComponent<PlayableDirector>().playableAsset.duration;
        //Attack1v1.GetComponent<PlayableDirector>().time = 0;
        Attack1v2.SetActive(true);
        Attack1v2.GetComponent<PlayableDirector>().Play();

        //Attack1v2.GetComponent<PlayableDirector>().time = 0;
        Attack1v2.GetComponent<PlayableDirector>().time = Attack1v2.GetComponent<PlayableDirector>().playableAsset.duration;

        Attack1v3.SetActive(true);
        Attack1v3.GetComponent<PlayableDirector>().Play();
        //Attack1v3.GetComponent<PlayableDirector>().time = 0;
        Attack1v3.GetComponent<PlayableDirector>().time = Attack1v3.GetComponent<PlayableDirector>().playableAsset.duration;

        Attack1v4.SetActive(true);
        Attack1v4.GetComponent<PlayableDirector>().Play();
        //Attack1v4.GetComponent<PlayableDirector>().time = 0;
        Attack1v4.GetComponent<PlayableDirector>().time = Attack1v4.GetComponent<PlayableDirector>().playableAsset.duration;
        Debug.Log("Rests bossfight");



    }

    // Update is called once per frame
    void Update()
    {
        UpdateBossHealthMeter();
        //DebugPhases();
        //OldBossPhases();

        //2s is hard mode, 3 normal, 4 easy
        if (bossAttackTimer <= 0) { bossAttackTimer = 1.5f+Random.Range(0, 2* (BossHP / 400)) +BossHP/400; NewBossPhases(); }
        if (BossHP < 400) bossAttackTimer = bossAttackTimer - Time.deltaTime;


    }

    void UpdateBossHealthMeter()
    {

        BossHP = The_Bossman.GetComponent<Bossman>().life;
        BossmanHP.GetComponent<UnityEngine.UI.Slider>().value = BossHP / The_Bossman.GetComponent<Bossman>().maxLife;
    }

    private void ActivateAttack(GameObject BossAttack) {

        BossAttack.GetComponent<PlayableDirector>().Stop();
        BossAttack.GetComponent<PlayableDirector>().Play();
        Debug.Log("Attack"+BossAttack.name);
    }


    private void RandomMirror(GameObject BossAttack)
    {
        RandomBossAttackVariant = (int)Random.Range(1, 2 + 1);
        /*switch (RandomBossAttackVariant) {

            case (1): BossAttack.transform.localScale = new Vector3(1, 1, 1); break;
            case (2): BossAttack.transform.localScale = new Vector3(-1, 1, 1); break;
        }*/

        if (Player.GetComponent<Transform>().position.x <= gameObject.GetComponent<Transform>().position.x) { 
            BossAttack.transform.localScale = new Vector3(1, 1, 1); }
        else { BossAttack.transform.localScale = new Vector3(-1, 1, 1); }


    }

    private void RandomMirrorFlip(GameObject BossAttack)
    {
        RandomBossAttackVariant = (int)Random.Range(1, 4 + 1);
        switch (RandomBossAttackVariant)
        {
            case (1): BossAttack.transform.localScale = new Vector3(1, 1, 1); break;
            case (2): BossAttack.transform.localScale = new Vector3(-1, 1, 1); break;
            case (3): BossAttack.transform.localScale = new Vector3(1, -1, 1); break;
            case (4): BossAttack.transform.localScale = new Vector3(-1, -1, 1); break;
        }

    }



    private void NewBossPhases()
    {

        if (BossHP == 400)
        {
            OnEnable();
        }

        if (BossHP <= 399 && BossHP > 0){

            RandomBossAttack = (int)Random.Range(0,4+1);
            //RandomBossAttack = 1;

            switch (RandomBossAttack) { 


                case (0): //Nothing! 
                    break;

                case (1):
                    ActivateAttack(Attack1v1);
                    RandomMirrorFlip(Attack1v1);
                    break;

                case (2):
                    ActivateAttack(Attack1v2);
                    RandomMirrorFlip(Attack1v2);
                    break;

                case (3):
                    ActivateAttack(Attack1v3);
                    RandomMirror(Attack1v3);
                    break;
                case (4):
                    ActivateAttack(Attack1v4);
                    RandomMirrorFlip(Attack1v4);

                    break;
                case (5):
                    Phase_5.SetActive(true);
                    Phase_5.GetComponent<PlayableDirector>().Play();

                    break;
                default:
                    break;
            }


            
        }

        if (The_Bossman.GetComponent<Bossman>().life <= 0)
        {
            Phase_1.SetActive(false);
            Phase_2.SetActive(false);
            Phase_3.SetActive(false);
            Phase_4.SetActive(false);
            Phase_5.SetActive(false);
            Phase_6.SetActive(false);

            Attack1v1.SetActive(false);
            Attack1v2.SetActive(false);
            Attack1v3.SetActive(false);
            Attack1v4.SetActive(false);

            End_Phase.SetActive(true);
            EventManager.OnBossCompleted();
            Debug.Log("Boss hp 0");
        }


    }


    private void OldBossPhases() {

        if (The_Bossman.GetComponent<Bossman>().life == 400){
            Phase_1.SetActive(false);
            Phase_2.SetActive(false);
            Phase_3.SetActive(false);
            Phase_4.SetActive(false);
            Phase_5.SetActive(false);
            Phase_6.SetActive(false);
            End_Phase.SetActive(false);
        }

        if (The_Bossman.GetComponent<Bossman>().life <= 380){
            Phase_1.SetActive(true);
        }


        if (The_Bossman.GetComponent<Bossman>().life <= 300){
            Phase_1.SetActive(false);
            Phase_2.SetActive(true);
        }

        if (The_Bossman.GetComponent<Bossman>().life <= 200){
            Phase_2.SetActive(false);
            Phase_3.SetActive(true);
            Phase_4.SetActive(true);
        }

        if (The_Bossman.GetComponent<Bossman>().life <= 100){
            Phase_4.SetActive(false);
            Phase_5.SetActive(true);
        }

        if (The_Bossman.GetComponent<Bossman>().life <= 0){
            Phase_1.SetActive(false);
            Phase_2.SetActive(false);
            Phase_3.SetActive(false);
            Phase_4.SetActive(false);
            Phase_5.SetActive(false);
            Phase_6.SetActive(false);
            End_Phase.SetActive(true);
            EventManager.OnBossCompleted();
            Debug.Log("Boss hp 0");
        }
    }


    void DebugPhases()
    {
        if (SET_BOSS_HP_400)
        {
            The_Bossman.GetComponent<Bossman>().life = 400;
            SET_BOSS_HP_380 = false;
            SET_BOSS_HP_300 = false;
            SET_BOSS_HP_200 = false;
            SET_BOSS_HP_100 = false;
            SET_BOSS_HP_0 = false;
        }
        if (SET_BOSS_HP_380)
        {
            The_Bossman.GetComponent<Bossman>().life = 380;
            SET_BOSS_HP_400 = false;
            SET_BOSS_HP_300 = false;
            SET_BOSS_HP_200 = false;
            SET_BOSS_HP_100 = false;
            SET_BOSS_HP_0 = false;
        }
        if (SET_BOSS_HP_300)
        {
            The_Bossman.GetComponent<Bossman>().life = 300;
            SET_BOSS_HP_400 = false;
            SET_BOSS_HP_380 = false;
            SET_BOSS_HP_200 = false;
            SET_BOSS_HP_100 = false;
            SET_BOSS_HP_0 = false;
        }
        if (SET_BOSS_HP_200)
        {
            The_Bossman.GetComponent<Bossman>().life = 200;
            SET_BOSS_HP_400 = false;
            SET_BOSS_HP_380 = false;
            SET_BOSS_HP_300 = false;
            SET_BOSS_HP_100 = false;
            SET_BOSS_HP_0 = false;
        }
        if (SET_BOSS_HP_100)
        {
            The_Bossman.GetComponent<Bossman>().life = 100;
            SET_BOSS_HP_400 = false;
            SET_BOSS_HP_380 = false;
            SET_BOSS_HP_300 = false;
            SET_BOSS_HP_200 = false;
            SET_BOSS_HP_0 = false;
        }
        if (SET_BOSS_HP_0)
        {
            The_Bossman.GetComponent<Bossman>().life = 0;
            SET_BOSS_HP_400 = false;
            SET_BOSS_HP_380 = false;
            SET_BOSS_HP_300 = false;
            SET_BOSS_HP_200 = false;
            SET_BOSS_HP_100 = false;
        }



    }
}

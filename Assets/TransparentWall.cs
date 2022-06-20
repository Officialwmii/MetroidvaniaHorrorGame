using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWall : MonoBehaviour
{
    public float Fade=2f;
    private bool StartFading = false;
    private float StartFade;


    // Start is called before the first frame update
    void Start()
    {
        StartFade = Fade;
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.g = 0;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;

    }

    // Update is called once per frame
    void Update()
    {
        if (StartFading) {

            Fade = Fade - Time.deltaTime;

            Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a = 150f*Fade/StartFade/100;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;

            //if (Fade <= 0) Destroy(gameObject);

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") )
        {
            StartFading = true;
        }

    }

}

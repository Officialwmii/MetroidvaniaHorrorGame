using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;

    public float angle = 0;

    private float currentSpeed = 10f;
    public bool AimngBullet = true;

    Vector2 newVelocity;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.rotation.Set(0f,0f, angle,0f);

    }

    // Update is called once per frame
    void Update()
    {
        //        Vector3 tempPos = pos;
        //        tempPos.y += speed * Time.deltaTime;
        //        pos = tempPos;

        //       this.transform.Translate(VectorFromAngle(angle) * Time.deltaTime*60);


        if (AimngBullet)
            //this.GetComponent<Rigidbody2D>().velocity = this.transform.right * speed * Time.deltaTime * 60 * 10;
        this.GetComponent<Rigidbody2D>().velocity = this.transform.right * speed ;

        else
        {

            //use sin and cos to work out x and y speed
             newVelocity.x = Mathf.Cos(angle *Mathf.Deg2Rad) / speed;
            newVelocity.y = Mathf.Sin(angle *Mathf.Deg2Rad) / speed;


            //apply the new velocity to the current object
            this.GetComponent<Rigidbody2D>().velocity = newVelocity;

        }


    }

    public void SetAngle(float _Angle) {

        angle = _Angle;

        
    }

    Vector2 VectorFromAngle(float theta)
    {
        return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
    }


    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

}

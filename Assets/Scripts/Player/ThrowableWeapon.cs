using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableWeapon : MonoBehaviour
{
	public Vector2 direction;
	public bool hasHit = false;
	public float speed = 10f;
	public bool StunGun = true;
	public Sprite rocket;
	public GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
		if (EventManager.HasRocketLauncher) { StunGun = false; GetComponent<SpriteRenderer>().sprite = rocket; }
		if (direction.x <= 0f) GetComponent<SpriteRenderer>().flipX = true;



	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if ( !hasHit)
		GetComponent<Rigidbody2D>().velocity = direction * speed;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{

			if(StunGun) collision.gameObject.SendMessage("Stun", 5f);
			else collision.gameObject.SendMessage("ApplyDamage", Mathf.Sign(direction.x) * 30f/4);

			Destroy(gameObject);
			GameObject NewParticle = Instantiate(particles, gameObject.transform.position, Quaternion.identity);
		}
		else if (collision.gameObject.tag == "Jumpthrough")
		{
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
		}

		else if (collision.gameObject.tag != "Player")
		{
			Destroy(gameObject);
			GameObject NewParticle = Instantiate(particles, gameObject.transform.position, Quaternion.identity);
		}


	}
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
	public Vector2 direction;
	public bool hasHit = false;
	public float speed = -10f;
	public float damageAmount = 1f;
	public GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if ( !hasHit)
		GetComponent<Rigidbody2D>().velocity = direction * speed;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.SendMessage("ApplyDamage", damageAmount);
			Destroy(gameObject);
			GameObject NewParticle = Instantiate(particles, gameObject.transform.position, Quaternion.identity);

		}
		else if (collision.gameObject.tag == "Enemy")
		{
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
		}
		else if (collision.gameObject.tag == "Jumpthrough")
		{
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
		}

		else
        {
			Destroy(gameObject);
			GameObject NewParticle = Instantiate(particles, gameObject.transform.position, Quaternion.identity);

		}
	}
}

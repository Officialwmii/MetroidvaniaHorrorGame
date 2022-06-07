using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float life = 10;
	private bool isPlat;
	private bool isObstacle;
	private Transform fallCheck;
	private Transform wallCheck;
	public LayerMask turnLayerMask;
	private Rigidbody2D rb;

	private bool facingRight = true;
	
	public float speed = 5f;

	public bool isInvincible = false;
	private bool isHitted = false;
	private bool isStunned = false;
	public bool Exploadable = false;
	public float damageAmount = 1f;

	public bool onAlert = false;
	public bool crawler = true;
	private GameObject FrozenEnemy;
	private GameObject particles;
	private GameObject particles2;
	public GameObject enemy;

	void Awake () {

		if (Exploadable){ FrozenEnemy = (GameObject)Resources.Load("prefabs/FrozenCrawler", typeof(GameObject));}
		else { FrozenEnemy = (GameObject)Resources.Load("prefabs/FrozenInfected", typeof(GameObject));	}

		particles = (GameObject)Resources.Load("prefabs/EnemyDeath", typeof(GameObject));
		particles2 = (GameObject)Resources.Load("prefabs/SpiderLegs", typeof(GameObject));

		fallCheck = transform.Find("FallCheck");
		wallCheck = transform.Find("WallCheck");
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (EventManager.EnemiesAlerted == true)
        {
			onAlert = true;
			speed = 10f;
        }
        else
        {
			onAlert = false;
			speed = 5f;
        }

		if (life <= 0) {

			//to only spawn particles once
			if (transform.GetComponent<Animator>().GetBool("IsDead") == false)
			{ GameObject NewParticle = Instantiate(particles, gameObject.transform.position, Quaternion.identity);
				if (Exploadable)
				{ GameObject NewParticle2 = Instantiate(particles2, gameObject.transform.position, Quaternion.identity); }
			}

			transform.GetComponent<Animator>().SetBool("IsDead", true);
			StartCoroutine(DestroyEnemy());

		}

		isPlat = Physics2D.OverlapCircle(fallCheck.position, .2f, 1 << LayerMask.NameToLayer("Default"));
		isObstacle = Physics2D.OverlapCircle(wallCheck.position, .2f, turnLayerMask);

		if (!isHitted && life > 0 && Mathf.Abs(rb.velocity.y) < 0.5f && !isStunned)
		{
			if (isPlat && !isObstacle && !isHitted)
			{
				if (facingRight)
				{
					rb.velocity = new Vector2(-speed, rb.velocity.y);
				}
				else
				{
					rb.velocity = new Vector2(speed, rb.velocity.y);
				}
			}
			else
			{
				Flip();
			}
		}
	}

	void Flip (){
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	public void ApplyDamage(float damage) {
		if (!isInvincible) 
		{
			float direction = damage / Mathf.Abs(damage);
			damage = Mathf.Abs(damage);
			transform.GetComponent<Animator>().SetBool("Hit", true);
			life -= damage;
			rb.velocity = Vector2.zero;
			rb.AddForce(new Vector2(direction * 500f, 100f));
			StartCoroutine(HitTime());
		}
	}


	public void Stun(float StunDuration){
		StartCoroutine(StunTime(StunDuration));

		if (Exploadable) { ApplyDamage(2000); AkSoundEngine.PostEvent("Crawler_Explode", enemy); EventManager.AddDanger(15f); }

	}
	IEnumerator StunTime(float _StunDuration){
		isStunned = true;
		transform.GetComponent<Animator>().SetBool("IsStunned", true);
		yield return new WaitForSeconds(_StunDuration);
		isStunned = false;
		transform.GetComponent<Animator>().SetBool("IsStunned", false);
	}

	public void Frozen()
	{

		GameObject FrozenCorpse = Instantiate(FrozenEnemy, transform.position, Quaternion.identity);
		Physics2D.IgnoreCollision(FrozenCorpse.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());

		Destroy(gameObject);
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && life > 0)
		{
			collision.gameObject.GetComponent<CharacterController2D>().ApplyDamage(-damageAmount);

			if (Exploadable){ ApplyDamage(2000); EventManager.AddDanger(15); }

		}
	}

	IEnumerator HitTime()
	{
		isHitted = true;
		isInvincible = true;
		yield return new WaitForSeconds(0.1f);
		isHitted = false;
		isInvincible = false;
	}

	IEnumerator DestroyEnemy()
	{
		CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
		capsule.size = new Vector2(1f, 0.25f);
		capsule.offset = new Vector2(0f, -0.8f);
		capsule.direction = CapsuleDirection2D.Horizontal;
		if (crawler)
		{
			AkSoundEngine.PostEvent("Crawler_Explode", enemy);
		}
		else if (!crawler)
		{
			AkSoundEngine.PostEvent("Infected_Death", enemy);
		}

		yield return new WaitForSeconds(0.25f);
		rb.velocity = new Vector2(0, rb.velocity.y);
		//yield return new WaitForSeconds(3f);
		Destroy(gameObject);
	}

	public void Alert()
	{
		//Debug.Log("My dad is alerting me!");
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
		//Debug.Log(collision.gameObject.name);
    }
}

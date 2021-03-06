using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Bossman : MonoBehaviour
{

	public float life = 20;
	public float maxLife = 20;
	private bool isPlat;
	private bool isObstacle;
	private Transform fallCheck;
	private Transform wallCheck;
	public LayerMask turnLayerMask;
	private Rigidbody2D rb;
	public bool playerDetectable;
	private bool facingRight = true;
	public GameObject detectionCollider;
	public float detectionRange = 5f;
	private GameObject player;
	public GameObject projectile;
	public float shootCooldown = 1.1f;
	public float nextShot = 1.1f;
	public float timer;
	public float damageAmount = 1f;

	public float speed = 5f;
	private Animator animator;

	public bool isInvincible = false;
	private bool isHitted = false;
	private bool isStunned = false;
	public bool onAlert = false;

	public GameObject boss;

	public AK.Wwise.Event idle;

	private int grenadeReducedDamage;

	void Awake()
	{

		boss = GameObject.Find("Boss");

		fallCheck = transform.Find("FallCheck");
		wallCheck = transform.Find("WallCheck");
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");

	}

    private void OnEnable()
    {
		grenadeReducedDamage = 0;

	}

    // Update is called once per frame
    void FixedUpdate()
	{
		if (EventManager.EnemiesAlerted == true)
		{
			onAlert = true;
			animator.speed = 2f;
			shootCooldown = 0.5f;
			nextShot = 0.5f;


		}
		else
		{
			onAlert = false;
			animator.speed = 1f;
			shootCooldown = 1.1f;
			nextShot = 1.1f;


		}
		checkForPlayer();

		//Debug.Log("Timetime  " + timer + "\n" + "NextShot  " + nextShot + "\n" + "Shoot Cooldown " + shootCooldown);

		void PlayIdle()
        {
			idle.Post(boss);
        }

		if (life <= 0 && transform.GetComponent<Animator>().GetBool("BossDead")==false)
		{
			transform.GetComponent<Animator>().SetBool("BossDead", true);
			StartCoroutine(DestroyEnemy());
		}

		//the boss doesn't move so we don't need this
		//isPlat = Physics2D.OverlapCircle(fallCheck.position, .2f, 1 << LayerMask.NameToLayer("Default"));
		//isObstacle = Physics2D.OverlapCircle(wallCheck.position, .2f, turnLayerMask);


		//if (!isHitted && life > 0 && Mathf.Abs(rb.velocity.y) < 0.5f)
		//{
		//	if (isPlat && !isObstacle && !isHitted)
		//	{
		//		if (facingRight)
		//		{
		//			rb.velocity = new Vector2(-speed, rb.velocity.y);
		//		}
		//		else
		//		{
		//			rb.velocity = new Vector2(speed, rb.velocity.y);
		//		}
		//	}
		//	else
		//	{
		//		Flip();
		//	}
		//}
	}

	void checkForPlayer()
	{
		if (Vector3.Distance(player.transform.position, transform.position) <= detectionRange)
		{
			playerDetectable = true;
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 0);

			if (player.transform.position.x > transform.position.x && facingRight)
				//	Flip();
				;
				if (player.transform.position.x < transform.position.x && !facingRight)
					//Flip(); 
					;

		}
		else
		{
			playerDetectable = false;

		}


		if (!playerDetectable)
		{
			//animator.SetBool("IsAttacking", false);

			//casue a bunch of warnings


		}
	}

	void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void ApplyDamage(float damage)
	{
		if (!isInvincible)
		{
			float direction = damage / Mathf.Abs(damage);
			damage = Mathf.Abs(damage);
			transform.GetComponent<Animator>().SetBool("Hit", true);
			Debug.Log("I hurt! I took damage.");
			AkSoundEngine.PostEvent("Xeno_Damaged", boss);
			life -= damage;
			rb.velocity = Vector2.zero;
			rb.AddForce(new Vector2(direction * 500f, 100f));
			StartCoroutine(HitTime());
		}
	}

	public void Stun(float StunDuration) {

		ApplyDamage(20f/4);

		//StartCoroutine(StunTime(StunDuration));
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

		//GameObject FrozenCorpse = Instantiate(FrozenEnemy, transform.position, Quaternion.identity);
		//Physics2D.IgnoreCollision(FrozenCorpse.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());

		//Destroy(gameObject);




		if (isInvincible == false) {

			grenadeReducedDamage++;
			if (grenadeReducedDamage == 1) ApplyDamage(75);
			if (grenadeReducedDamage == 2) ApplyDamage(60);
			if (grenadeReducedDamage == 3) ApplyDamage(50);
			if (grenadeReducedDamage == 4) ApplyDamage(25);
			if (grenadeReducedDamage >= 5) ApplyDamage(10);

			StartCoroutine(FrozenTime());}
	}


	IEnumerator FrozenTime()
	{
		isHitted = true;
		isInvincible = true;
		transform.GetComponent<Animator>().SetBool("Frozen", true);

		yield return new WaitForSeconds(1f);
		isHitted = false;
		isInvincible = false;

		transform.GetComponent<Animator>().SetBool("Frozen", false);
	}


	void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && life > 0)
		{
			collision.gameObject.GetComponent<CharacterController2D>().ApplyDamage(-damageAmount);
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
		yield return new WaitForSeconds(0.25f);
		rb.velocity = new Vector2(0, rb.velocity.y);
		yield return new WaitForSeconds(3f);
		Destroy(gameObject);


	}

	public void Shoot()
    {

		animator.SetBool("IsAttacking", true);

		//Debug.Log("I spit");
		if (facingRight)
		{
			Vector2 direction = new Vector2(-1, 0);
			GameObject spitBullet = Instantiate(projectile, transform.position + new Vector3(transform.localScale.x - 1.9f, 0.5f), Quaternion.identity);
			spitBullet.GetComponent<EnemyProjectile>().direction = direction;
			spitBullet.name = "SpitBullet";
		}
		else if (!facingRight)
        {
			Vector2 direction = new Vector2(1, 0);
			GameObject spitBullet = Instantiate(projectile, transform.position + new Vector3(transform.localScale.x + 1.9f, 0.5f), Quaternion.identity);
			spitBullet.GetComponent<EnemyProjectile>().direction = direction;
			spitBullet.name = "SpitBullet";
		}
		timer = 0f;

	}
		
	
}

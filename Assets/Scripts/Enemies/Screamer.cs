using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Screamer : MonoBehaviour {

	public float life = 10;
	private bool isPlat;
	private bool isObstacle;
	private Transform fallCheck;
	private Transform wallCheck;
	public LayerMask turnLayerMask;
	private Rigidbody2D rb;
	public bool playerDetectable;
	private bool facingRight = true;
	public GameObject detectionCollider;
	public float detectionRange = 1f;
	private float startDetectionRange;
	public float extendedDetectionRange = 1f;

	private GameObject player;
	public float damageAmount = 1f;
	public bool onAlert;
	
	public float speed = 5f;
	private Animator animator;

	public bool isInvincible = false;
	private bool isHitted = false;
	private bool isStunned = false;

	public UnityEvent IsScreamingEvent;
	public UnityEvent IsQuietEvent;

	private GameObject FrozenEnemy;

	void Awake () {
		startDetectionRange = detectionRange;
		FrozenEnemy = (GameObject)Resources.Load("prefabs/FrozenScremer", typeof(GameObject));


		fallCheck = transform.Find("FallCheck");
		wallCheck = transform.Find("WallCheck");
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		if (IsScreamingEvent == null)
        {
			IsScreamingEvent = new UnityEvent();
        }
		if (IsQuietEvent == null)
        {
			IsQuietEvent = new UnityEvent();
        }



	}

	// Update is called once per frame
	void Update () {
		checkForPlayer();
		onAlert = EventManager.EnemiesAlerted;
		
		if (onAlert)
		{
			animator.SetBool("HasNoticed", true);
		}

        else if (!onAlert)
        {
			animator.SetBool("HasNoticed", false);
		}
		
		
		
		if (life <= 0) {
			transform.GetComponent<Animator>().SetBool("IsDead", true);
			StartCoroutine(DestroyEnemy());
		}

		isPlat = Physics2D.OverlapCircle(fallCheck.position, .2f, 1 << LayerMask.NameToLayer("Default"));
		isObstacle = Physics2D.OverlapCircle(wallCheck.position, .2f, turnLayerMask);


		//if (!isHitted && life > 0 && Mathf.Abs(rb.velocity.y) < 0.5f && !isStunned)
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
		if (Vector3.Distance(player.transform.position, transform.position) <= detectionRange && !isStunned)
		{
			playerDetectable = true;



			EventManager.Alert();
			EventManager.AddDangerEveryTick();
			//IsScreamingEvent.Invoke();
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 0);

			detectionRange = extendedDetectionRange;

			if (player.transform.position.x > transform.position.x && facingRight)
				Flip();
			if (player.transform.position.x < transform.position.x && !facingRight)
				Flip();



		}
		else {

			detectionRange = startDetectionRange;

		}


		if (Vector3.Distance(player.transform.position, transform.position) > detectionRange && !isStunned)
		{
			playerDetectable = false;

			EventManager.Calm();
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


	public void Stun(float StunDuration)
	{
		StartCoroutine(StunTime(StunDuration));
		
		EventManager.Calm();
		playerDetectable = false;
		//IsQuietEvent.Invoke();

	}
	IEnumerator StunTime(float _StunDuration)
	{
		isStunned = true;
		transform.GetComponent<Animator>().SetBool("IsStunned", true);
		yield return new WaitForSeconds(_StunDuration);
		isStunned = false;
		transform.GetComponent<Animator>().SetBool("IsStunned", false);
	}


	public void Frozen() {

		GameObject FrozenCorpse = Instantiate(FrozenEnemy, transform.position, Quaternion.identity);
		Physics2D.IgnoreCollision(FrozenCorpse.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());

		Destroy(gameObject);
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
}

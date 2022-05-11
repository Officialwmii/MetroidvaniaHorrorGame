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
	public float detectionRange = 2f;
	private GameObject player;
	
	public float speed = 5f;
	private Animator animator;

	public bool isInvincible = false;
	private bool isHitted = false;
	public UnityEvent IsScreamingEvent;
	public UnityEvent IsQuietEvent;

	void Awake () {
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
	void FixedUpdate () {
		checkForPlayer();
		if (life <= 0) {
			transform.GetComponent<Animator>().SetBool("IsDead", true);
			StartCoroutine(DestroyEnemy());
		}

		isPlat = Physics2D.OverlapCircle(fallCheck.position, .2f, 1 << LayerMask.NameToLayer("Default"));
		isObstacle = Physics2D.OverlapCircle(wallCheck.position, .2f, turnLayerMask);		


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
			IsScreamingEvent.Invoke();
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 0);

			if (player.transform.position.x > transform.position.x && facingRight)
				Flip();
			if (player.transform.position.x < transform.position.x && !facingRight)
				Flip();
		}
        else
        {
			playerDetectable = false;
			IsQuietEvent.Invoke();
        }


		if (playerDetectable)
		{
			animator.SetBool("HasNoticed", true);
		}
        else
        {
			animator.SetBool("HasNoticed", false);
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

	void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && life > 0)
		{
			collision.gameObject.GetComponent<CharacterController2D>().ApplyDamage(2f, transform.position);
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
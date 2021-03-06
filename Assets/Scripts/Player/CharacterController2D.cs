using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{ 
	[SerializeField] private float m_JumpForce = 10f;                        // Amount of force added when the player jumps.
	[SerializeField] private float m_JetpackForce = 90f;					// The Jetpack force
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_WallCheck;                             //Posicion que controla si el personaje toca una pared
	[SerializeField] private float jumpAcceleration = 5f;
	private float jumpForce;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	static public bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;
	private float limitFallSpeed = 25f; // Limit fall speed
	[SerializeField]public float gravityScale = 1f;

	public bool canDoubleJump = false; //If player can double jump
	[SerializeField] private float m_DashForce = 25f;
	private bool canDash = true;
	private bool isDashing = false; //If player is dashing
	private bool MultiDirectionalDashing = true;

	private bool m_IsWall = false; //If there is a wall in front of the player
	private bool isWallSliding = false; //If player is sliding in a wall
	private bool oldWallSlidding = false; //If player is sliding in a wall in the previous frame
	private float prevVelocityX = 0f;
	private bool canCheck = false; //For check if player is wallsliding

	public bool WallSlideAbilityActive = false;
	public bool DoubleJumpAbilityActive = false;

	public float life = 10f; //Life of the player
	private float startLife = 0;
	public bool invincible = false; //If player can die
	private bool canMove = true; //If player can move

	private Animator animator;
	public ParticleSystem particleJumpUp; //Trail particles
	public ParticleSystem particleJumpDown; //Explosion particles
	public GameObject particleBlood;
	public GameObject particleBloodFauntain;
	public GameObject head;
	public GameObject particleDash;
	public GameObject particleJetpack;
	public GameObject DashTrail;
	public GameObject JetpackTrail;
	public GameObject player;

	private float jumpWallStartX = 0;
	private float jumpWallDistX = 0; //Distance between player and wall
	private bool limitVelOnWallJump = false; //For limit wall jump distance with low fps

	private GameObject StartPosition;

	[Header("Events")]
	[Space]

	public UnityEvent OnFallEvent;
	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public SetParameter gameState;

	private AudioClip SFXDenial;

	private void Awake()
	{
		startLife = life;

		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		if (OnFallEvent == null)
			OnFallEvent = new UnityEvent();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
		SFXDenial = GetComponent<Attack>().SFXDenial;

	}


	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				m_Grounded = true;
				if (!wasGrounded )
				{
				//if (m_Rigidbody2D.velocity.y <9)
					{ OnLandEvent.Invoke();
					//Debug.Log(m_Rigidbody2D.velocity.y);
						}
					
					
					if (!m_IsWall && !isDashing) 
						particleJumpDown.Play();
					//canDoubleJump = true;
					if (m_Rigidbody2D.velocity.y < 0f)
						limitVelOnWallJump = false;
				}
		}

		m_IsWall = false;

		if (!m_Grounded)
		{
			OnFallEvent.Invoke();
			Collider2D[] collidersWall = Physics2D.OverlapCircleAll(m_WallCheck.position, k_GroundedRadius, m_WhatIsGround);
			for (int i = 0; i < collidersWall.Length; i++)
			{
				if (collidersWall[i].gameObject != null)
				{
					isDashing = false;
					m_IsWall = true;
				}
			}
			prevVelocityX = m_Rigidbody2D.velocity.x;
		}

		if (limitVelOnWallJump&& WallSlideAbilityActive)
		{
			if (m_Rigidbody2D.velocity.y < -0.5f)
				limitVelOnWallJump = false;
			jumpWallDistX = (jumpWallStartX - transform.position.x) * transform.localScale.x;
			if (jumpWallDistX < -0.5f && jumpWallDistX > -1f) 
			{
				canMove = true;
			}
			else if (jumpWallDistX < -1f && jumpWallDistX >= -2f) 
			{
				canMove = true;
				m_Rigidbody2D.velocity = new Vector2(10f * transform.localScale.x, m_Rigidbody2D.velocity.y);
			}
			else if (jumpWallDistX < -2f) 
			{
				limitVelOnWallJump = false;
				m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
			}
			else if (jumpWallDistX > 0) 
			{
				limitVelOnWallJump = false;
				m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
			}
		}
	}


	public void Move(float move, bool jump, bool dash, bool holdJump, float gravity, bool holdJetpack)
	{
		// Variable jump height is achievemed by lowering the player's gravity for a period of time. 
		if (EventManager.inLowGravityZone) { m_Rigidbody2D.gravityScale = gravity / 4; }
		else {m_Rigidbody2D.gravityScale = gravity; }
		//Debug.Log(gravity);
		if (canMove) {

			if (dash && (EventManager.HasJetpack == false || EventManager.canUseDash == false )) AudioSource.PlayClipAtPoint(SFXDenial, gameObject.transform.position, 0.1f);

			if (dash && canDash && !isWallSliding && EventManager.canUseDash && EventManager.HasJetpack)
			{
				//m_Rigidbody2D.AddForce(new Vector2(transform.localScale.x * m_DashForce, 0f));
				StartCoroutine(DashCooldown());
				//particleDash.GetComponent<ParticleSystem>().Play();
			}
			// If crouching, check to see if the character can stand up
			if (isDashing)
			{

				if (MultiDirectionalDashing) {

					if (Mathf.Round(Input.GetAxisRaw("Horizontal")) == 0 && Mathf.Round(Input.GetAxisRaw("Vertical")) == 0) {
						m_Rigidbody2D.velocity = new Vector2(transform.localScale.x * m_DashForce, 0);


					}
					else {

						m_Rigidbody2D.velocity = new Vector2(Mathf.Round(Input.GetAxisRaw("Horizontal")) * 
							m_DashForce, Mathf.Round(Input.GetAxisRaw("Vertical")) * m_DashForce*0.75f);

					}

				}
				else {
					m_Rigidbody2D.velocity = new Vector2(transform.localScale.x * m_DashForce, 0);
				}

			}
			//only control the player if grounded or airControl is turned on
			else if (m_Grounded || m_AirControl)
			{
				if (m_Rigidbody2D.velocity.y < -limitFallSpeed)
					m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -limitFallSpeed);
				// Move the character by finding the target velocity
				Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
				// And then smoothing it out and applying it to the character
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

				// If the input is moving the player right and the player is facing left...
				if (move > 0 && !m_FacingRight && !isWallSliding)
				{
					// ... flip the player.
					Flip();
				}
				// Otherwise if the input is moving the player left and the player is facing right...
				else if (move < 0 && m_FacingRight && !isWallSliding)
				{
					// ... flip the player.
					Flip();
				}
			}
			// If the player should jump...
			if (m_Grounded && jump )
			{
				// Add a vertical force to the player.	

				animator.SetBool("IsJumping", true);
				animator.SetBool("JumpUp", true);
				m_Grounded = false;
				//Debug.Log(jumpForce);

				m_Rigidbody2D.AddForce(new Vector2(0, m_JumpForce), ForceMode2D.Impulse);


				if (DoubleJumpAbilityActive) canDoubleJump = true;
				particleJumpDown.Play();
				particleJumpUp.Play();
			}

			if (holdJetpack && EventManager.canUseJetpack == false) { AudioSource.PlayClipAtPoint(SFXDenial, gameObject.transform.position, 0.1f); }

			if (EventManager.HasDoubleJetpack && holdJetpack && EventManager.canUseJetpack){

				m_Rigidbody2D.AddForce(new Vector2(0, m_JetpackForce));
				EventManager.UseJetpack();
				//particleJetpack.GetComponent<ParticleSystem>().Play();
				JetpackTrail.GetComponent<TrailRenderer>().emitting = true;
				//Debug.Log("Jetpack speed: "+m_Rigidbody2D.velocity.y);
				if (m_Rigidbody2D.velocity.y >= 40) { m_Rigidbody2D.velocity = m_Rigidbody2D.velocity.normalized * 40; }

			}

			if (holdJetpack == false || EventManager.canUseJetpack == false){ JetpackTrail.GetComponent<TrailRenderer>().emitting = false;}

			else if (!m_Grounded && jump && canDoubleJump && !isWallSliding)
			{
				canDoubleJump = false;
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce / 1.2f));
				animator.SetBool("IsDoubleJumping", true);
			}

			else if (m_IsWall && !m_Grounded && WallSlideAbilityActive)
			{
				if (!oldWallSlidding && m_Rigidbody2D.velocity.y < 0 || isDashing)
				{
					isWallSliding = true;
					m_WallCheck.localPosition = new Vector3(-m_WallCheck.localPosition.x, m_WallCheck.localPosition.y, 0);
					Flip();
					StartCoroutine(WaitToCheck(0.1f));
					if (DoubleJumpAbilityActive) canDoubleJump = true;
					animator.SetBool("IsWallSliding", true);
				}
				isDashing = false;

				if (isWallSliding)
				{
					if (move * transform.localScale.x > 0.1f)
					{
						StartCoroutine(WaitToEndSliding());
					}
					else
					{
						oldWallSlidding = true;
						m_Rigidbody2D.velocity = new Vector2(-transform.localScale.x * 2, -5);
					}
				}

				if (jump && isWallSliding)
				{
					animator.SetBool("IsJumping", true);
					animator.SetBool("JumpUp", true);
					m_Rigidbody2D.velocity = new Vector2(0f, 0f);
					m_Rigidbody2D.AddForce(new Vector2(transform.localScale.x * m_JumpForce * 1.2f, m_JumpForce));
					jumpWallStartX = transform.position.x;
					limitVelOnWallJump = true;
					if (DoubleJumpAbilityActive) canDoubleJump = true;
					isWallSliding = false;
					animator.SetBool("IsWallSliding", false);
					oldWallSlidding = false;
					m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
					canMove = false;
				}
				else if (dash && canDash)
				{
					isWallSliding = false;
					animator.SetBool("IsWallSliding", false);
					oldWallSlidding = false;
					m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
					if (DoubleJumpAbilityActive) canDoubleJump = true;
					StartCoroutine(DashCooldown());
				}
			}
			else if (isWallSliding && !m_IsWall && canCheck)
			{
				isWallSliding = false;
				animator.SetBool("IsWallSliding", false);
				oldWallSlidding = false;
				m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
				if (DoubleJumpAbilityActive) canDoubleJump = true;
			}
		}

		//if (m_Grounded) { m_Rigidbody2D.sharedMaterial.friction = 1f; }
		//else { m_Rigidbody2D.sharedMaterial.friction = 0f; }

	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void ApplyDamage(float damage) 
	{
		if (!invincible)
		{
			animator.SetBool("Hit", true);
			//Debug.Log("My health! " + life);
			life += damage;
			m_Rigidbody2D.velocity = Vector2.zero;
			GameObject bloodeffect = Instantiate(particleBlood, transform.position, Quaternion.identity);

			
			EventManager.SetHP(life);
			AkSoundEngine.PostEvent("Player_Hurt", player);

			if (life <= 0)
			{
				StartCoroutine(WaitToDead());
			}
			else
			{
				StartCoroutine(Stun(0.25f));
				StartCoroutine(MakeInvincible(1f));
			}
		}
	}

	IEnumerator DashCooldown()
	{
		animator.SetBool("IsDashing", true);
		EventManager.UseDash();
		isDashing = true;
		canDash = false;
		DashTrail.GetComponent<TrailRenderer>().emitting = true;
		AkSoundEngine.PostEvent("Jetpack_Dash", player);

		yield return new WaitForSeconds(0.1f);
		isDashing = false;
		yield return new WaitForSeconds(0.25f);
		DashTrail.GetComponent<TrailRenderer>().emitting = false;
		//Debug.Log("turn off dash trail");
		yield return new WaitForSeconds(0.25f);
		canDash = true;

	}

	IEnumerator Stun(float time) 
	{
		canMove = false;
		yield return new WaitForSeconds(time);
		canMove = true;
	}
	IEnumerator MakeInvincible(float time) 
	{
		invincible = true;
		yield return new WaitForSeconds(time);
		invincible = false;
	}
	IEnumerator WaitToMove(float time)
	{
		canMove = false;
		yield return new WaitForSeconds(time);
		canMove = true;
	}

	IEnumerator WaitToCheck(float time)
	{
		canCheck = false;
		yield return new WaitForSeconds(time);
		canCheck = true;
	}

	IEnumerator WaitToEndSliding()
	{
		yield return new WaitForSeconds(0.1f);
		if (DoubleJumpAbilityActive) canDoubleJump = true;
		isWallSliding = false;
		animator.SetBool("IsWallSliding", false);
		oldWallSlidding = false;
		m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
	}

	IEnumerator WaitToDead()
	{
		if (invincible == false)
		{
			GameObject bloodeffect2 = Instantiate(particleBloodFauntain, transform.position, Quaternion.identity);
			GameObject Head = Instantiate(head, transform.position, Quaternion.identity);

		


		animator.SetBool("IsDead", true);
		AkSoundEngine.PostEvent("Player_Death", head);
		gameState.alive = false;
		canMove = false;
		invincible = true;
		GetComponent<Attack>().enabled = false;
		

		yield return new WaitForSeconds(0.4f);
		m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
		
		yield return new WaitForSeconds(1.1f);

		canMove = true;
		invincible = false;
		GetComponent<Attack>().enabled = true;
		//Destroy(Head);


		EventManager.OnRespawning();
		}

		//SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}

	public void ResetHealth() {

		life = startLife;
		EventManager.SetHP(life);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool dash = false;
	bool holdJump = false;
	public float variableGravity = 2f;
	float defaultGravity = 5f;
	
	private bool startTimer = false;
	[SerializeField]private float jumpTimer = 0.5f;
	private float timer;

    private bool holdJetpack = false;

	public GameObject player;

    //bool dashAxis = false;

    // Update is called once per frame
    private void Awake()
    {
		timer = jumpTimer;
    }

	void Update()
	{
		//Debug.Log(timer);
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		//JUMP

		if (Input.GetButtonUp("Jump"))
		{
			holdJump = false;
		}

		if (Input.GetButton("Jump"))
		{
			holdJump = true;
			startTimer = true;
		}

		if (Input.GetButtonDown("Jump"))
		{
			holdJump = true;
			startTimer = true;
			timer = jumpTimer;

		}



		if (startTimer)
		{
			timer -= Time.deltaTime;
			if (timer <= 0)
			{
				holdJump = false;
				//timer = jumpTimer;
				startTimer = false;
			}

		}


		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			//holdJump = true;
			//startTimer = true;
			AkSoundEngine.PostEvent("Player_Jump", player);
		}
		/*	if (Input.GetButton("Jump"))
			{
				if (holdJump == false)
				{
					jump = true;

					holdJump = true;
				}

			}
		*/

		if (Input.GetButtonUp("Jump"))
		{
			//holdJump = false;
			jump = false;
			//timer = jumpTimer;
		}


		//JETPACK
		if (Input.GetButtonDown("Jetpack"))
		{
			holdJetpack = true;
		}
		if (Input.GetButtonUp("Jetpack"))
		{
			holdJetpack = false;
		}

		//DASH
		if (Input.GetButtonDown("Dash"))
		{
			dash = true;
		}

		/*if (Input.GetAxisRaw("Dash") == 1 || Input.GetAxisRaw("Dash") == -1) //RT in Unity 2017 = -1, RT in Unity 2019 = 1
		{
			if (dashAxis == false)
			{
				dashAxis = true;
				dash = true;
			}
		}
		else
		{
			dashAxis = false;
		}
		*/


		// Move our character






	}

	public void OnFall()
	{
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate() {



		if (holdJump) //Lower gravity to achieve variable jump height.
		{
			controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash, holdJump, variableGravity, holdJetpack);
			jump = false;
			dash = false;
		}
		if (!holdJump) //Default jump height.
		{
			controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash, holdJump, defaultGravity, holdJetpack);
			jump = false;
			dash = false;
		}


	}

}

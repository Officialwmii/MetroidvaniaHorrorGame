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
	bool stoppedJump = false;
	public float variableGravity = 2f;
	float defaultGravity = 5f;



	

	//bool dashAxis = false;
	
	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump") && CharacterController2D.m_Grounded) //Remove the grounded part for jetpack
		{

			jump = true;
			holdJump = true;
			stoppedJump = false;

			


		}

		if (Input.GetButtonUp("Jump"))
        {

			holdJump = false;
			jump = false;
			stoppedJump = true;



        }

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

	}

	public void OnFall()
	{
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate ()
	{
		// Move our character
		

		if (holdJump)
        {
			controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash, holdJump, stoppedJump, variableGravity);
			jump = false;
			dash = false;
		}
        if (!holdJump)
        {
			controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash, holdJump, stoppedJump, defaultGravity);
			jump = false;
			dash = false;
		}
	}
}

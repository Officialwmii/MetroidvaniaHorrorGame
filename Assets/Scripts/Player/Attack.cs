using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	public float dmgValue = 4;
	public GameObject throwableObject;
	public Transform attackCheck;
	private Rigidbody2D m_Rigidbody2D;
	public Animator animator;
	public bool canAttack = true;
	public bool isTimeToCheck = false;

	public GameObject cam;
	public GameObject Grenade;
	public GameObject player;
	public AudioClip SFXDenial;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
    {
		Grenade = (GameObject)Resources.Load("prefabs/GrenadeProjectile", typeof(GameObject));

	}

	// Update is called once per frame
	void Update()
    {
		/*if (Input.GetButtonDown("EMP") && canAttack)
		{
			canAttack = false;
			animator.SetBool("IsAttacking", true);
			StartCoroutine(AttackCooldown());
		}*/

		if (Input.GetButtonDown("Stun") && (EventManager.canUseStun == false || canAttack == false)) {
			AudioSource.PlayClipAtPoint(SFXDenial, gameObject.transform.position, 0.1f);
		}

			if (Input.GetButtonDown("Stun") && EventManager.canUseStun == true && canAttack)
		{

			animator.SetBool("IsShooting", true);
			AkSoundEngine.PostEvent("Stun_Gun_Fire", player);
			EventManager.StartCooldown();
			canAttack = false;
			StartCoroutine(AttackCooldown());
		}

		if (Input.GetButtonDown("Grenade") && EventManager.grenades <= 0) {

			AudioSource.PlayClipAtPoint(SFXDenial, gameObject.transform.position,0.1f);
		}

		if (Input.GetButton("Grenade") && EventManager.grenades>0)
		{
			EventManager.UpdateGrenadeCountdown();
		}

		if (Input.GetButtonUp("Grenade"))
		{
			EventManager.GrenadeCountdown=0;
			EventManager.CanThrowGrenade = true;
			EventManager.GrenadeCircle.SetActive(false);
		}

	}

	public void ThrowGrenade()
    {
		EventManager.GrenadeCountdown = 0;
		EventManager.UseGrenades();
		AkSoundEngine.PostEvent("Grenade_Freeze", Grenade);
		animator.SetBool("IsThrowingGrenade", true);

		GameObject Grenade2 = Instantiate(Grenade, transform.position + new Vector3(transform.localScale.x * 0.5f, -0.2f), Quaternion.identity) as GameObject;
		Vector2 direction = new Vector2(transform.localScale.x, 0);
		Grenade2.GetComponent<GrenadeProjectile>().direction = direction;
	}

	IEnumerator AttackCooldown()
	{
		AkSoundEngine.PostEvent("Stun_Gun_Cooldown", player);
		yield return new WaitForSeconds(0.1f);
		canAttack = true;
		GameObject throwableWeapon = Instantiate(throwableObject, transform.position + new Vector3(transform.localScale.x * 0.5f, -0.2f), Quaternion.identity) as GameObject;
		Vector2 direction = new Vector2(transform.localScale.x, 0);
		throwableWeapon.GetComponent<ThrowableWeapon>().direction = direction;
		throwableWeapon.name = "ThrowableWeapon";



	}

	public void DoDashDamage()
	{
		dmgValue = Mathf.Abs(dmgValue);
		Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, 0.9f);
		for (int i = 0; i < collidersEnemies.Length; i++)
		{
			if (collidersEnemies[i].gameObject.tag == "Enemy")
			{
				if (collidersEnemies[i].transform.position.x - transform.position.x < 0)
				{
					dmgValue = -dmgValue;
				}
				collidersEnemies[i].gameObject.SendMessage("ApplyDamage", dmgValue);
				cam.GetComponent<CameraFollow>().ShakeCamera();
			}
		}
	}
}

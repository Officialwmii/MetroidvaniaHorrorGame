using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    private GameObject GrenadeArea;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        GrenadeArea = (GameObject)Resources.Load("prefabs/GrenadeArea", typeof(GameObject));

    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * 5* direction, 0f);
        GetComponent<Rigidbody2D>().velocity = direction * 15;
        GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, transform.localScale.y * -15);

    }

    void OnCollisionEnter2D(Collision2D collision)
	{

	 if (collision.gameObject.tag != "Player")
		{

           // Debug.Log("Spawn area");
            GameObject GrenadeArea2 = Instantiate(GrenadeArea, transform.position, Quaternion.identity);
           // Physics2D.IgnoreCollision(FrozenCorpse.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());

            Destroy(gameObject);
        }
	}

}

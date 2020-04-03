using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpinnerBehaviour : MonoBehaviour {

	bool attack = false;
	Vector3 dir;

	Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (attack)
		{
			AttackState();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			attack = true;
			if (this.transform.position.x > other.transform.position.x)
			{
				dir = Vector2.left;
			}
			else dir = Vector2.right;
		}
	}

	void AttackState()
	{
		//SPIN CAP AL JUGADOR
		rb2D.AddForce(dir * 2, ForceMode2D.Impulse);
		if(rb2D.velocity.x >= 3)
		{
			rb2D.velocity =new Vector2(9,0);
		}
		else if(rb2D.velocity.x <= -3)
		{
			rb2D.velocity = new Vector2(-9, 0);
		}

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider.CompareTag("Wall") || other.collider.CompareTag("Box") || other.collider.CompareTag("Player"))
		{
			dir.x = -dir.x;
			print("flip");
		}
	}

	

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour {

	GameObject particleBurst;
	GameObject coin;

	public float coinBurst;

	public bool needsCoins;
	
	public int minCoins;
	public int maxCoins;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		print("trigger");
		if (other.CompareTag("PlayerShotExplosion"))
		{
			particleBurst = Instantiate((GameObject)Resources.Load("Particles/BoxBurst"), this.transform.position, Quaternion.identity);
			if (needsCoins)
			{
				int rand = Random.Range(minCoins, maxCoins);
				for (int i = 0; i <= rand; i++)
				{
					coin = Instantiate((GameObject)Resources.Load("EnemyCoin"), this.transform.position, Quaternion.identity);
					coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0f, 1f), Random.Range(0.5f, 1.5f)) * coinBurst, ForceMode2D.Impulse);
					
				}
				Destroy(this.gameObject);
			}
		}
	}
}

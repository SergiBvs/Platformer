using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour {

	private GameObject particleBurst;
	private GameObject coin;

    public bool breakable = true;

    public bool needsCoins;
    public float coinBurst;
	
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
		if (other.CompareTag("PlayerShotExplosion"))
		{
            DestroyBox();
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("EnemyHead"))
        {
            collision.gameObject.GetComponent<EnemyParentScript>().DestroyParent();
        }
        else if (collision.collider.CompareTag("Player"))
        {
            if (breakable)
            {
                if (collision.collider.GetComponent<Player>().m_IsDashing)
                {
                    DestroyBox();
                }
            }
        }
    }

    public void DestroyBox()
    {
        particleBurst = Instantiate((GameObject)Resources.Load("Particles/BoxBurst"), this.transform.position, Quaternion.identity);
        if (needsCoins)
        {
            int rand = Random.Range(minCoins, maxCoins);
            for (int i = 0; i <= rand; i++)
            {
                coin = Instantiate((GameObject)Resources.Load("EnemyCoin"), this.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 90)));
                coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0.5f, 1f)) * coinBurst, ForceMode2D.Impulse);
                //float randSize = Random.Range(1, 2.5f);
               // coin.transform.localScale = new Vector2(randSize, randSize);

            }
            Destroy(this.gameObject);
        }
    }
}

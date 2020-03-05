using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {


    GameObject coin;
    public float coinBurst;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerShotExplosion"))
        {
            BasicEnemyDeath();
        }
    }

    public void BasicEnemyDeath()
    {
        int rand = Random.Range(1, 5);
        for (int i = 0; i < rand; i++)
        {
            coin = Instantiate((GameObject)Resources.Load("EnemyCoin"), this.transform.position, Quaternion.identity);
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0f, 1f), Random.Range(0.5f, 1.5f)) * coinBurst, ForceMode2D.Impulse);
            Destroy(this.gameObject);
        }
    }
}

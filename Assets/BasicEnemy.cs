using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {


    GameObject coin;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Trigger");
        if (collision.CompareTag("PlayerShotExplosion"))
        {
            coin = Instantiate((GameObject)Resources.Load("EnemyCoin"), this.transform.position, Quaternion.identity);
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0f, 1f), Random.Range(0.5f, 1.5f)), ForceMode2D.Impulse);
            Destroy(this.gameObject);
        }
    }
}

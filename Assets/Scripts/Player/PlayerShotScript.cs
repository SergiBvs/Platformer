using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotScript : MonoBehaviour {

   	public GameObject explosion;

	// Use this for initialization
	void Start () {
		StartCoroutine(ShotExplode());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator ShotExplode()
	{
		yield return new WaitForSeconds(1.5f);
        Explode();
	}

    void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Box"))
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BasicEnemy"))
        {
            print("explode");
            Explode();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotScript : MonoBehaviour {

   	public GameObject explosion;

    private SoundManager m_GrenadeExplosion;

	// Use this for initialization
	void Start ()
    {
        m_GrenadeExplosion = GameObject.FindGameObjectWithTag("GrenadeExplosion").GetComponent<SoundManager>();
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
        m_GrenadeExplosion.m_AS.clip = m_GrenadeExplosion.m_GrenadeExplosion;
        m_GrenadeExplosion.m_AS.Play();

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

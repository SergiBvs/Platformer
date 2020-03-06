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
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}
}

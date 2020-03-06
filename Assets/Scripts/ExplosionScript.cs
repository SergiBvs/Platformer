using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(ExplosionTime());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator ExplosionTime()
	{
		yield return new WaitForSeconds(0.5f);
		Destroy(this.gameObject);
	}
}

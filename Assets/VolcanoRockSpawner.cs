using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoRockSpawner : MonoBehaviour {

	float spawnCD = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		spawnCD -= Time.deltaTime;
		if(spawnCD <= 0)
		{
			spawnCD = Random.Range(1f, 3.5f);
			Instantiate(Resources.Load("VolcanoRock"), new Vector2(transform.position.x + Random.Range(-7f, 7f), transform.position.y), Quaternion.identity);
		}
	
	}
}

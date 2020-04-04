using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoRockBehaviour : MonoBehaviour {

	public float speed = 3;

	// Use this for initialization
	void Start () {
		speed = Random.Range(4f, 7f);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += Vector3.down * Time.deltaTime * speed;
		if (this.transform.position.y < Camera.main.transform.position.y - 7)
		{
			Destroy(this.gameObject);
		}
	}
}

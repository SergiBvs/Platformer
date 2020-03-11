using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRemove : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

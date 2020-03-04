using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotScript : MonoBehaviour {

    public float dir;
    private Player playerScript;

    public float shotSpeed;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dir = playerScript.m_lastDirection;

        GetComponent<Rigidbody2D>().AddForce(new Vector2(dir*1.1f, 1) * shotSpeed, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsandPlatform : MonoBehaviour {

    float animspeed;

    // Use this for initialization
    void Start () {
        animspeed = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().speed;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().m_JumpForce = 7;
            collision.gameObject.GetComponent<Animator>().speed = animspeed / 2;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("OutofSoulsand");
            collision.gameObject.GetComponent<Player>().m_JumpForce = 10;
            collision.gameObject.GetComponent<Animator>().speed = animspeed;
        }
    }
}

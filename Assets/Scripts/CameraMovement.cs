using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	Transform m_playerPosition;
	public float yoffset;
	
	// Use this for initialization
	void Start () {
		m_playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(m_playerPosition.position.x,this.transform.position.y+yoffset,this.transform.position.z);
		//this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(m_playerPosition.position.x, m_playerPosition.position.y + yoffset, this.transform.position.z), 0.1f);
	}
}

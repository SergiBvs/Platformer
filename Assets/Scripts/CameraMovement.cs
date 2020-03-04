using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	Transform m_playerPosition;
	public float yoffset;

    float lerpSpeed;
	
	// Use this for initialization
	void Start () {
		m_playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {


        //this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(m_playerPosition.position.x, m_playerPosition.position.y + yoffset, this.transform.position.z), 0.1f);
        lerpSpeed = Vector2.Distance(transform.position, m_playerPosition.position);
        float lerpSpeedX = Mathf.Abs(transform.position.x - m_playerPosition.position.x);
        float lerpSpeedY = Mathf.Abs(transform.position.y - m_playerPosition.position.y);

        if(lerpSpeedX < 1f)
        {
            lerpSpeedX = 0;
        }


        if(transform.position.y < m_playerPosition.position.y - 3)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(m_playerPosition.position.x, m_playerPosition.position.y + 1, transform.position.z), lerpSpeed/10*Time.deltaTime);
        }
        else if (transform.position.y > m_playerPosition.position.y + 3)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(m_playerPosition.position.x, m_playerPosition.position.y -1, transform.position.z),lerpSpeed/10 * Time.deltaTime);
        }
        else
        {
            this.transform.position = Vector3.Lerp(transform.position, new Vector3(m_playerPosition.position.x, this.transform.position.y, this.transform.position.z),lerpSpeedX * Time.deltaTime);
        }

        print(lerpSpeedX + "   " + lerpSpeedY);
    }
}

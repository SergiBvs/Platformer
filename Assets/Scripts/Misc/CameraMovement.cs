using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	Transform m_playerPosition;
	public float yoffset;

    float DistanceX;
    float DistanceY;

    float dirX;
    float dirY;

    // Use this for initialization
    void Start () {
		m_playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.m_GameOver)
        {
            dirX = transform.position.x - m_playerPosition.position.x;
            dirY = transform.position.y - m_playerPosition.position.y;

            DistanceX = Mathf.Abs(dirX);
            DistanceY = Mathf.Abs(dirY);

            if (DistanceX > 2)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(m_playerPosition.position.x, transform.position.y, transform.position.z), DistanceX / 4 * Time.deltaTime);
            }

            if (DistanceY > 2.5)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(m_playerPosition.position.x, m_playerPosition.position.y, transform.position.z), DistanceY / 2.5f * Time.deltaTime);
            }


        }
    }
}

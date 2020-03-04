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

        dirX = transform.position.x - m_playerPosition.position.x;
        dirY = transform.position.y - m_playerPosition.position.y;

        DistanceX = Mathf.Abs(dirX);
        DistanceY = Mathf.Abs(dirY);

        //if(DistanceX < 1f)
        //{
        //    DistanceX = 0;
        //}else if (DistanceX > 2.5f)
        //{
        //    DistanceX = 2.5f;
        //}

        //if (lerpSpeedX < 1f)
        //{
        //    lerpSpeedX = 0;
        //}
        //else if (lerpSpeedX > 2.5f)
        //{
        //    lerpSpeedX = 2.5f;
        //}

        if (DistanceX > 3)
        {
            if (DistanceY > 3)
            {
                transform.position = Vector2.Lerp(transform.position, m_playerPosition.position, 1f * Time.deltaTime);
            }

            transform.position = Vector2.Lerp(transform.position, new Vector2(m_playerPosition.position.x, transform.position.y), 1f * Time.deltaTime);
        }


    }

    //    if (transform.position.y < m_playerPosition.position.y - 3)
    //    {
    //        transform.position = Vector3.Lerp(transform.position, new Vector3(m_playerPosition.position.x, m_playerPosition.position.y + 1, transform.position.z), lerpSpeedY*Time.deltaTime);
    //    }
    //    else if (transform.position.y > m_playerPosition.position.y + 3)
    //    {
    //        transform.position = Vector3.Lerp(transform.position, new Vector3(m_playerPosition.position.x, m_playerPosition.position.y -1, transform.position.z),lerpSpeedY * Time.deltaTime);
    //    }
    //    else
    //    {
    //        this.transform.position = Vector3.Lerp(transform.position, new Vector3(m_playerPosition.position.x, this.transform.position.y, this.transform.position.z),lerpSpeedX * Time.deltaTime);
    //    }

    //    print(lerpSpeedX + "   " + lerpSpeedY);
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {

    public int m_EnemyMovement;
    public int m_EnemySpeed;
    public Rigidbody2D m_EnemyRB2D;
	
	void Start ()
    {
        m_EnemyMovement = 1;
        m_EnemyRB2D = GameObject.FindGameObjectWithTag("BasicEnemy").GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {
		if(m_EnemyMovement == 1)
        {
            m_EnemyRB2D.AddForce(new Vector2(0.1f, 0) * m_EnemySpeed, ForceMode2D.Impulse);
        }
        else if(m_EnemyMovement == -1)
        {
            m_EnemyRB2D.AddForce(new Vector2(-0.1f, 0) * m_EnemySpeed, ForceMode2D.Impulse);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Wall"))
        {
            if(m_EnemyMovement == 1)
            {
                m_EnemyMovement = -1;
            }

            if(m_EnemyMovement == 1)
            {
                m_EnemyMovement = 1;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{

    public bool m_EnemyMovement;
    public int m_EnemySpeed;
    public Rigidbody2D m_EnemyRB2D;

    void Start()
    {
        m_EnemyMovement = true;
        m_EnemyRB2D = GameObject.FindGameObjectWithTag("BasicEnemy").GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }
}

   

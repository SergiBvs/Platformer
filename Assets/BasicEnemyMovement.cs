using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{

    public bool m_EnemyMovement;
    public int m_EnemySpeed;
    public Rigidbody2D m_EnemyRB2D;


    //---Moviment---
    public Transform[] pivots;
    private Transform currentTarget;

    private int targetInt = 0;
    int dir = 1;

    void Start()
    {

        currentTarget = pivots[0];
        m_EnemyMovement = true;
        m_EnemyRB2D = GameObject.FindGameObjectWithTag("BasicEnemy").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (m_EnemyMovement)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, currentTarget.position, 0.1f);

            if (this.transform.position == currentTarget.position)
            {
                if (targetInt == pivots.Length - 1)
                {
                    dir = -1;
                }
                else if (targetInt == 0)
                {
                    dir = 1;
                }

                targetInt += dir;
                currentTarget = pivots[targetInt];
            }
        }


    }

}
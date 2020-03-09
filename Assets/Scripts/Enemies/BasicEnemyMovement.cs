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
            this.transform.position = Vector3.MoveTowards(this.transform.position, currentTarget.position, m_EnemySpeed * Time.deltaTime);

            

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
                m_EnemyMovement = false;
                this.GetComponent<Animator>().SetTrigger("IDLE");
                StartCoroutine(WaitTime());
            }
        }
    }

    public void GoBack()
    {
        if (targetInt > 0 && dir == 1)
        {
            targetInt -= 1;
        }
        else if (targetInt > 0 && dir == -1)
        {
            targetInt += 1;
        }
        else if (targetInt == 0)
        {
            targetInt = 1;
        }
        currentTarget = pivots[targetInt];
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(3);
        m_EnemyMovement = true;
        this.GetComponent<Animator>().SetTrigger("WALK");
    }

}
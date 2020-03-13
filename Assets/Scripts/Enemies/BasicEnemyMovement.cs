using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{

    public bool m_EnemyMovement;
    public float m_EnemySpeed = 2;


    //---Moviment---
    public Transform[] pivots;
    private Transform currentTarget;

    private int targetInt = 0;
    int dir = 1;

    public bool m_ObjectFlipped;

    //public float EnemyLocalScaleY;

    void Start()
    {
        currentTarget = pivots[0];
        //m_EnemyMovement = true;
        //EnemyLocalScaleY = this.transform.localScale.y;
    }

    void Update()
    {
        if (m_EnemyMovement)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, currentTarget.position, m_EnemySpeed * Time.deltaTime);

            if(currentTarget.position.x < transform.position.x) // left
            {
                if(!m_ObjectFlipped)
                {
                    //EnemyLocalScaleY = -EnemyLocalScaleY;
                    this.transform.localScale = new Vector3 (-this.transform.localScale.x , this.transform.localScale.y , this.transform.localScale.z);
                    m_ObjectFlipped = true;
                }
                //GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (currentTarget.position.x > transform.position.x) //right
            {
                if(m_ObjectFlipped)
                {
                    //EnemyLocalScaleY = -EnemyLocalScaleY;
                    this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                    m_ObjectFlipped = false;
                }
                //GetComponent<SpriteRenderer>().flipX = false;
            }

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
                //this.GetComponent<Animator>().SetTrigger("IDLE");
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
        //this.GetComponent<Animator>().SetTrigger("WALK");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            GoBack();
        }
    }

}
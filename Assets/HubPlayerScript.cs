using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubPlayerScript : MonoBehaviour
{

    public float m_EnemySpeed = 2;

    //---Moviment---
    private Transform currentTarget;
    public Transform[] pivots;

    private int targetInt = 0;
    int dir = 1;

    public bool m_ObjectFlipped;
    public bool move = true;

    

    // Use this for initialization
    void Start()
    {
        currentTarget = pivots[0];
    }

    // Update is called once per frame
    void Update()
    {

        if (move)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, currentTarget.position, m_EnemySpeed * Time.deltaTime);

            if (currentTarget.position.x < transform.position.x) // left
            {
                if (!m_ObjectFlipped)
                {
                    //EnemyLocalScaleY = -EnemyLocalScaleY;
                    this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                    m_ObjectFlipped = true;
                }
                //GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (currentTarget.position.x > transform.position.x) //right
            {
                if (m_ObjectFlipped)
                {
                    //EnemyLocalScaleY = -EnemyLocalScaleY;
                    this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                    m_ObjectFlipped = false;
                }
                //GetComponent<SpriteRenderer>().flipX = false;
            }

            if (this.transform.position == currentTarget.position)
            {
                move = false;
               
                //this.GetComponent<Animator>().SetTrigger("IDLE");

            }

           
        }
        else
        {
            if (targetInt > 0)
            {
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    dir = -1;
                    move = true;
                    targetInt += dir;
                    currentTarget = pivots[targetInt];
                    print(targetInt);
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    dir = 1;
                    move = true;
                    targetInt += dir;
                    currentTarget = pivots[targetInt];
                    print(targetInt);
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    LoadScene(targetInt);
                }
            }
            else if (targetInt == 0)
            {
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    dir = 1;
                    move = true;
                    targetInt += dir;
                    currentTarget = pivots[targetInt];
                    print(targetInt);
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    LoadScene(targetInt);
                }
            }
            else dir = 0;


            
        }

    }


    public void LoadScene(int scene)
    {
        //m_Telon.SetTrigger("Telon");
        StartCoroutine(TelonWait(scene));
    }

    public IEnumerator TelonWait(int sceneToGo)
    {
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(sceneToGo);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int m_PlayerSpeed;
    Rigidbody2D m_PlayerRB2D;
    public float m_JumpForce;
    public bool m_IsTouchingFloor;
    public bool m_IsDashing;
    Vector3 DashDestination;
    float step;
	
	void Start ()
    {
        m_PlayerRB2D = this.GetComponent<Rigidbody2D>();
	}
	
	
	void Update ()
    {
		if(Input.GetAxisRaw("Horizontal") > 0) //Move right
        {
            this.transform.position += Vector3.right * Time.deltaTime * m_PlayerSpeed;

            /*  if ((Input.GetAxisRaw("Horizontal") > 0) && (IsMoving == false)) //Move right
                {
                    if(CurrentPos == "Central")
                    {

                        IsMoving = true;
                        CurrentPos = "Derecha";
                        Destino = Ambulancias[2].position;
                        CurrentCD = 0;
                        m_Animator.SetTrigger("TurnedRight");


                    }*/
        }
        else if(Input.GetAxisRaw("Horizontal") < 0) //Move Left
        {
            this.transform.position += Vector3.left * Time.deltaTime * m_PlayerSpeed;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if(m_IsTouchingFloor)
            {
                m_PlayerRB2D.AddForce( new Vector2 (0, 1.5f) * m_JumpForce, ForceMode2D.Impulse);
                m_IsTouchingFloor = false;
            }
        }

        if(!m_IsDashing)
        {
            //transform.position = Vector3.MoveTowards(this.transform.position, Destination, step);
        }

	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("floor"))
        {
            m_IsTouchingFloor = true;
        } 
    }
}

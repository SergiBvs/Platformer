using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int m_PlayerSpeed;
    Rigidbody2D m_PlayerRB2D;
    public float m_JumpForce;
    public bool m_IsTouchingFloor;
    public bool m_IsDashing;
    public bool DashCooldownOver;
    public bool HasTouchedFloor; //per no poder fer dos dashes sense tocar terra 
    Vector3 DashDestination;
    public float step;
	
	void Start ()
    {
        DashCooldownOver = true;
        m_PlayerRB2D = this.GetComponent<Rigidbody2D>();
	}
	
	
	void Update ()
    {
		if(Input.GetAxisRaw("Horizontal") > 0) //Move right
        {
           
            m_PlayerRB2D.AddForce(new Vector2(1f, 0) * m_PlayerSpeed, ForceMode2D.Impulse);

            if(m_PlayerRB2D.velocity.x >= 1)
            {
                m_PlayerRB2D.velocity = new Vector2(m_PlayerSpeed, m_PlayerRB2D.velocity.y);
            }

            if (Input.GetMouseButtonDown(1) && !m_IsDashing && DashCooldownOver) //Move right
            {
                
                DashCooldownOver = false;
                StartCoroutine(DashCooldown());
                m_IsDashing = true;
                DashDestination = new Vector3 (this.transform.position.x + 5 , this.transform.position.y , 0);
            }
        }
        else if(Input.GetAxisRaw("Horizontal") < 0) //Move Left
        {
            m_PlayerRB2D.AddForce(new Vector2(-1, 0) * m_PlayerSpeed, ForceMode2D.Impulse);
            
            if(m_PlayerRB2D.velocity.x <= -1)
            {
                m_PlayerRB2D.velocity = new Vector2(-m_PlayerSpeed, m_PlayerRB2D.velocity.y);
            }

            if (Input.GetMouseButtonDown(1) && !m_IsDashing && DashCooldownOver) //Move right
            {
                DashCooldownOver = false;
                StartCoroutine(DashCooldown());
                m_IsDashing = true;
                DashDestination = new Vector3(this.transform.position.x - 5, this.transform.position.y, 0);
            }
        }
        else
        {
            m_PlayerRB2D.velocity = new Vector3(0, m_PlayerRB2D.velocity.y);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if(m_IsTouchingFloor)
            {
                m_PlayerRB2D.AddForce( new Vector2 (0, 1.5f) * m_JumpForce, ForceMode2D.Impulse);
                m_IsTouchingFloor = false;
            }
        }

        if(m_IsDashing)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, DashDestination, step);
        }

        if (Vector3.Distance(this.transform.position, DashDestination) <= 0.01f)
        {
            m_IsDashing = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("floor"))
        {
            m_IsTouchingFloor = true;
            
        }

        if (collision.collider.CompareTag("Wall"))
        {
            m_IsDashing = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            m_IsDashing = false;
        }
    }




    public IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        DashCooldownOver = true;
    }
}

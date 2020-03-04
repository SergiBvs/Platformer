﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    //FUERZAS QUE AFECTAN AL PERSONAJE

    public int m_PlayerSpeed;
    public float m_JumpForce;
    public float step;

    //COMPROBACIONES DE COSAS

    public bool m_IsTouchingFloor;
    public bool m_IsDashing;
    public bool DashCooldownOver;
    public bool HasTouchedFloor; //per no poder fer dos dashes sense tocar terra 
    public bool m_IsOnIce;

    //OTRAS COSAS

    public Rigidbody2D m_PlayerRB2D;
    Vector3 DashDestination;
    public GameObject MainCamera; 

	void Start ()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        DashCooldownOver = true;
        m_PlayerRB2D = this.GetComponent<Rigidbody2D>();
        //MainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1);
    }
	
	
	void Update ()
    {
        //MainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1);

		if(Input.GetAxisRaw("Horizontal") > 0) //Move right
        {
            m_PlayerRB2D.AddForce(new Vector2(1f, 0) * m_PlayerSpeed, ForceMode2D.Impulse);

            if(m_PlayerRB2D.velocity.x >= 1)
            {
                m_PlayerRB2D.velocity = new Vector2(m_PlayerSpeed, m_PlayerRB2D.velocity.y);
            }

            if (Input.GetMouseButtonDown(1) && !m_IsDashing && DashCooldownOver && HasTouchedFloor) //Move right
            {
                m_PlayerRB2D.gravityScale = 0;
                HasTouchedFloor = false;
                DashCooldownOver = false;
                m_IsDashing = true;
                DashDestination = new Vector3 (this.transform.position.x + 5 , this.transform.position.y , 0);
                StartCoroutine(DashCooldown());
            }
        }
        else if(Input.GetAxisRaw("Horizontal") < 0) //Move Left
        {
            m_PlayerRB2D.AddForce(new Vector2(-1, 0) * m_PlayerSpeed, ForceMode2D.Impulse);
            
            if(m_PlayerRB2D.velocity.x <= -1)
            {
                m_PlayerRB2D.velocity = new Vector2(-m_PlayerSpeed, m_PlayerRB2D.velocity.y);
            }

            if (Input.GetMouseButtonDown(1) && !m_IsDashing && DashCooldownOver && HasTouchedFloor) //Move right
            {
                m_PlayerRB2D.gravityScale = 0;
                HasTouchedFloor = false;
                DashCooldownOver = false;
                StartCoroutine(DashCooldown());
                m_IsDashing = true;
                DashDestination = new Vector3(this.transform.position.x - 5, this.transform.position.y, 0);
            }
        }
        else if (!m_IsOnIce)
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
            m_PlayerRB2D.gravityScale = 2;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("floor"))
        {
            m_IsTouchingFloor = true;
            HasTouchedFloor = true;
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            m_IsDashing = false;
            m_PlayerRB2D.gravityScale = 2;
        }
        else if(collision.collider.CompareTag("Ice"))
        {
            m_IsTouchingFloor = true;
            m_IsOnIce = true;
            HasTouchedFloor = true;
        }
        else if (collision.collider.CompareTag("JumpPad"))
        {
            HasTouchedFloor = true;
        }
        else if (collision.collider.CompareTag("DeathZone"))
        {
            m_IsDashing = false;
            m_PlayerRB2D.gravityScale = 2;
            print("You're dead lmao, destroy game object and restart level");
            this.transform.position = new Vector3(0, 4);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("floor"))
        {
            HasTouchedFloor = true;
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            m_IsDashing = false;
            m_PlayerRB2D.gravityScale = 2;
        }
        else if (collision.collider.CompareTag("Ice"))
        {
            m_IsTouchingFloor = true;
            m_IsOnIce = true;
            HasTouchedFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("floor"))
        {
            m_IsTouchingFloor = false; 
        }
        else if (collision.collider.CompareTag("Ice"))
        {
            m_IsTouchingFloor = false;
            m_IsOnIce = false;
        }
    }

    public IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        DashCooldownOver = true;
    }
}

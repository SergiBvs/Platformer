﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // MOVIMIENTOS
    float m_direction;

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

	void Start ()
    {
        DashCooldownOver = true;
        m_PlayerRB2D = this.GetComponent<Rigidbody2D>();
        
    }
	
	
	void Update ()
    {
        //DIRECCION Y MOVIMIENTO
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            m_direction = Input.GetAxisRaw("Horizontal");
        }

        m_PlayerRB2D.AddForce(new Vector2(m_direction, 0) * m_PlayerSpeed, ForceMode2D.Impulse);

        //FRENAR SI NO ESTA EN HIELO
        if (!m_IsOnIce && Input.GetAxisRaw("Horizontal")==0)
        {
            m_direction = 0;
        }

        //LIMITE DE VELOCIDAD
        if (Mathf.Abs(m_PlayerRB2D.velocity.x) >= 1)
        {
            m_PlayerRB2D.velocity = new Vector2(m_PlayerSpeed*m_direction, m_PlayerRB2D.velocity.y);
        }

        //DASH
        if (Input.GetMouseButtonDown(1) && !m_IsDashing && DashCooldownOver && HasTouchedFloor) 
        {
            HasTouchedFloor = false;
            DashCooldownOver = false;
            m_IsDashing = true;
            DashDestination = new Vector3(this.transform.position.x + 5*m_direction, this.transform.position.y, 0);
            StartCoroutine(DashCooldown());
        }

        //SALTO
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        if(m_IsDashing)
        {
            Dash();
        }
    }

    void Dash()
    {
        //Velocidad en Y a 0 para que no haga cosas raras.
        m_PlayerRB2D.velocity = new Vector2(m_PlayerRB2D.velocity.x, 0);

        //Mover
        transform.position = Vector3.MoveTowards(this.transform.position, DashDestination, step);

        //Acabar dash
        if (Vector3.Distance(this.transform.position, DashDestination) <= 0.005f)
        {
            m_IsDashing = false;
        }
    }

    void Jump()
    {
        if (m_IsTouchingFloor)
        {
            m_PlayerRB2D.AddForce(new Vector2(0, 1.5f) * m_JumpForce, ForceMode2D.Impulse);
            m_IsTouchingFloor = false;
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
        else if(collision.collider.CompareTag("EnemyBody"))
        {
            if(!m_IsDashing)
            {
                print("perder una vida y morir si no te quedan");
            }
            else //si haces un dash o les saltas en la cabeza mueren
            {
                Destroy(collision.gameObject);
            }
        }
        else if(collision.collider.CompareTag("EnemyHead"))
        {
            //REBOTAR
            m_IsTouchingFloor = true;
            HasTouchedFloor = true;
            Jump();

            //Eliminar enemigo / Lo que sea.
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("floor"))
        {
            HasTouchedFloor = true;
            m_IsTouchingFloor = true;
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            m_IsDashing = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // MOVIMIENTOS
     float m_direction;
    public float m_lastDirection;

    int knockbackDirection;

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
    public bool m_HasRecievedDamage = false;
    public bool m_HasExitedCollision;

    float iceSpeed = .4f;

    public bool m_Knockback = false;

    //OTRAS COSAS

    public Rigidbody2D m_PlayerRB2D;
    Vector3 DashDestination;
    private GameManager m_GameManager;

	void Start ()
    {
        DashCooldownOver = true;
        m_PlayerRB2D = this.GetComponent<Rigidbody2D>();
        m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
	
	
	void Update ()
    {
        //DIRECCION Y MOVIMIENTO
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            m_lastDirection = Input.GetAxisRaw("Horizontal");
            
        }

        

        m_direction = Input.GetAxisRaw("Horizontal");

        

        if (!m_IsOnIce)
        {
            if (m_direction < 0)
            {
                m_PlayerRB2D.AddForce(new Vector2(-1, 0) * m_PlayerSpeed, ForceMode2D.Impulse);
            }
            else if (m_direction > 0)
            {
                m_PlayerRB2D.AddForce(new Vector2(1, 0) * m_PlayerSpeed, ForceMode2D.Impulse);
            }
        }
        else
        {
            if (m_direction < 0)
            {
                m_PlayerRB2D.AddForce(new Vector2(-1, 0) * iceSpeed, ForceMode2D.Impulse);
            }
            else if (m_direction > 0)
            {
                m_PlayerRB2D.AddForce(new Vector2(1, 0) * iceSpeed, ForceMode2D.Impulse);
            }
        }
        
        //LIMITE DE VELOCIDAD
        if (Mathf.Abs(m_PlayerRB2D.velocity.x) >= 1)
        {
            if (m_Knockback)
            {
                m_PlayerRB2D.velocity = new Vector2(m_PlayerSpeed * knockbackDirection, m_PlayerRB2D.velocity.y);
            }
            else if (!m_IsOnIce)
            {
                m_PlayerRB2D.velocity = new Vector2(m_PlayerSpeed * m_direction, m_PlayerRB2D.velocity.y);
                iceSpeed = .4f;
            }
        }

        //DASH
        if (Input.GetMouseButtonDown(1) && !m_IsDashing && DashCooldownOver && HasTouchedFloor) 
        {
            HasTouchedFloor = false;
            DashCooldownOver = false;
            m_IsDashing = true;
            DashDestination = new Vector3(this.transform.position.x + 5*m_lastDirection, this.transform.position.y, 0);
            StartCoroutine(DashCooldown());
            m_Knockback = false;
            m_GameManager.m_IsDashAvaliable = false;
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

        //GIRAR
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mousePos = transform.position.x - mouse.x;
        
        if(mousePos > 0)
        {
            transform.localScale = new Vector2(-1,1);
        } 
        else
        {
            transform.localScale = new Vector2(1, 1);
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
        m_Knockback = false;
        if (collision.collider.CompareTag("floor"))
        {
            m_IsTouchingFloor = true;
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
        else if (collision.collider.CompareTag("JumpPad"))
        {
            HasTouchedFloor = true;
        }
        else if (collision.collider.CompareTag("EnemyBody"))
        {
            RecievingDamage(collision);
        }
        else if (collision.collider.CompareTag("Shield"))
        {
            m_IsDashing = false;
            RecievingDamage(collision);
        }
        else if (collision.collider.CompareTag("EnemyHead"))
        {
            //REBOTAR
            m_IsTouchingFloor = true;
            HasTouchedFloor = true;
            Jump();

            //Eliminar enemigo
            collision.gameObject.GetComponent<EnemyParentScript>().DestroyParent();
        }
        else if (collision.collider.CompareTag("Box"))
        {
            if (m_IsDashing)
            {
                if (collision.collider.GetComponent<BreakableScript>().breakable)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1 * m_lastDirection, 0.1f) * 200, ForceMode2D.Impulse);
                    GetComponent<BreakableScript>().DestroyBox();
                }
                else
                {
                    print("box");
                    m_IsDashing = false;
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1 * m_lastDirection, 0.1f) * 200, ForceMode2D.Impulse);
                    m_PlayerRB2D.gravityScale = 2;
                }
            }
            else
            {
                m_IsTouchingFloor = true;
                HasTouchedFloor = true;
            }
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
        else if (collision.collider.CompareTag("Shield"))
        {
            //m_HasExitedCollision = false;
            m_IsDashing = false;
            //if(!m_HasRecievedDamage)
            //StartCoroutine(RecievingDamageCD(collision));
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
        else if(collision.collider.CompareTag("Shield"))
        {
            m_HasExitedCollision = true;
        }
    }

    public IEnumerator DashCooldown()
    {
            yield return new WaitForSeconds(0.5f);
            DashCooldownOver = true;
            m_GameManager.m_IsDashAvaliable = true;
    }

    /*public IEnumerator RecievingDamageCD(Collision2D colision)
    {
        if (!m_HasExitedCollision)
        {
            m_HasRecievedDamage = true;
            yield return new WaitForSeconds(5f);
            RecievingDamage(colision);
            m_HasRecievedDamage = false;
        }
    }*/


    public void RecievingDamage(Collision2D colision)
    {
        if (!m_IsDashing)
        {
            m_GameManager.m_Health--;

            if (m_GameManager.m_Health <= 0)
            {
                Destroy(this.gameObject);
                m_GameManager.m_GameOverPanel.SetActive(true);
            }

            //KNOCKBACK
            if (colision.transform.position.x < transform.position.x)
            {
                //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-20, 10), ForceMode2D.Impulse);
                colision.transform.GetComponentInChildren<BasicEnemyMovement>().GoBack();
                m_Knockback = true;
                knockbackDirection = 1;
                m_PlayerRB2D.AddForce(new Vector2(100, 3), ForceMode2D.Impulse);
            }
            else
            {
                m_Knockback = true;
                knockbackDirection = -1;
                m_PlayerRB2D.AddForce(new Vector2(100, 3), ForceMode2D.Impulse);
                colision.transform.GetComponentInParent<BasicEnemyMovement>().GoBack();
                //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-20, 10), ForceMode2D.Impulse);
            }
        }
        else if (m_IsDashing) //si haces un dash o les saltas en la cabeza mueren
        {
            colision.gameObject.GetComponent<EnemyParentScript>().DestroyParent();
        }
    }

}


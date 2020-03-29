using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // MOVIMIENTOS
    float m_direction;
    [HideInInspector] public float m_lastDirection;

    //FUERZAS QUE AFECTAN AL PERSONAJE

    public int m_PlayerSpeed;
    public float m_JumpForce;
    public float step;

    //COMPROBACIONES DE COSAS

    [HideInInspector] public bool m_IsTouchingFloor;
    [HideInInspector] public bool m_IsDashing;
    [HideInInspector] public bool DashCooldownOver;
    [HideInInspector] public bool HasTouchedFloor; //per no poder fer dos dashes sense tocar terra 
    [HideInInspector] public bool m_IsOnIce;
    [HideInInspector] public bool m_HasRecievedDamage = false;
    [HideInInspector]public bool m_HasExitedCollision;

    float iceSpeed = .4f;

    [HideInInspector] public bool m_Knockback = false;

    //OTRAS COSAS

    public Rigidbody2D m_PlayerRB2D;
    Vector3 DashDestination;
    //private GameManager m_GameManager;
    private ParticleSystem.MainModule feetParticles;

    public bool addforce = false;

	void Start ()
    {
        DashCooldownOver = true;
        m_PlayerRB2D = this.GetComponent<Rigidbody2D>();
        //m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        feetParticles = GetComponentInChildren<ParticleSystem>().main;
    }
	
	
	void Update ()
    {
        ////////////DEBUG - Eliminar cuando no se necesite más///////////
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance.RestartLevel();
        }

        //DIRECCION
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            m_lastDirection = Input.GetAxisRaw("Horizontal");
        }
        m_direction = Input.GetAxisRaw("Horizontal");


        //MOVIMIENTO
        if (!addforce)
        {
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
        }
        
        //LIMITE DE VELOCIDAD
        if (Mathf.Abs(m_PlayerRB2D.velocity.x) >= 0 && m_direction!=0)
        {
            if (!m_IsOnIce && !addforce)
            {
                m_PlayerRB2D.velocity = new Vector2(m_PlayerSpeed * m_direction, m_PlayerRB2D.velocity.y);
                iceSpeed = .4f;
            }
        }

        if(!m_IsOnIce && m_direction == 0 && !addforce)
        {
            m_PlayerRB2D.velocity = new Vector2(0, m_PlayerRB2D.velocity.y);
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
            GameManager.instance.DashIndicator(false);
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

        //GIRAR hacia el ratón
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

    public void Jump()
    {
        if (m_IsTouchingFloor)
        {
            m_PlayerRB2D.AddForce(new Vector2(0, 1.5f) * m_JumpForce, ForceMode2D.Impulse);
            m_IsTouchingFloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.CompareTag("floor"))
        {
            m_IsTouchingFloor = true;
            HasTouchedFloor = true;
            GetComponentInChildren<ParticleSystem>().Play();
            if (collision.collider.name == "SoulsandPlatform")
            {
                feetParticles.startColor = new Color(61 / 255f, 52 / 255f, 69 / 255f, 1);
            }
            else
            {
                feetParticles.startColor = new Color(145 / 255f, 207 / 255f, 87 / 255f, 1);
            }
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
            GetComponentInChildren<ParticleSystem>().Play();
            feetParticles.startColor = new Color(93 / 255f, 200 / 255f, 220 / 255f, 1);
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
            GetComponentInChildren<ParticleSystem>().Stop();
        }
        else if (collision.collider.CompareTag("Ice"))
        {
            m_IsTouchingFloor = false;
            m_IsOnIce = false;
            GetComponentInChildren<ParticleSystem>().Stop();
        }
        else if(collision.collider.CompareTag("Shield"))
        {
            m_HasExitedCollision = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Final"))
        {
            GameManager.instance.NextScene();
        }
        else if (collision.name == "MovingPlatform")
        {
            print("MovingPlatform");
        }
    }

    public IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        DashCooldownOver = true;
        GameManager.instance.DashIndicator(true);
    }


    public void RecievingDamage(Collision2D collision)
    {
        if (!m_IsDashing)
        {
            GameManager.instance.Health(-1);

            //KNOCKBACK
            if (collision.transform.position.x <= transform.position.x)
            {
                addforce = true;
                StartCoroutine(KnockbackTime());
                m_PlayerRB2D.AddForce(new Vector2(10, 10), ForceMode2D.Impulse);
                collision.transform.GetComponentInParent<BasicEnemyMovement>().GoBack();
            }
            else
            {
                addforce = true;
                StartCoroutine(KnockbackTime());
                m_PlayerRB2D.AddForce(new Vector2(-10, 10), ForceMode2D.Impulse);
                collision.transform.GetComponentInParent<BasicEnemyMovement>().GoBack();
                
            }
        }
        else if (m_IsDashing) //si haces un dash o les saltas en la cabeza mueren
        {
            collision.gameObject.GetComponent<EnemyParentScript>().DestroyParent();
        }
    }

    public IEnumerator KnockbackTime()
    {
        yield return new WaitForSeconds(.5f);
        addforce = false;
    }

}


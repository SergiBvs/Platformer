using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public GameObject m_ConnectedPortal;
    private Player m_Player;

    private bool m_PortalUsed = false;
	
	void Start ()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!m_PortalUsed)
        {
            if(collision.CompareTag("Player"))  
            {
                m_Player.m_IsDashing = false;
                m_PortalUsed = true;
                collision.gameObject.transform.position = m_ConnectedPortal.transform.position;
                collision.gameObject.GetComponent<Player>().m_PlayerRB2D.AddForce(m_ConnectedPortal.transform.right * 5);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PortalCooldown());
       }
       
    }

    IEnumerator PortalCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        m_PortalUsed = false;
    }
}

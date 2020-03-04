using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public GameObject m_ConnectedPortal;
    private GameManager m_GameManager;
	
	void Start ()
    {
        m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}
	
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!m_GameManager.m_PortalUsed)
        {
            if(collision.CompareTag("Player"))  
            {
                m_GameManager.m_PortalUsed = true;
                collision.gameObject.transform.position = m_ConnectedPortal.transform.position;
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
        m_GameManager.m_PortalUsed = false;
    }
}

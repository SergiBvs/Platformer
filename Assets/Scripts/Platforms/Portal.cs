using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public GameObject m_ConnectedPortal;
    private Player m_Player;
    private SoundManager m_PortalSound;

    private static bool m_PortalUsed = false;
	
	void Start ()
    {
        m_PortalSound = GameObject.FindGameObjectWithTag("PortalSound").GetComponent<SoundManager>();
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!m_PortalUsed)
        {
            if(collision.CompareTag("Player"))
            {
                m_PortalSound.m_AS.clip = m_PortalSound.m_PortalSound;
                m_PortalSound.m_AS.Play();

                print("test");
                m_Player.m_IsDashing = false;
                m_PortalUsed = true;
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
        m_PortalUsed = false;
    }
}

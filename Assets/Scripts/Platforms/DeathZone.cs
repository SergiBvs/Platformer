using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    public Transform m_RespawnPoint;
    private GameManager m_GameManager;

    void Start()
    {
        m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().m_IsDashing = false;
            collision.gameObject.GetComponent<Player>().m_PlayerRB2D.gravityScale = 2;

            m_GameManager.m_Health--;

            if(m_GameManager.m_Health > 0)
            {
                collision.gameObject.GetComponent<Player>().transform.position = m_RespawnPoint.position; 
            }
            else
            {
                Destroy(collision.gameObject);
                m_GameManager.m_GameOverPanel.SetActive(true);
                //pon la pantalla de muerte
            }

        }
    }
}

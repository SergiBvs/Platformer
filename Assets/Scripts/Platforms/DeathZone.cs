using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    public Transform m_RespawnPoint;

    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().m_IsDashing = false;
            collision.gameObject.GetComponent<Player>().m_PlayerRB2D.gravityScale = 2;

            GameManager.GetInstance().m_Health--;

            if (GameManager.GetInstance().m_Health > 0)
            {
                collision.gameObject.GetComponent<Player>().transform.position = m_RespawnPoint.position; 
            }
            else
            {
                Destroy(collision.gameObject);
                GameManager.GetInstance().m_GameOverPanel.SetActive(true);
                //pon la pantalla de muerte
            }

        }
    }
}

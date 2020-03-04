using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    public Transform m_RespawnPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().m_IsDashing = false;
            collision.gameObject.GetComponent<Player>().m_PlayerRB2D.gravityScale = 2;
            collision.gameObject.GetComponent<Player>().transform.position = m_RespawnPoint.position; 
            //Quan es perdin totes les vides reiniciarem el nivell
        }
    }
}

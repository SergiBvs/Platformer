using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    public Transform m_RespawnPoint;
    //private GameManager m_GameManager;

    void Start()
    {
        //m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().m_IsDashing = false;
            GameManager.instance.Health(-1);
            collision.gameObject.GetComponent<Player>().transform.position = m_RespawnPoint.position; 
        }
    }
}

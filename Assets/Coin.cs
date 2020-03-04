using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private GameManager m_GameManager;

    void Start()
    {
        m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            m_GameManager.SumarCoins();
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.GetInstance().m_Coins++;
            GameManager.GetInstance().CoinUpdate();
            Destroy(this.gameObject);
        }
    }
}

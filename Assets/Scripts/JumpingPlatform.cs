using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour {


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().m_PlayerRB2D.AddForce(new Vector2(0, 2f) * collision.gameObject.GetComponent<Player>().m_JumpForce, ForceMode2D.Impulse);
        }
    }
}

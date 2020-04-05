using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour {

    private SoundManager m_BounceSound;

    void Start()
    {
        m_BounceSound = GameObject.FindGameObjectWithTag("BounceSound").GetComponent<SoundManager>();    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_BounceSound.m_AS.clip = m_BounceSound.m_BounceSound;
            m_BounceSound.m_AS.Play();
            collision.gameObject.GetComponent<Player>().m_PlayerRB2D.AddForce(new Vector2(0, 2f) * collision.gameObject.GetComponent<Player>().m_JumpForce, ForceMode2D.Impulse);
        }
    }
}

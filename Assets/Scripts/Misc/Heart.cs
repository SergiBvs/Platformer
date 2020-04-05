using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    private SoundManager m_HealthUpSound;

    void Start()
    {
        m_HealthUpSound = GameObject.FindGameObjectWithTag("HealthUpSound").GetComponent<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(GameManager.instance.m_Health <3)
            {
                m_HealthUpSound.m_AS.clip = m_HealthUpSound.m_HealthUpSound;
                m_HealthUpSound.m_AS.Play();

                GameManager.instance.Health(1);
                Destroy(this.gameObject);
            }
        }
    }
}

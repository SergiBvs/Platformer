using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    //private GameManager m_GameManager;
    private bool collect = false;
    public bool isDrop = false;
    private SoundManager m_CoinPickUpSound;
    
    void Start()
    {
        m_CoinPickUpSound = GameObject.FindGameObjectWithTag("CoinPickUpSound").GetComponent<SoundManager>();
       
        //m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        StartCoroutine(CollectCooldown());
        if (isDrop)
        {
            StartCoroutine(DropTimeOut());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (collect)
            {
                float l_rand;

                l_rand = Random.Range(0.8f, 1.5f);

                m_CoinPickUpSound.m_AS.pitch = l_rand;

                m_CoinPickUpSound.m_AS.clip = m_CoinPickUpSound.m_CoinPickUpSound;
                m_CoinPickUpSound.m_AS.Play();

                GameManager.instance.CoinUpdate(1);
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator CollectCooldown()
    {
        yield return new WaitForSeconds(1f);
        collect = true;
    }

    //LOS DROPS TIENEN TIEMPO LIMITADO.
    IEnumerator DropTimeOut()
    {
        yield return new WaitForSeconds(3);
        this.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        this.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.3f);
        this.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);




    }
}

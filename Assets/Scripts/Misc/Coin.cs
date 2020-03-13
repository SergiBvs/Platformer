using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private GameManager m_GameManager;
    private bool collect = false;
    public bool isDrop = false;

    void Start()
    {
        m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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
                m_GameManager.m_Coins++;
                m_GameManager.CoinUpdate();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    [HideInInspector] public bool m_PortalUsed;
    public TextMeshProUGUI m_textCoins;
    public int m_Coins;
    public int m_Health;
    public int m_nOfHearts;
    public Image[] m_Hearts;
    public Sprite FullHeart;
    public Sprite EmptyHeart;

    void Start ()
    {
        m_textCoins.text = "Coins : " + m_Coins;
    }
	
	void Update ()
    {
        m_textCoins.text = "Coins : " + m_Coins;
        HealthSystem();
    }

    public void HealthSystem()
    {
        if (m_Health > m_nOfHearts)
        {
            m_Health = m_nOfHearts;
        }

        for (int i = 0; i < m_Hearts.Length; i++)
        {
            if (i < m_Health)
            {
                m_Hearts[i].sprite = FullHeart;
            }
            else
            {
                m_Hearts[i].sprite = EmptyHeart;
            }

            if (i < m_nOfHearts)
            {
                m_Hearts[i].enabled = true;
            }
            else
            {
                m_Hearts[i].enabled = false;
            }
        }
    }
}

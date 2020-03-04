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
    }

    public void SumarCoins()
    {
        m_Coins++;
    }
}

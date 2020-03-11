using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    [HideInInspector] public bool m_PortalUsed;
    public TextMeshProUGUI m_textCoins;
    public int m_Coins;
    public int m_Health;
    public int m_nOfHearts;
    public Image[] m_Hearts;
    public Sprite FullHeart;
    public Sprite EmptyHeart;

    public Sprite DashAvaliable;
    public Sprite DashUnavaliable;
    public Image DashIndicatorIMG;
    public bool m_IsDashAvaliable;


    public GameObject m_GameOverPanel;

    private static GameManager instance;

    public static GameManager GetInstance()
    {
        if(instance == null)
        {
            instance = new GameManager();
        }
        return instance;
    }
    void Start()
    {
        m_IsDashAvaliable = true;
        CoinUpdate();
    }

    void Update()
    {
        HealthSystem();
        DashIndicator();

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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

    public void DashIndicator()
    {
        if (m_IsDashAvaliable)
        {
            DashIndicatorIMG.sprite = DashAvaliable;
        }
        else
        {
            DashIndicatorIMG.sprite = DashUnavaliable;
        }
    }

    public void CoinUpdate()
    {
        m_textCoins.text = m_Coins + "x";
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
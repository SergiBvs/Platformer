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
    [HideInInspector] public int m_Coins;
    [HideInInspector] public int m_Health;
    [HideInInspector] public int m_nOfHearts;
    public Image[] m_Hearts;
    public Sprite FullHeart;
    public Sprite EmptyHeart;

    public Sprite DashAvaliable;
    public Sprite DashUnavaliable;
    public Image DashIndicatorIMG;
    [HideInInspector] public bool m_IsDashAvaliable;


    public GameObject m_GameOverPanel;
    public Animator m_Telon;

    
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
            RestartLevel();
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
        m_Telon.SetTrigger("Telon");
        StartCoroutine(TelonWait(SceneManager.GetActiveScene().buildIndex));
    }

    public void NextScene()
    {
        m_Telon.SetTrigger("Telon");
        StartCoroutine(TelonWait(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public IEnumerator TelonWait(int sceneToGo)
    {
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(sceneToGo);
    }

}
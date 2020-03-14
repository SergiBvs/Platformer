using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    

   
    //__PLAYER STATS__//

        private GameObject m_Player;
        [HideInInspector] public static int m_Coins;
        [HideInInspector] public static int m_Health;

    //__GAME UI__//

        //Currency
        public TextMeshProUGUI m_textCoins;

        //Health
        [HideInInspector] public static int m_nOfHearts;
        public Image[] m_Hearts;
        public Sprite FullHeart;
        public Sprite EmptyHeart;

        //Dash
        public Sprite DashAvaliable;
        public Sprite DashUnavaliable;
        public Image DashIndicatorIMG;
        [HideInInspector] public bool m_IsDashAvaliable;

    //__GAME OVER__//
        [HideInInspector] public bool m_GameOver;
        public GameObject m_GameOverPanel;

    //__OTHER__//
        public Animator m_Telon;

    void Start()
    {
        m_IsDashAvaliable = true;
        m_Player = GameObject.FindGameObjectWithTag("Player");
        CoinUpdate(0);
        HealthSystem();
    }

    void Update()
    {
        DashIndicator();

        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void Health(int howMuch)
    {
        m_Health += howMuch;
        HealthSystem();
        if(m_Health <= 0)
        {
            Destroy(m_Player);
            m_GameOverPanel.SetActive(true);
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

    public void CoinUpdate(int howMuch)
    {
        m_Coins += howMuch;
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
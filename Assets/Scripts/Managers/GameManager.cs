using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
   
    //__PLAYER STATS__//

        private GameObject m_Player;
        [HideInInspector] public int m_Coins;
        [HideInInspector] public int m_Health = 3;

    //__GAME UI__//

        private GUIHelper GUIHelp;

        //Health
        [HideInInspector] public static int m_nOfHearts = 3;
        public Sprite FullHeart;
        public Sprite EmptyHeart;

        //Dash
        public Sprite DashAvaliable;
        public Sprite DashUnavaliable;
        [HideInInspector] public bool m_IsDashAvaliable;

    //__GAME OVER__//
        [HideInInspector] public bool m_GameOver;
        private GameObject m_GameOverPanel;

    //__OTHER__//
        [HideInInspector] public Animator m_Telon;

    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        ReassignObjs();
    }

    public void ReassignObjs()
    {
        m_IsDashAvaliable = true;
        GUIHelp = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIHelper>();
        m_Telon = GUIHelp.telon;
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_GameOver = false;
        CoinUpdate(0);
        HealthSystem();
        DashIndicator(true);
    }

    public void Health(int howMuch)
    {
        m_Health += howMuch;
        HealthSystem();

        if(m_Health <= 0)
        {
            m_GameOver = true;
            Destroy(m_Player);
            GUIHelp.m_GameOverPanel.SetActive(true);
        }

    }

    public void HealthSystem()
    {
        if (m_Health > m_nOfHearts)
        {
            m_Health = m_nOfHearts;
        }

        for (int i = 0; i < GUIHelp.m_Hearts.Length; i++)
        {
            if (i < m_Health)
            {
                GUIHelp.m_Hearts[i].sprite = FullHeart;
            }
            else
            {
                GUIHelp.m_Hearts[i].sprite = EmptyHeart;
            }

            if (i < m_nOfHearts)
            {
                GUIHelp.m_Hearts[i].enabled = true;
            }
            else
            {
                GUIHelp.m_Hearts[i].enabled = false;
            }
        }
    }

    public void DashIndicator(bool l_isDashAvailable)
    {
        if (l_isDashAvailable)
        {
            GUIHelp.DashIndicatorIMG.sprite = DashAvaliable;
        }
        else
        {
            GUIHelp.DashIndicatorIMG.sprite = DashUnavaliable;
        }
    }

    public void CoinUpdate(int howMuch)
    {
        m_Coins += howMuch;
        GUIHelp.m_textCoins.text = m_Coins + "x";
    }


    public void NextScene()
    {
        m_Telon.SetTrigger("Telon");
        m_Health = 3;
        StartCoroutine(TelonWait(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadScene(int scene)
    {
        m_Telon.SetTrigger("Telon");
        StartCoroutine(TelonWait(scene));
    }

    public IEnumerator TelonWait(int sceneToGo)
    {
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(sceneToGo);
    }

}
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GUIHelper : MonoBehaviour {

    //Currency
    public TextMeshProUGUI m_textCoins;

    //Health
    public Image[] m_Hearts;

    //Dash
    public Image DashIndicatorIMG;

    //GameOver
    public GameObject m_GameOverPanel;

    //Telon
    public Animator telon;

    //EndFlag

    public GameObject m_Endflag;
    [HideInInspector] public SpriteRenderer m_EndFlagSR;
    public Sprite m_EndFlagSad;
    public Sprite m_EndFlagHappy;

    //Sounds

        [HideInInspector] public SoundManager m_DeathSound;
        

    void Start()
    {
        telon = GameObject.FindGameObjectWithTag("Telon").GetComponent<Animator>();
        m_DeathSound = GameObject.FindGameObjectWithTag("DeathSound").GetComponent<SoundManager>();
        GameManager.instance.ReassignObjs();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartPauseGame : MonoBehaviour
{
    public GameObject m_PausePanel;
    public bool m_GameIsPaused;


    void Update()
    {
        if(GameManager.instance.m_IsGameOverPanelOn == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (m_GameIsPaused)
                {
                    ResumeGame();
                }
                else if (!m_GameIsPaused)
                {
                    PauseGame();
                }
            }
        }
    }

    public void ResumeGame()
    {
        m_PausePanel.SetActive(false);
        Time.timeScale = 1f;
        m_GameIsPaused = false;
    }

    public void PauseGame()
    {
        m_PausePanel.SetActive(true);
        Time.timeScale = 0f;
        m_GameIsPaused = true;

    }

    public void RestartLevel()
    {
        GameManager.instance.m_Telon.SetTrigger("Telon");
        GameManager.instance.m_Health = 3;
        GameManager.instance.m_CurrentCoins = 0;
        StartCoroutine(GameManager.instance.TelonWait(SceneManager.GetActiveScene().buildIndex));
        m_PausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameManager.instance.m_IsGameOverPanelOn = false;
    }



}

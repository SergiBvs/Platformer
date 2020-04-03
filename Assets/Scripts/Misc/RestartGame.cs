using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void RestartLevel()
    {
        GameManager.instance.m_Telon.SetTrigger("Telon");
        GameManager.instance.m_Health = 3;
        StartCoroutine(GameManager.instance.TelonWait(SceneManager.GetActiveScene().buildIndex));
    }

}

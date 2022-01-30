using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button m_StartGame;
    [SerializeField] private Button m_Credits;
    [SerializeField] private Button m_ExitGame;

    string m_GameScene = "SampleScene";
    string m_CreditsScene = "CreditScene";
    void Start()
    {
        m_StartGame.onClick.AddListener(() =>
        {
            LoadScene(m_GameScene);
        });
        m_Credits.onClick.AddListener(() =>
        {
            LoadScene(m_CreditsScene);
        });
        m_ExitGame.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}

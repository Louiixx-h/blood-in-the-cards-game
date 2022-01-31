using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitCredit : MonoBehaviour
{
    [SerializeField] private Button m_Button;

    void Start()
    {
        m_Button.onClick.AddListener(() =>
        {
            GotToMenuScene();
        });
    }

    void GotToMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}

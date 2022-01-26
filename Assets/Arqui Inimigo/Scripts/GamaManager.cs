using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamaManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private ListView m_ListViewAction, m_ListViewMove;
    [SerializeField] private PlayButton m_PlayButton;
    [SerializeField] private CardSelected m_CardSelected;
    [SerializeField] private Text m_TextStart;

    [Header("Caracters")]
    [SerializeField] private Player m_Player;
    [SerializeField] private Enemy m_Enemy;
    
    private GamaManager m_GameManager;
    int timer = 3;

    private void Awake()
    {
        if(m_GameManager != null)
        {
            Destroy(gameObject);
            return;
        }
        m_GameManager = this;
    }

    void Start()
    {
        m_ListViewAction.OnClickCard += m_CardSelected.SaveCardSelected;
        m_ListViewMove.OnClickCard += m_CardSelected.SaveCardSelected;

        m_ListViewAction.OnClickCard += m_Player.SetCard;
        m_ListViewMove.OnClickCard += m_Player.SetCard;

        m_CardSelected.OnDiscardCard += m_ListViewAction.RecoverCard;
        m_CardSelected.OnDiscardCard += m_ListViewMove.RecoverCard;

        m_PlayButton.OnClickPlay += PlayRound;

        StartCoroutine(CountSeconds());   
    }

    IEnumerator CountSeconds()
    {
        do
        {
            if(timer == 0) m_TextStart.text = "Start!";
            else m_TextStart.text = timer.ToString();
            
            timer--;
            yield return new WaitForSeconds(1);
        }
        while (timer >= 0);
        m_TextStart.text = "";
        StartGame();
    }

    void StartGame()
    {

    }

    void PlayRound()
    {
        m_Player.DoAction();
        m_Enemy.DoMove();
    }
}

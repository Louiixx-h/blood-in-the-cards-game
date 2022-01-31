using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("CARACTERS")]
    [SerializeField] private Player m_Player;
    [SerializeField] private Enemy m_Enemy;

    [Header("LIST VIEW")]
    [SerializeField] private ListView m_ListViewAction;
    [SerializeField] private ListView m_ListViewMove;
    [SerializeField] private ListViewMap m_ListViewMap;
    [SerializeField] private SwitchListView m_SwitchListView;

    [Header("CARD SELECTED")]
    [SerializeField] private CardSelected m_CardSelectedPlayer;
    [SerializeField] private CardItem m_CardSelectedAction;
    [SerializeField] private CardItem m_CardSelectedMove;

    [Header("DIALOG")]
    [SerializeField] private DialogAction m_DialogEnemy;
    [SerializeField] private DialogAction m_DialogPlayer;
    [SerializeField] private Text m_Dialog;

    [Header("UI TEXTS")]
    [SerializeField] private Text m_TextWin;
    [SerializeField] private Text m_TextStart;
    [SerializeField] private Text m_TextStartShadow;
    [SerializeField] private Text m_TurnPlayer;
    [SerializeField] private Text m_TurnEnemy;
    [SerializeField] private Text m_TextRewardEnemy;
    [SerializeField] private Text m_TextRewardPlayer;
    [SerializeField] private Text m_TextGoldEnemy;
    [SerializeField] private Text m_TextGoldPlayer;

    [Header("UI BUTTON")]
    [SerializeField] private Button m_PlayButton;
    [SerializeField] private Button m_ExitButton;

    [Header("UI DIALOG END GAME")]
    [SerializeField] private GameObject m_PanelShadow;

    [Header("UI IMAGE")]
    [SerializeField] private Image m_SceneImage;

    [Header("UI PANEL")]
    [SerializeField] private GameObject m_PanelChooseMap;

    int m_Timer = 3;

    void Start()
    {
        ConfigPlayer();
        ConfigListView();
        InitialRewards();
        ClearTextsOfStatus();

        GamaManager.OnStateChooseMap += ChooseMapState;
        GamaManager.OnStateCountSeconds += CountSecondsStateUI;
        GamaManager.OnStateRunGame += RunGameStateUI;
        GamaManager.OnStateTurnPlayer += TurnPlayerStateUI;
        GamaManager.OnStateTurnEnemy += TurnEnemyStateUI;
        GamaManager.OnStateTurnEnemy += TurnEnemyStateUI;
        GamaManager.OnStateEndGame += EndGameStateUI;
        GamaManager.OnAction += ChooseAction;
        GamaManager.OnIncrementReward += IncrementReward;
        GamaManager.OnShowDialog += ShowDialog;
    }

    void ConfigListView()
    {
        m_ListViewAction.OnClickCard += m_CardSelectedPlayer.SaveCardSelected;
        m_ListViewMove.OnClickCard += m_CardSelectedPlayer.SaveCardSelected;

        m_SwitchListView.OnClickAction += m_ListViewAction.IsVisible;
        m_SwitchListView.OnClickMove += m_ListViewMove.IsVisible;

        m_CardSelectedPlayer.OnDiscardCard += m_ListViewAction.RecoverCard;
        m_CardSelectedPlayer.OnDiscardCard += m_ListViewMove.RecoverCard;

        m_ListViewMap.OnClickMap += SetMap;
    }

    void ConfigPlayer()
    {
        m_ListViewAction.OnClickCard += m_Player.SetCard;
        m_ListViewMove.OnClickCard += m_Player.SetCard;

        m_CardSelectedPlayer.OnDiscardCard += m_Player.RemoveCard;
        m_Player.OnUseCards += m_CardSelectedPlayer.RemoveAllListeners;
    }

    void ChooseMapState()
    {
    }

    void CountSecondsStateUI()
    {
        StartCoroutine(CountSeconds());
    }

    private void RunGameStateUI()
    {
        if (!m_ListViewAction.HasCard() && !m_ListViewMove.HasCard())
        {
            GamaManager.ChangeState(StateGame.STATE_END_GAME);
            return;
        }

        m_TurnEnemy.text = "Escolhendo...";
        m_TurnPlayer.text = "Escolhendo...";

        m_CardSelectedPlayer.RemoveCards();
        m_ListViewAction.AddAllListeners();
        m_ListViewMove.AddAllListeners();

        m_PlayButton.onClick.AddListener(() =>
        {
            if (!m_Player.IsReady)
            {
                ShowDialog("Alerta: O jogador não está pronto!");
                return;
            }
            m_PlayButton.onClick.RemoveAllListeners();
            SetCardEnemy();
            GamaManager.ChangeState.Invoke(StateGame.STATE_TURN_PLAYER);
        });
    }

    private void TurnPlayerStateUI()
    {
        m_TurnPlayer.text = "Seu turno";
        m_TurnEnemy.text = "";
    }

    private void TurnEnemyStateUI()
    {
        m_TurnEnemy.text = "Turno inimigo";
        m_TurnPlayer.text = "";
    }

    private void EndGameStateUI()
    {
        m_TextGoldEnemy.text = m_TextRewardEnemy.text;
        m_TextGoldPlayer.text = m_TextRewardPlayer.text;
        m_PanelShadow.gameObject.SetActive(false);
        m_PanelShadow.gameObject.GetComponentInChildren<DialogStatus>().StartDialog();

        RemoveAllListeners();

        int valueEnemy = GetIntFromString(m_TextRewardEnemy.text);
        int valuePlayer = GetIntFromString(m_TextRewardPlayer.text);

        if (valueEnemy > valuePlayer)
        {
            m_TextWin.text = "Você perdeu!!";
        }
        else
        {
            m_TextWin.text = "Você ganhou!!";
        }

        m_PanelShadow.SetActive(true);
        m_PanelShadow.GetComponentInChildren<DialogStatus>().StartDialog();
        m_ExitButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MenuScene");
            GamaManager.Instance.Destroy();
            Destroy(gameObject);
        });
    }

    IEnumerator CountSeconds()
    {
        m_TextStart.gameObject.SetActive(true);
        m_TextStartShadow.gameObject.SetActive(true);
        do
        {
            if (m_Timer == 0)
            {
                m_TextStart.text = "Start!";
                m_TextStartShadow.text = "Start!";
            }
            else
            {
                m_TextStart.text = m_Timer.ToString();
                m_TextStartShadow.text = m_Timer.ToString();
            }

            m_Timer--;
            yield return new WaitForSeconds(1);
        }
        while (m_Timer >= 0);
        m_SwitchListView.Up();
        ClearTextOfCount();
        GamaManager.ChangeState(StateGame.STATE_RUN_GAME);
    }

    void SetMap(MapTemplate mapInfo)
    {
        m_SceneImage.sprite = mapInfo.m_Sprite;
        m_PanelChooseMap.SetActive(false);
        GamaManager.ChangeState(StateGame.STATE_COUNT_SECONDS);
    }

    void SetCardEnemy()
    {
        CardTemplate currentAction = m_Enemy.m_CurrentAction;
        CardTemplate currentMove = m_Enemy.m_CurrentMove;

        m_CardSelectedAction.gameObject.SetActive(true);
        m_CardSelectedMove.gameObject.SetActive(true);
        
        m_CardSelectedAction.SetCardInfo(currentAction);
        m_CardSelectedMove.SetCardInfo(currentMove);
    }

    private void InitialRewards()
    {
        m_TextRewardPlayer.text = "R$ 0,00";
        m_TextRewardEnemy.text = "R$ 0,00";
    }

    private void ClearTextOfCount()
    {
        m_TextStart.text = "";
        m_TextStartShadow.text = "";
    }

    private void ClearTextsOfStatus()
    {
        m_TurnEnemy.text = "";
        m_TurnPlayer.text = "";
    }

    void IncrementReward(string name, int increment)
    {
        if (name.Equals(m_Enemy.m_NameEnemy))
        {
            double oldValue = GetIntFromString(m_TextRewardPlayer.text);
            double newValue = (oldValue + increment);
            m_TextRewardPlayer.text = "R$ " + newValue + ",00";
        }
        else if (name.Equals(m_Player.m_NamePlayer))
        {
            double oldValue = GetIntFromString(m_TextRewardEnemy.text);
            double newValue = (oldValue + increment);
            m_TextRewardEnemy.text = "R$ " + newValue + ",00";
        }
    }

    int GetIntFromString(string text)
    {
        return Convert.ToInt16(
            text.Replace("R$", "")
                .Replace(",00", "")
                .Trim()
            );
    }

    private void ShowDialog(string msg)
    {
        m_Dialog.text = msg;
    }

    private void ChooseAction(string name)
    {
        if (name.Equals(m_Player.m_NamePlayer))
        {
            ActionDialogPlayer();
        }
        if (name.Equals(m_Enemy.m_NameEnemy))
        {
            ActionDialogEnemy();
        }
    }

    private void ActionDialogEnemy()
    {
        m_DialogEnemy.StartDialog();
    }

    private void ActionDialogPlayer()
    {
        m_DialogPlayer.StartDialog();
    }

    void RemoveAllListeners()
    {
        m_PlayButton.onClick.RemoveAllListeners();
        m_CardSelectedPlayer.RemoveAllListeners();
        m_SwitchListView.RemoveAllListeners();
        m_ListViewAction.RemoveAllListeners();
        m_ListViewMove.RemoveAllListeners();

        GamaManager.OnStateCountSeconds -= CountSecondsStateUI;
        GamaManager.OnStateRunGame -= RunGameStateUI;
        GamaManager.OnStateTurnPlayer -= TurnPlayerStateUI;
        GamaManager.OnStateTurnEnemy -= TurnEnemyStateUI;
        GamaManager.OnStateTurnEnemy -= TurnEnemyStateUI;
        GamaManager.OnStateEndGame -= EndGameStateUI;
        GamaManager.OnAction -= ChooseAction;
        GamaManager.OnIncrementReward -= IncrementReward;
        GamaManager.OnShowDialog -= ShowDialog;
    }
}

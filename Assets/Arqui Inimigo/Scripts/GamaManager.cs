using UnityEngine;

public class GamaManager : MonoBehaviour
{
    [Header("CARACTERS")]
    [SerializeField] private Player m_Player;
    [SerializeField] private Enemy m_Enemy;
    
    public static GamaManager Instance;
    
    const string m_PlayerName = "Jogador";
    const string m_EnemyName = "Inimigo";


    public delegate void ActionDelegate(string name);
    public static ActionDelegate OnAction;

    public delegate void ShowDialogDelegate(string msg);
    public static ShowDialogDelegate OnShowDialog; 

    public delegate void IncrementRewardDelegate(string name, int increment);
    public static IncrementRewardDelegate OnIncrementReward;


    public delegate void ChangeStateDelegate(StateGame state);
    public static ChangeStateDelegate ChangeState;

    public delegate void StateCountSecondDelegate();
    public static StateCountSecondDelegate OnStateCountSeconds;

    public delegate void StateRunGameDelegate();
    public static StateRunGameDelegate OnStateRunGame;

    public delegate void StateTurnPlayerDelegate();
    public static StateTurnPlayerDelegate OnStateTurnPlayer;

    public delegate void StateTurnEnemyDelegate();
    public static StateTurnEnemyDelegate OnStateTurnEnemy;

    public delegate void StateEndGameDelegate();
    public static StateEndGameDelegate OnStateEndGame;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        InitObservers();
        ChangeState(StateGame.STATE_COUNT_SECONDS);
    }

    void InitObservers()
    {
        ConfigState();
        ConfigPlayer();
        ConfigEnemy();
    }

    private void ConfigState()
    {
        ChangeState += ChangeStateGame;
        OnStateRunGame += RunState;
        OnStateTurnPlayer += TurnPlayerState;
        OnStateTurnEnemy += TurnEnemyState;
        OnStateEndGame += EndState;
    }

    void ConfigPlayer()
    {
        m_Enemy.OnAction += m_Player.CalculateDamage;
        m_Player.OnAffectedSite += Damage;
    }

    void ConfigEnemy()
    {
        m_Player.OnAction += m_Enemy.CalculateDamage;
        m_Enemy.OnAffectedSite += Damage;
    }

    private void ChangeStateGame(StateGame state)
    {
        switch (state)
        {
            case StateGame.STATE_COUNT_SECONDS:
                OnStateCountSeconds.Invoke();
                break;
            case StateGame.STATE_RUN_GAME:
                OnStateRunGame.Invoke();
                break;
            case StateGame.STATE_TURN_PLAYER:
                OnStateTurnPlayer.Invoke();
                break;
            case StateGame.STATE_TURN_ENEMY:
                OnStateTurnEnemy.Invoke();
                break;
            case StateGame.STATE_END_GAME:
                OnStateEndGame.Invoke();
                break;
        }
    }

    void RunState()
    {
        m_Enemy.ChooseCards();
    }

    void TurnPlayerState()
    {
        m_Player.DoAction();
    }

    void TurnEnemyState()
    {
        m_Enemy.DoAction();
    }

    private void EndState()
    {
        m_Enemy.OnAction -= m_Player.CalculateDamage;
        m_Player.OnAffectedSite -= Damage;
        m_Player.OnAction -= m_Enemy.CalculateDamage;
        m_Enemy.OnAffectedSite -= Damage;
        ChangeState -= ChangeStateGame;
        OnStateRunGame -= RunState;
        OnStateTurnPlayer -= TurnPlayerState;
        OnStateTurnEnemy -= TurnEnemyState;
        OnStateEndGame -= EndState;
    }

    private void Damage(EnumBody body, string name)
    {
        switch (body)
        {
            case EnumBody.HEAD:
                {
                    OnShowDialog.Invoke(name + ": Foi atingido na cabeça!");
                    OnIncrementReward.Invoke(name, 100);
                    break;
                }
            case EnumBody.TRUNK:
                {
                    OnShowDialog.Invoke(name + ": Foi atingido no torso!");
                    OnIncrementReward.Invoke(name, 80);
                    break;
                }
            case EnumBody.LEFT_LEG:
                {
                    OnShowDialog.Invoke(name + ": Foi atingido na perna esquerda!");
                    OnIncrementReward.Invoke(name, 25);
                    break;
                }
            case EnumBody.RIGHT_LEG:
                {
                    OnShowDialog.Invoke(name + ": Foi atingido na perna direita!");
                    OnIncrementReward.Invoke(name, 25);
                    break;
                }
            case EnumBody.LEFT_ARM:
                {
                    OnShowDialog.Invoke(name + ": Foi atingido no braço esquerdo!");
                    OnIncrementReward.Invoke(name, 35);
                    break;
                }
            case EnumBody.RIGHT_ARM:
                {
                    OnShowDialog.Invoke(name + ": Foi atingido no braço direito!");
                    OnIncrementReward.Invoke(name, 35);
                    break;
                }
            default: 
                {
                    OnShowDialog.Invoke(name + ": Desviou da bala!");
                    OnIncrementReward.Invoke(name, 0);
                    break;
                }
        }

        if(name == m_EnemyName)
        {
            ChangeState.Invoke(StateGame.STATE_TURN_ENEMY);
        }
        else if (name == m_PlayerName)
        {
            ChangeState.Invoke(StateGame.STATE_RUN_GAME);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

public enum StateGame
{
    STATE_COUNT_SECONDS,
    STATE_RUN_GAME,
    STATE_TURN_PLAYER,
    STATE_TURN_ENEMY,
    STATE_END_GAME
}

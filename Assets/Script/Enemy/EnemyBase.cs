using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IEnemy {

    protected IEnemyState _currentState = IEnemyState.InPool;

    public CombatManager CB;

    
    public int Credits;
    public int Attack;
    public bool IsAlive;
    public string PlayerToAttack;
    public GameObject PGreen, PBlue, PRed, PYellow;
    public GamePlayManager Gpm;
    public Player CurrentPlayer;

    public int WinPoint;

    public int ID { get { return getID(); } }

    protected abstract int getID();

    public IEnemyState CurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }

    int IEnemy.Attack { get { return Attack; } set { Attack = value; } }

    int IEnemy.Credits { get { return Credits; } }

    int IEnemy.CombatPoint { get { return WinPoint; } set { WinPoint = value; } }

    bool IEnemy.IsAlive { get { return IsAlive; } }

    Player IEnemy.CurrentPlayer { get { return CurrentPlayer; } set { CurrentPlayer = value; } }

    public event IEnemyEvents.EnemyEvent OnSpawn;
    public event IEnemyEvents.EnemyEvent OnAttack;
    public event IEnemyEvents.EnemyEvent OnDestroy;

    #region Events

    protected void InvockOnSpawn()
    {
        if (OnSpawn != null)
            OnSpawn(this);
    }

    protected void InvockOnAttack()
    {
        if (OnAttack != null)
            OnAttack(this);
    }

    protected void InvockOnDestroy()
    {
        if (OnDestroy != null)
            OnDestroy(this);
    }

    #endregion

    #region Combat

    public virtual void Spawn()
    {
        IsAlive = true;
        CurrentState = IEnemyState.InUse;
        InvockOnSpawn();
    }

    public virtual void DamagePlayer(Player player)
    {
        InvockOnAttack();
        CurrentPlayer = player;
        if (CurrentState == IEnemyState.InUse)           
            player.LoseRound();
    }

    public virtual void DestroyMe()
    {
        CurrentState = IEnemyState.Destroying;
        InvockOnDestroy();
        ResetStatistic();
        DestroyVisualEffect();
    }

    private void DestroyVisualEffect()
    {
        CurrentState = IEnemyState.InPool;
    }

    public virtual void LoseRound()
    {
        Attack = 0;
        CurrentPlayer.Attacks = 0;
        CurrentPlayer.CombatPoint++;
        if (CurrentPlayer.CombatPoint == 2)
        {
            IsAlive = false;
            CurrentPlayer.CombatPoint = 0;
            DestroyMe();
        }
    }

    private void Start()
    {
        Gpm = FindObjectOfType<GamePlayManager>();
        PGreen = GameObject.Find("PedoneGreen");
        PBlue = GameObject.Find("PedoneBlue");
        PRed = GameObject.Find("PedoneRed");
        PYellow = GameObject.Find("PedoneYellow");
        CB = FindObjectOfType<CombatManager>();

    }

    public virtual void ResetStatistic()
    {
        WinPoint = 0;
        Attack = 0;
    }

    private void Update()
    {
        switch (PlayerToAttack)
        {
            case "Green":
                CurrentPlayer = PGreen.GetComponent<Player>();
                break;
            case "Blue":
                CurrentPlayer = PBlue.GetComponent<Player>();
                break;
            case "Red":
                CurrentPlayer = PRed.GetComponent<Player>();
                break;
            case "Yellow":
                CurrentPlayer = PYellow.GetComponent<Player>();
                break;
            default:
                break;
        }

        CurrentPlayer = GetComponent<Player>();

        
    }

    #endregion
}

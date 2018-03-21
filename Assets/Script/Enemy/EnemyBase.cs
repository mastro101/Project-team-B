using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IEnemy {

    protected IEnemyState _currentState = IEnemyState.InPool;

    public int Stamina;
    public int CombatPoints;
    public int Credits;
    public int Attack;
    public bool IsAlive;
    public string PlayerToAttack;
    public GameObject PGreen, PBlue, PRed, PYellow;
    public GamePlayManager Gpm;
    Player CurrentPlayer;

    int _stamina;

    public int ID { get { return getID(); } }

    protected abstract int getID();

    public IEnemyState CurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }

    int IEnemy.Attack { get { return Attack; } }

    int IEnemy.Credits { get { return Credits; } }

    int IEnemy.CombatPoints { get { return CombatPoints; } }

    bool IEnemy.IsAlive { get { return IsAlive; } }

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

    public virtual void AttackPlayer(Player player)
    {
        InvockOnAttack();
        if (CurrentState == IEnemyState.InUse)           
            player.TakeDamage(Attack);
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

    public virtual void TakeDamage(int damage)
    {
        Stamina -= damage;
        if (Stamina <= 0)
        {
            IsAlive = false;
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

        _stamina = Stamina;
    }

    public virtual void ResetStatistic()
    {
        Stamina = _stamina;
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

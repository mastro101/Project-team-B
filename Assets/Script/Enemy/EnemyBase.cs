using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IEnemy {

    protected IEnemyState _currentState = IEnemyState.InPool;

    public CombatManager CB;

    
    public int Credits;
    public int Attack;
    public int Damage;
    public int Materiali;
    public bool IsAlive;
    public string PlayerToAttack;
    public GameObject PGreen, PBlue, PRed, PYellow;
    public GamePlayManager Gpm;
    public Player CurrentPlayer;
    public IEnemyType Type;

    public int WinPoint;

    public int ID { get { return getID(); } }

    protected abstract int getID();

    public IEnemyState CurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }

    int IEnemy.Damage { get { return Damage; } }

    int IEnemy.Materiali { get { return Materiali; } }

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
        int random = UnityEngine.Random.Range(0, 5);
        switch (random)
        {
            case 1:
                Type = IEnemyType.Green;
                break;
            case 2:
                Type = IEnemyType.Blue;
                break;
            case 3:
                Type = IEnemyType.Red;
                break;
            case 4:
                Type = IEnemyType.Yellow;
                break;
            default:
                break;
        }
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
        if (CurrentPlayer.CombatPoint == 1)
        {
            IsAlive = false;
            switch (Type)
            {
                case IEnemyType.Green:
                    CurrentPlayer.AddMaterial(0, Materiali);
                    CurrentPlayer.Lg.SetTextLog("Guadagnato " + Materiali.ToString() + " M1", true);
                    break;
                case IEnemyType.Blue:
                    CurrentPlayer.AddMaterial(1, Materiali);
                    CurrentPlayer.Lg.SetTextLog("Guadagnato " + Materiali.ToString() + " M2", true);
                    break;
                case IEnemyType.Red:
                    CurrentPlayer.AddMaterial(2, Materiali);
                    CurrentPlayer.Lg.SetTextLog("Guadagnato " + Materiali.ToString() + " M3", true);
                    break;
                case IEnemyType.Yellow:
                    CurrentPlayer.AddMaterial(3, Materiali);
                    CurrentPlayer.Lg.SetTextLog("Guadagnato " + Materiali.ToString() + " M4", true);
                    break;
                default:
                    Debug.Log("Non Ha dato materiali. Colore assente");
                    break;
            }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{

    public string Name="Green";

    public ButtonManager buttonManager;
    public DetectObject DO;
    public EnemyBase Enemy;

    public enum PlayerTurn
    {
        P1,
        P2,
        P3,
        P4,
    }


    public enum State
    {
        Mission,
        Movement,
        Event,
        City,
        Combat,
        Debug,
        End,
    }

    public enum CombatState
    {
        Choose,
        Animation,
        Check,
        NotCombat,
    }

    public enum EventState
    {
        NotEvent,
        Event,
        Animation,
    }

    public CombatState CurrentCombatState;
    public EventState CurrentEventState;

    public PlayerTurn CurrentTurn
    {
        get
        {
            return _currentTurn;
        }
        set
        {
            if (CheckTurnChange(value) == true)
            {
                OnTurnEnd(_currentTurn);
                _currentTurn = value;
                OnTurnStart(_currentTurn);
            }
            else
            {
                CurrentTurn = PlayerTurn.P1;
                Debug.Log("Nope Turn");
            }
                

        }
    }

    public State CurrentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            if (CheckStateChange(value) == true)
            {
                OnStateEnd(_currentState);
                _currentState = value;
                OnStateStart(_currentState);
            }
            else
                Debug.Log("Nope State");
        }
    }

    private State _currentState;
    private PlayerTurn _currentTurn;

    #region StateMachine
    /// <summary>
    /// Chiamatoi dopo aver settato un nuovo state come current <paramref name="newState"/>
    /// </summary>
    /// <param name="newState"></param>
    void OnStateStart(State newState)
    {
        switch (newState)
        {
            case State.Mission:
                Debug.Log("Enter" + CurrentState);
                break;
            case State.Movement:
                Debug.Log("Enter" + CurrentState);
                break;
            case State.Event:
                Debug.Log("Enter" + CurrentState);
                break;
            case State.City:
                Debug.Log("Enter" + CurrentState);
                break;
            case State.Combat:
                Debug.Log("Enter" + CurrentState);
                break;
            case State.End:
                Debug.Log("Enter" + CurrentState);
                break;
            default:
                break;
        }
    }

    void OnTurnStart(PlayerTurn newTurn)
    {
        switch (newTurn)
        {
            case PlayerTurn.P1:
                Debug.Log("Enter" + CurrentTurn);
                break;
            case PlayerTurn.P2:
                Debug.Log("Enter" + CurrentTurn);
                break;
            case PlayerTurn.P3:
                Debug.Log("Enter" + CurrentTurn);
                break;
            case PlayerTurn.P4:
                Debug.Log("Enter" + CurrentTurn);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Controlla in che current sei
    /// </summary>
    void OnStateUpdate()
    {
        switch (CurrentState)
        {
            case State.Movement:
                Debug.Log("Movement");
                break;
            case State.Event:
                Debug.Log("Event");
                break;
            case State.City:
                Debug.Log("Object");
                break;
            case State.Combat:
                Debug.Log("Combat");
                break;
            case State.End:
                Debug.Log("End");
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Chiamata prima di uscire dallo stato <paramref name="oldState"/>
    /// </summary>
    /// <param name="oldState"></param>
    void OnStateEnd(State oldState)
    {
        switch (oldState)
        {
            case State.Mission:
                Debug.Log("Exit" + CurrentState);
                break;
            case State.Movement:
                Debug.Log("Exit" + CurrentState);
                break;
            case State.Event:
                Debug.Log("Exit" + CurrentState);
                break;
            case State.City:
                Debug.Log("Exit" + CurrentState);
                break;
            case State.Combat:
                Debug.Log("Exit" + CurrentState);
                break;
            case State.End:
                Debug.Log("Exit" + CurrentState);
                break;
            default:
                break;
        }
    }

    void OnTurnEnd(PlayerTurn oldTurn)
    {
        switch (oldTurn)
        {
            case PlayerTurn.P1:
                Debug.Log("Exit" + CurrentTurn);
                break;
            case PlayerTurn.P2:
                Debug.Log("Exit" + CurrentTurn);
                break;
            case PlayerTurn.P3:
                Debug.Log("Exit" + CurrentTurn);
                break;
            case PlayerTurn.P4:
                Debug.Log("Exit" + CurrentTurn);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Controlla se è possibile cambiare lo stato in quello richiesto in <paramref name="newState"/>
    /// </summary>
    /// <param name="newState"></param>
    /// <returns></returns>
    bool CheckStateChange(State newState)
    {
        switch (newState)
        {
            case State.Mission:
                if (CurrentState != State.End)
                    return false;
                return true;
            case State.Movement:
                if (CurrentState == State.End || CurrentState == State.Mission || CurrentState == State.Event)
                    return true;
                return false;
            case State.Event:
                if (CurrentState != State.Movement)
                    return false;
                return true;
            case State.City:
                if (CurrentState != State.Event)
                    return false;
                return true;
            case State.Combat:
                if (CurrentState != State.Event)
                    return false;
                return true;
            case State.Debug:
                return true;
            case State.End:
                if (CurrentState == State.Debug || CurrentState == State.Event || CurrentState == State.City || CurrentState == State.Combat || CurrentState == State.Mission || CurrentState == State.Movement)
                    return true;
                return false;
            default:
                return false;
        }
    }

    bool CheckTurnChange(PlayerTurn newTurn)
    {
        switch (newTurn)
        {
            case PlayerTurn.P1:
                if (CurrentTurn != PlayerTurn.P4)
                    return false;
                return true;
            case PlayerTurn.P2:
                if (CurrentTurn != PlayerTurn.P1)
                    return false;
                return true;
            case PlayerTurn.P3:
                if (CurrentTurn != PlayerTurn.P2)
                    return false;
                return true;
            case PlayerTurn.P4:
                if (CurrentTurn != PlayerTurn.P3)
                    return false;
                return true;
            default:
                return false;
        }
    }
    #endregion

    private void Start()
    {
        CurrentState = State.Mission;
    }

    private void Update()
    {
        

        if (CurrentState == State.End)
        {
            DO.CorrectMove = false;
            buttonManager.EndP.SetActive(false);

            PlayerTurn _playerTurn = CurrentTurn;

            CurrentTurn = _playerTurn + 1;

            switch (CurrentTurn) {
                case PlayerTurn.P1:
                    Name = "Green";
                    DO.NameDO = "Green";
                    Enemy.PlayerToAttack = "Green";
                    break;
                case PlayerTurn.P2:
                    Name = "Blue";
                    DO.NameDO = "Blue";
                    Enemy.PlayerToAttack = "Blue";
                    break;
                case PlayerTurn.P3:
                    Name = "Red";
                    DO.NameDO = "Red";
                    Enemy.PlayerToAttack = "Red";
                    break;
                case PlayerTurn.P4:
                    Name = "Yellow";
                    DO.NameDO = "Yellow";
                    Enemy.PlayerToAttack = "Yellow";
                    break;
            }



            CurrentState = State.Mission;
        }

        //OnStateUpdate();
    }
}
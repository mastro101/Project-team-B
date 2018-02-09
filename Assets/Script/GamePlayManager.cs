using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{

    public string Name="Green";


    public enum PlayerTurn
    {
        P1,
        P2,
        P3,
        P4,
    }


    public enum State
    {
        Movement,
        Event,
        End,
    }

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
                OnStateEnd(_currentState);
                _currentTurn = value;
                OnStateStart(_currentState);
            }
            else
            {
                CurrentTurn = PlayerTurn.P1;
                Debug.Log("Nope");
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
                Debug.Log("Nope");
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
            case State.Movement:
                Debug.Log("Enter" + CurrentState);
                break;
            case State.Event:
                Debug.Log("Enter" + CurrentState);
                break;
            case State.End:
                Debug.Log("Enter" + CurrentState);
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
            case State.Movement:
                Debug.Log("Exit" + CurrentState);
                break;
            case State.Event:
                Debug.Log("Exit" + CurrentState);
                break;
            case State.End:
                Debug.Log("Exit" + CurrentState);
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
            case State.Movement:
                if (CurrentState != State.End)
                    return false;
                return true;
            case State.Event:
                if (CurrentState != State.Movement)
                    return false;
                return true;
            case State.End:
                if (CurrentState != State.Event)
                    return false;
                return true;
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
        CurrentState = State.Movement;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            CurrentState = State.Movement;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            CurrentState = State.Event;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            CurrentState = State.End;

        if (CurrentState == State.End)
        {
            PlayerTurn _playerTurn = CurrentTurn;

            CurrentTurn = _playerTurn + 1;

            switch (CurrentTurn) {
                case PlayerTurn.P1:
                    Name = "Green";
                    break;
                case PlayerTurn.P2:
                    Name = "Yellow";
                    break;
                case PlayerTurn.P3:
                    Name = "Blue";
                    break;
                case PlayerTurn.P4:
                    Name = "Red";
                   
                    break;
            }

            if (CurrentTurn == PlayerTurn.P4)


            Debug.Log(CurrentTurn);
            CurrentState = State.Movement;
        }



        OnStateUpdate();
    }
}
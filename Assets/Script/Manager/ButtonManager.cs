﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    public GameObject PanelConsole;
    bool ActiveConsole = true;

    Vector3 ConsolPosition;

    public GamePlayManager Gpm;
    public GameObject EndP;
    public Player Pl1, Pl2, Pl4, Pl3;

    private void Start()
    {
        ConsolPosition = PanelConsole.transform.position;
        PanelConsole.transform.position += new Vector3(800f, 0f, 0f);
    }


    public void OpenConsole() {
        if (!ActiveConsole)
        {
            //PanelConsole.SetActive(true);
            PanelConsole.transform.position += new Vector3(800f, 0f, 0f);
            ActiveConsole = true;

        }
        else
        {
            //PanelConsole.SetActive(false);
            PanelConsole.transform.position = ConsolPosition;
            ActiveConsole = false;

        }
    }

    public void HealLife()
    {
        switch (Gpm.Name)
        {
            case "Green":
                Pl1.Life = 5;
                Pl1.Credit -= 3;
                break;
            case "Blue":
                Pl2.Life = 5;
                Pl2.Credit -= 3;
                break;
            case "Red":
                Pl3.Life = 5;
                Pl3.Credit -= 3;
                break;
            case "Yellow":
                Pl4.Life = 5;
                Pl4.Credit -= 3;
                break;
        }
        Gpm.CurrentState = GamePlayManager.State.End;
    }

    public void NoHeal()
    {
        Gpm.CurrentState = GamePlayManager.State.End;
    }

    public void EndPhase() {
        switch (Gpm.Name)
        {
            case "Green":
                if (Pl1.jumperEvent == true)
                {
                    Pl1.jumperEvent = false;
                    Gpm.CurrentState = GamePlayManager.State.End;
                }
                break;
            case "Blue":
                if (Pl2.jumperEvent == true)
                {
                    Pl2.jumperEvent = false;
                    Gpm.CurrentState = GamePlayManager.State.End;
                }
                break;
            case "Red":
                if (Pl3.jumperEvent == true)
                {
                    Pl3.jumperEvent = false;
                    Gpm.CurrentState = GamePlayManager.State.End;
                }
                break;
            case "Yellow":
                if (Pl4.jumperEvent == true)
                {
                    Pl4.jumperEvent = false;
                    Gpm.CurrentState = GamePlayManager.State.End;                    
                }
                    
                break;
        }
        Gpm.CurrentState = GamePlayManager.State.Event;
        switch (Gpm.Name) {
            case "Green":
                Pl1.PossibleMove = 4;
                break;
            case "Blue":
                Pl2.PossibleMove = 4;
                break;
            case "Red":
                Pl3.PossibleMove = 4;
                break;
            case "Yellow":
                Pl4.PossibleMove = 4;
                break;
        }
        EndP.SetActive(false);
    }
}

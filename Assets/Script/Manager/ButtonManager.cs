using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public GameObject PanelConsole;
    bool ActiveConsole = true;
    public GameObject MainMenu;
    public GameObject Tutorial;
    bool ActiveMainMenu = true;
    Vector3 ConsolPosition;

    UIManager UI;
    TheGrid grid;
    public GamePlayManager Gpm;
    public GameObject EndP;
    public Player Pl1, Pl2, Pl4, Pl3;

    private void Start()
    {
        grid = FindObjectOfType<TheGrid>();
        UI = FindObjectOfType<UIManager>();
        ConsolPosition = PanelConsole.transform.position;
        //PanelConsole.transform.position += new Vector3(800f, 0f, 0f);
    }


    public void OpenConsole() {
       //if (!ActiveConsole)
       //{
       //    //PanelConsole.SetActive(true);
       //    PanelConsole.transform.position += new Vector3(800f, 0f, 0f);
       //    ActiveConsole = true;
       //
       //}
       //else
       //{
       //    //PanelConsole.SetActive(false);
       //    PanelConsole.transform.position = ConsolPosition;
       //    ActiveConsole = false;
       //
       //}
        UI.Console.SetActive(false);
        Gpm.CurrentState = GamePlayManager.State.End;
    }

    public void OpenTutorial()
    {
        Tutorial.SetActive(true);
    }


    public void exitGame()
    {
        Application.Quit();
    }

    public void OpenMenu()
    {
        MainMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        MainMenu.SetActive(false);
    }

    public void gotoMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void SellMateriali(int _materialType)
    {
        switch (Gpm.Name)
        {
            case "Green":
                if (Pl1.Materiali[_materialType] >= 10)
                {
                    Pl1.Materiali[_materialType] -= 10;
                    Pl1.WinPoint++;
                }
                break;
            case "Blue":
                if (Pl2.Materiali[_materialType] >= 10)
                {
                    Pl2.Materiali[_materialType] -= 10;
                    Pl2.WinPoint++;
                }
                break;
            case "Red":
                if (Pl3.Materiali[_materialType] >= 10)
                {
                    Pl3.Materiali[_materialType] -= 10;
                    Pl3.WinPoint++;
                }
                break;
            case "Yellow":
                if (Pl4.Materiali[_materialType] >= 10)
                {
                    Pl4.Materiali[_materialType] -= 10;
                    Pl4.WinPoint++;
                }
                break;
        }
    }

    // Event


    public void Event1()
    {
        Gpm.CurrentEventState = GamePlayManager.EventState.Animation;
        switch (Gpm.Name)
        {
            case "Green":
                Pl1.EventList(Pl1.event1);
                if (Gpm.CurrentState != GamePlayManager.State.Combat && Pl1.jumperEvent == false)
                {
                    UI.UICardEvent.SetActive(false);
                    Pl1.neutralizeCell(Pl1.XPos, Pl1.ZPos);
                    Pl1.eventCard = 0;
                    Pl1.countdown = 2f;
                    Pl1.JustEmpted = true;
                }
                break;
            case "Blue":
                Pl2.EventList(Pl2.event1);
                if (Gpm.CurrentState != GamePlayManager.State.Combat && Pl2.jumperEvent == false)
                {
                    UI.UICardEvent.SetActive(false);
                    Pl2.neutralizeCell(Pl2.XPos, Pl2.ZPos);
                    Pl2.eventCard = 0;
                    Pl2.countdown = 2f;
                    Pl2.JustEmpted = true;
                }
                break;
            case "Red":
                Pl3.EventList(Pl3.event1);
                if (Gpm.CurrentState != GamePlayManager.State.Combat && Pl3.jumperEvent == false)
                {
                    UI.UICardEvent.SetActive(false);
                    Pl3.neutralizeCell(Pl3.XPos, Pl3.ZPos);
                    Pl3.eventCard = 0;
                    Pl3.countdown = 2f;
                    Pl3.JustEmpted = true;
                }
                break;
            case "Yellow":
                Pl4.EventList(Pl4.event1);
                if (Gpm.CurrentState != GamePlayManager.State.Combat && Pl4.jumperEvent == false)
                {
                    UI.UICardEvent.SetActive(false);
                    Pl4.neutralizeCell(Pl4.XPos, Pl4.ZPos);
                    Pl4.eventCard = 0;
                    Pl4.countdown = 2f;
                    Pl4.JustEmpted = true;
                }
                break;
        }
    }

    public void Event2()
    {
        Gpm.CurrentEventState = GamePlayManager.EventState.Animation;
        switch (Gpm.Name)
        {
            case "Green":
                Pl1.EventList(Pl1.event2);
                if (Gpm.CurrentState != GamePlayManager.State.Combat && Pl1.jumperEvent == false)
                {
                    UI.UICardEvent.SetActive(false);
                    Pl1.neutralizeCell(Pl1.XPos, Pl1.ZPos);
                    Pl1.eventCard = 0;
                    Pl1.countdown = 2f;
                    Pl1.JustEmpted = true;
                }
                break;
            case "Blue":
                Pl2.EventList(Pl2.event2);
                if (Gpm.CurrentState != GamePlayManager.State.Combat && Pl2.jumperEvent == false)
                {
                    UI.UICardEvent.SetActive(false);
                    Pl2.neutralizeCell(Pl2.XPos, Pl2.ZPos);
                    Pl2.eventCard = 0;
                    Pl2.countdown = 2f;
                    Pl2.JustEmpted = true;
                }
                break;
            case "Red":
                Pl3.EventList(Pl3.event2);
                if (Gpm.CurrentState != GamePlayManager.State.Combat && Pl3.jumperEvent == false)
                {
                    UI.UICardEvent.SetActive(false);
                    Pl3.neutralizeCell(Pl3.XPos, Pl3.ZPos);
                    Pl3.eventCard = 0;
                    Pl3.countdown = 2f;
                    Pl3.JustEmpted = true;
                }
                break;
            case "Yellow":
                Pl4.EventList(Pl4.event2);
                if (Gpm.CurrentState != GamePlayManager.State.Combat && Pl4.jumperEvent == false)
                {
                    UI.UICardEvent.SetActive(false);
                    Pl4.neutralizeCell(Pl4.XPos, Pl4.ZPos);
                    Pl4.eventCard = 0;
                    Pl4.countdown = 2f;
                    Pl4.JustEmpted = true;
                }
                break;
        }
    }

    void evento(Player _player)
    {
        UI.UICardEvent.SetActive(false);
        _player.neutralizeCell(_player.XPos, _player.ZPos);
        _player.eventCard = 0;
        _player.countdown = 2f;
        _player.JustEmpted = true;
    }
    //





    public void BuyMateriali(int _materialType)
    {
        switch (Gpm.Name)
        {
            case "Green":
                if (Pl1.Credit >= 2)
                {
                    Pl1.Materiali[_materialType]++;
                    Pl1.Credit -= 2;
                }
                break;
            case "Blue":
                if (Pl2.Credit >= 2)
                {
                    Pl2.Materiali[_materialType]++;
                    Pl2.Credit -= 2;
                }
                break;
            case "Red":
                if (Pl3.Credit >= 2)
                {
                    Pl3.Materiali[_materialType]++;
                    Pl3.Credit -= 2;
                }
                break;
            case "Yellow":
                if (Pl4.Credit >= 2)
                {
                    Pl4.Materiali[_materialType]++;
                    Pl4.Credit -= 2;
                }
                break;
        }
    }

    // Funzioni da mettere nei bottoni delle città

        // per la vendita di materiali in cambio di punti vittoria

    public void Sell()
    {
        switch (Gpm.Name)
        {
            case "Green":
                switch (grid.FindCell(Pl1.XPos, Pl1.ZPos).GetNameTile())
                {
                    case "A":
                        SellInCityA();
                        break;
                    case "B":
                        SellInCityB();
                        break;
                    case "C":
                        SellInCityC();
                        break;
                    case "D":
                        SellInCityD();
                        break;
                    default:
                        break;
                }
                break;
            case "Blue":
                switch (grid.FindCell(Pl2.XPos, Pl2.ZPos).GetNameTile())
                {
                    case "A":
                        SellInCityA();
                        break;
                    case "B":
                        SellInCityB();
                        break;
                    case "C":
                        SellInCityC();
                        break;
                    case "D":
                        SellInCityD();
                        break;
                    default:
                        break;
                }
                break;
            case "Red":
                switch (grid.FindCell(Pl3.XPos, Pl3.ZPos).GetNameTile())
                {
                    case "A":
                        SellInCityA();
                        break;
                    case "B":
                        SellInCityB();
                        break;
                    case "C":
                        SellInCityC();
                        break;
                    case "D":
                        SellInCityD();
                        break;
                    default:
                        break;
                }
                break;
            case "Yellow":
                switch (grid.FindCell(Pl4.XPos, Pl4.ZPos).GetNameTile())
                {
                    case "A":
                        SellInCityA();
                        break;
                    case "B":
                        SellInCityB();
                        break;
                    case "C":
                        SellInCityC();
                        break;
                    case "D":
                        SellInCityD();
                        break;
                    default:
                        break;
                }
                break;
        }
    }
    
    public void SellInCityA()
    {
        SellMateriali(0);
    }

    public void SellInCityB()
    {
        SellMateriali(1);
    }

    public void SellInCityC()
    {
        SellMateriali(2);
    }

    public void SellInCityD()
    {
        SellMateriali(3);
    }

        // Per Comprare materiali


    public void Buy()
    {
        switch (Gpm.Name)
        {
            case "Green":
                switch (grid.FindCell(Pl1.XPos, Pl1.ZPos).GetNameTile())
                {
                    case "A":
                        BuyInCityA();
                        break;
                    case "B":
                        BuyInCityB();
                        break;
                    case "C":
                        BuyInCityC();
                        break;
                    case "D":
                        BuyInCityD();
                        break;
                    default:
                        break;
                }
                break;
            case "Blue":
                switch (grid.FindCell(Pl2.XPos, Pl2.ZPos).GetNameTile())
                {
                    case "A":
                        BuyInCityA();
                        break;
                    case "B":
                        BuyInCityB();
                        break;
                    case "C":
                        BuyInCityC();
                        break;
                    case "D":
                        BuyInCityD();
                        break;
                    default:
                        break;
                }
                break;
            case "Red":
                switch (grid.FindCell(Pl3.XPos, Pl3.ZPos).GetNameTile())
                {
                    case "A":
                        BuyInCityA();
                        break;
                    case "B":
                        BuyInCityB();
                        break;
                    case "C":
                        BuyInCityC();
                        break;
                    case "D":
                        BuyInCityD();
                        break;
                    default:
                        break;
                }
                break;
            case "Yellow":
                switch (grid.FindCell(Pl4.XPos, Pl4.ZPos).GetNameTile())
                {
                    case "A":
                        BuyInCityA();
                        break;
                    case "B":
                        BuyInCityB();
                        break;
                    case "C":
                        BuyInCityC();
                        break;
                    case "D":
                        BuyInCityD();
                        break;
                    default:
                        break;
                }
                break;
        }
    }

    public void BuyInCityA()
    {
        BuyMateriali(1);
    }

    public void BuyInCityB()
    {
        BuyMateriali(2);
    }

    public void BuyInCityC()
    {
        BuyMateriali(3);
    }

    public void BuyInCityD()
    {
        BuyMateriali(0);
    }

    //

    public void HealLife()
    {
        switch (Gpm.Name)
        {
            case "Green":
                if (Pl1.Credit >= 6 && Pl1.Life != Pl1.MaxLife)
                {
                    Pl1.Life = Pl1.MaxLife;
                    Pl1.Credit -= 6;
                }
                break;
            case "Blue":
                if (Pl2.Credit >= 6 && Pl2.Life != Pl2.MaxLife)
                {
                    Pl2.Life = Pl2.MaxLife;
                    Pl2.Credit -= 6;
                }
                break;
            case "Red":
                if (Pl3.Credit >= 6 && Pl3.Life != Pl3.MaxLife)
                {
                    Pl3.Life = Pl3.MaxLife;
                    Pl3.Credit -= 6;
                }
                break;
            case "Yellow":
                if (Pl4.Credit >= 6 && Pl4.Life != Pl4.MaxLife)
                {
                    Pl4.Life = Pl4.MaxLife;
                    Pl4.Credit -= 6;
                }
                break;
        }
        //Gpm.CurrentState = GamePlayManager.State.End;
    }

    public void NoHeal()
    {
        //Gpm.CurrentState = GamePlayManager.State.End;
    }

    public void EndPhase() {

        if (Gpm.CurrentState == GamePlayManager.State.Event)
        {
            EndP.SetActive(false);
            UI.UICardEvent.SetActive(false);
            switch (Gpm.Name)
            {
                case "Green":
                    Pl1.eventCard = 0;
                    break;
                case "Blue":
                    Pl2.eventCard = 0;
                    break;
                case "Red":
                    Pl3.eventCard = 0;
                    break;
                case "Yellow":
                    Pl4.eventCard = 0;
                    break;
            }
            Gpm.CurrentState = GamePlayManager.State.End;
        }
        else if (Gpm.CurrentState == GamePlayManager.State.Movement)
        {
            Gpm.CurrentState = GamePlayManager.State.Event;
            switch (Gpm.Name)
            {
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
        }
        else if (Gpm.CurrentState == GamePlayManager.State.City)
        {
            UI.UICity.SetActive(false);
            Gpm.CurrentState = GamePlayManager.State.End;
        }
        //EndP.SetActive(false);
    }

    


}

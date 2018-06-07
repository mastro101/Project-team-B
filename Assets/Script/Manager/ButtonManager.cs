using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    public GameObject PanelConsole;
    bool ActiveConsole = true;

    Vector3 ConsolPosition;

    TheGrid grid;
    public GamePlayManager Gpm;
    public GameObject EndP;
    public Player Pl1, Pl2, Pl4, Pl3;

    private void Start()
    {
        grid = FindObjectOfType<TheGrid>();
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
                if (Pl1.Credit >= 6)
                {
                    Pl1.Life = Pl1.MaxLife;
                    Pl1.Credit -= 6;
                }
                break;
            case "Blue":
                if (Pl2.Credit >= 6)
                {
                    Pl2.Life = Pl2.MaxLife;
                    Pl2.Credit -= 6;
                }
                break;
            case "Red":
                if (Pl3.Credit >= 6)
                {
                    Pl3.Life = Pl3.MaxLife;
                    Pl3.Credit -= 6;
                }
                break;
            case "Yellow":
                if (Pl4.Credit >= 6)
                {
                    Pl4.Life = Pl4.MaxLife;
                    Pl4.Credit -= 6;
                }
                break;
        }
        Gpm.CurrentState = GamePlayManager.State.End;
    }

    public void NoHeal()
    {
        Gpm.CurrentState = GamePlayManager.State.End;
    }

    public void EndPhase() {

        if (Gpm.CurrentState == GamePlayManager.State.Event)
        {
            EndP.SetActive(false);
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
        //EndP.SetActive(false);
    }
}

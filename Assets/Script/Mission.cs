using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {

    public TheGrid grid;
    public LogManager Lg;
    public Player Player1, Player2, Player3, Player4;
    public GameObject WinScreen, p1ws, p2ws, p3ws, p4ws;
    bool end;


    public int MissioniComuni;
    public int EmptyCell;

    public bool[] check = new bool[4];




    private void Update()
    {
        /* era solo un Test WinScreen
         * 
        if (Input.GetKeyDown(KeyCode.H))
        {
            WinScreen.SetActive(true);
            p1ws.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            WinScreen.SetActive(true);
            p2ws.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            WinScreen.SetActive(true);
            p3ws.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            WinScreen.SetActive(true);
            p4ws.SetActive(true);
        }
        */

        if (Player1.WinPoint == 2)
        {
            WinScreen.SetActive(true);
            p1ws.SetActive(true);
            Lg.SetTextLog(Player1 + " HA VINTO", true);
        }
        else if (Player2.WinPoint == 2)
        {
            WinScreen.SetActive(true);
            p2ws.SetActive(true);
            Lg.SetTextLog(Player2 + " HA VINTO", true);
        }
        else if (Player3.WinPoint == 2)
        {
            WinScreen.SetActive(true);
            p3ws.SetActive(true);
            Lg.SetTextLog(Player3 + " HA VINTO", true);
        }
        else if (Player1.WinPoint == 2)
        {
            WinScreen.SetActive(true);
            p4ws.SetActive(true);
            Lg.SetTextLog(Player4 + " HA VINTO", true);
        }

        if (EmptyCell == 153)
        {
            if (Player1.WinPoint > Player2.WinPoint && Player1.WinPoint > Player3.WinPoint && Player1.WinPoint > Player4.WinPoint)
            {
                WinScreen.SetActive(true);
                p1ws.SetActive(true);
                Lg.SetTextLog(Player1 + " HA VINTO", true);
                end = true;
            }
            else if (Player2.WinPoint > Player1.WinPoint && Player2.WinPoint > Player3.WinPoint && Player2.WinPoint > Player4.WinPoint)
            {
                WinScreen.SetActive(true);
                p2ws.SetActive(true);
                Lg.SetTextLog(Player2 + " HA VINTO", true);
                end = true;
            }
            else if (Player3.WinPoint > Player1.WinPoint && Player3.WinPoint > Player2.WinPoint && Player3.WinPoint > Player4.WinPoint)
            {
                WinScreen.SetActive(true);
                p3ws.SetActive(true);
                Lg.SetTextLog(Player3 + " HA VINTO", true);
                end = true;
            }
            else if (Player4.WinPoint > Player1.WinPoint && Player4.WinPoint > Player2.WinPoint && Player4.WinPoint > Player3.WinPoint)
            {
                WinScreen.SetActive(true);
                p4ws.SetActive(true);
                Lg.SetTextLog(Player4 + " HA VINTO", true);
                end = true;
            }
        }

        if (EmptyCell == 153 && end == false)
        {
            int p1Point = countRisorse(Player1);
            int p2Point = countRisorse(Player2);
            int p3Point = countRisorse(Player3);
            int p4Point = countRisorse(Player4);

            if (Player1.WinPoint >= Player2.WinPoint && Player1.WinPoint >= Player3.WinPoint && Player1.WinPoint >= Player4.WinPoint)
            {
                if (p1Point > p2Point && p1Point > p3Point && p1Point > p4Point)
                {
                    WinScreen.SetActive(true);
                    p1ws.SetActive(true);
                    Lg.SetTextLog(Player1 + " HA VINTO", true);
                }
            }
            else if (Player1.WinPoint >= Player2.WinPoint && Player1.WinPoint >= Player3.WinPoint && Player1.WinPoint >= Player4.WinPoint)
            {
                if (p2Point > p1Point && p2Point > p3Point && p2Point > p4Point)
                {
                    WinScreen.SetActive(true);
                    p2ws.SetActive(true);
                    Lg.SetTextLog(Player2 + " HA VINTO", true);
                }
            }
            else if (Player3.WinPoint >= Player1.WinPoint && Player3.WinPoint >= Player2.WinPoint && Player3.WinPoint >= Player4.WinPoint)
            {
                if (p3Point > p1Point && p3Point > p2Point && p3Point > p4Point)
                {
                    WinScreen.SetActive(true);
                    p3ws.SetActive(true);
                    Lg.SetTextLog(Player3 + " HA VINTO", true);
                }
            }
            else if (Player4.WinPoint >= Player1.WinPoint && Player4.WinPoint >= Player2.WinPoint && Player4.WinPoint >= Player3.WinPoint)
            {
                if (p4Point > p1Point && p4Point > p2Point && p4Point > p3Point)
                {
                    WinScreen.SetActive(true);
                    p4ws.SetActive(true);
                    Lg.SetTextLog(Player4 + " HA VINTO", true);
                }
            }
        }
        /*if (grid.GetCity("A").POnTile != null && grid.GetCity("A").POnTile.Mission == 1)
        {
            if (!check[0])
                Lg.SetTextLog("Now go to B", true);
            check[0] = true;
            
        }
        if (grid.GetCity("B").POnTile != null && check[0] == true && grid.GetCity("B").POnTile.Mission == 1)
        {
            Lg.SetTextLog("Missione completata", true);
        }
        
 
        if (grid.GetCity("B").POnTile != null && grid.GetCity("B").POnTile.Mission == 2)
        {
            if (!check[1])
                Lg.SetTextLog("Now go to C", true);
            check[1] = true;

        }
        if (check[1] == true && grid.GetCity("C").POnTile != null && grid.GetCity("C").POnTile.Mission == 2)
        {
            Lg.SetTextLog("Missione Comune completata", true);

        }
        


        if (grid.GetCity("C").POnTile != null && grid.GetCity("C").POnTile.Mission == 3)
        {
            if (!check[2])
                Lg.SetTextLog("Now go to D", true);
            check[2] = true;
            
        }
        if (check[2] == true && grid.GetCity("D").POnTile != null && grid.GetCity("D").POnTile.Mission == 3)
        {
            Lg.SetTextLog("Missione Comune completata", true);

        }

   
        if (grid.GetCity("D").POnTile != null && grid.GetCity("D").POnTile.Mission == 4)
        {
            if (!check[3])
                Lg.SetTextLog("Now go to A", true);
            check[3] = true;

        }
        if (check[3] == true && grid.GetCity("A").POnTile != null && grid.GetCity("A").POnTile.Mission == 4)
        {
            Lg.SetTextLog("Missione Comune completata", true);

        }*/

    }

    int countRisorse(Player _player)
    {
        int i = _player.Credit + _player.Materiali[0] + _player.Materiali[1] + _player.Materiali[2] + _player.Materiali[3];
        return i;
    }


}

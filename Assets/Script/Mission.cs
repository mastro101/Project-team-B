using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {

    public TheGrid grid;
    public LogManager Lg;
    public Player Player1, Player2, Player3, Player4;

    public int MissioniComuni;

    public bool[] check = new bool[4];




    private void Update()
    {
        if (Player1.WinPoint == 4)
        {
            Lg.SetTextLog(Player1 + " HA VINTO", true);
        }
        else if (Player2.WinPoint == 4)
        {
            Lg.SetTextLog(Player2 + " HA VINTO", true);
        }
        else if (Player3.WinPoint == 4)
        {
            Lg.SetTextLog(Player3 + " HA VINTO", true);
        }
        else if (Player1.WinPoint == 4)
        {
            Lg.SetTextLog(Player4 + " HA VINTO", true);
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
        
    


}

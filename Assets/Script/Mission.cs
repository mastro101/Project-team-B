﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {

    public Grid grid;
    public LogManager Lg;


    public int MissioniComuni;
    




    private void Update()
    {
        bool[] check = new bool[4];
        
        if (grid.GetCity("A").POnTile != null && grid.GetCity("A").POnTile.Mission == 0)
        {
            check[0] = true;
            Lg.SetTextLog("Now go to B", true);
        }
        if (grid.GetCity("B").POnTile != null && check[0] == true && grid.GetCity("B").POnTile.Mission == 0)
        {
            Lg.SetTextLog("Missione Comune completata", true);

        }
        
 
        if (grid.GetCity("B").POnTile != null && grid.GetCity("B").POnTile.Mission == 1)
        {
            check[1] = true;
            Lg.SetTextLog("Now go to C", true);
        }
        if (check[1] == true && grid.GetCity("C").POnTile != null && grid.GetCity("C").POnTile.Mission == 1)
        {
            Lg.SetTextLog("Missione Comune completata", true);

        }
        


        if (grid.GetCity("C").POnTile != null && grid.GetCity("C").POnTile.Mission == 2)
        {
            check[2] = true;
            Lg.SetTextLog("Now go to D", true);
        }
        if (check[2] == true && grid.GetCity("D").POnTile != null && grid.GetCity("D").POnTile.Mission == 2)
        {
            Lg.SetTextLog("Missione Comune completata", true);

        }

   
        if (grid.GetCity("D").POnTile != null && grid.GetCity("D").POnTile.Mission == 3)
        {
            check[3] = true;
            Lg.SetTextLog("Now go to A", true);
        }
        if (check[3] == true && grid.GetCity("A").POnTile != null && grid.GetCity("A").POnTile.Mission == 3)
        {
            Lg.SetTextLog("Missione Comune completata", true);

        }
        
    }
        
    


}

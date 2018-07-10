using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {

    public TheGrid grid;
    public LogManager Lg;
    public Player Player1, Player2, Player3, Player4;
    public GameObject WinScreen, p1ws, p2ws, p3ws, p4ws, Draw;
    GamePlayManager Gpm;
    bool realEnd, end, win;


    public int MissioniComuni;
    public int EmptyCell;

    public bool[] check = new bool[4];

    private void Awake()
    {
        Gpm = FindObjectOfType<GamePlayManager>();
    }


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
        if (win && !realEnd)
        {
            SoundManager sound = FindObjectOfType<SoundManager>();
            sound.audioSource.clip = sound.WinAudio;
            sound.audioSource.Play();
            realEnd = true;
        }

        if (Gpm.CurrentState == GamePlayManager.State.Mission)
        {
            EmptyCell = 0;
            for (int x = 0; x < 13; x++)
            {
                for (int z = 0; z < 13; z++)
                {
                    if (grid.FindCell(x, z).GetNameTile() == "Empty")
                    {
                        EmptyCell++;
                    }

                }
            }
            Debug.Log(EmptyCell);

            if (Player1.WinPoint == 2)
            {
                WinScreen.SetActive(true);
                p1ws.SetActive(true);
                win = true;
            }
            else if (Player2.WinPoint == 2)
            {
                WinScreen.SetActive(true);
                p2ws.SetActive(true);
                win = true;
            }
            else if (Player3.WinPoint == 2)
            {
                WinScreen.SetActive(true);
                p3ws.SetActive(true);
                win = true;
            }
            else if (Player1.WinPoint == 2)
            {
                WinScreen.SetActive(true);
                p4ws.SetActive(true);
                win = true;
            }

            if (EmptyCell == 153)
            {
                if (Player1.WinPoint > Player2.WinPoint && Player1.WinPoint > Player3.WinPoint && Player1.WinPoint > Player4.WinPoint)
                {
                    WinScreen.SetActive(true);
                    p1ws.SetActive(true);
                    end = true;
                    win = true;
                }
                else if (Player2.WinPoint > Player1.WinPoint && Player2.WinPoint > Player3.WinPoint && Player2.WinPoint > Player4.WinPoint)
                {
                    WinScreen.SetActive(true);
                    p2ws.SetActive(true);
                    end = true;
                    win = true;
                }
                else if (Player3.WinPoint > Player1.WinPoint && Player3.WinPoint > Player2.WinPoint && Player3.WinPoint > Player4.WinPoint)
                {
                    WinScreen.SetActive(true);
                    p3ws.SetActive(true);
                    end = true;
                    win = true;
                }
                else if (Player4.WinPoint > Player1.WinPoint && Player4.WinPoint > Player2.WinPoint && Player4.WinPoint > Player3.WinPoint)
                {
                    WinScreen.SetActive(true);
                    p4ws.SetActive(true);
                    end = true;
                    win = true;
                }
            }

            if (EmptyCell == 153 && end == false)
            {
                int p1Point = 0;
                int p2Point = 0;
                int p3Point = 0;
                int p4Point = 0;
                bool p1tie = false;
                bool p2tie = false;
                bool p3tie = false;
                bool p4tie = false;

                if (Player1.WinPoint >= Player2.WinPoint && Player1.WinPoint >= Player3.WinPoint && Player1.WinPoint >= Player4.WinPoint)
                {
                    p1Point = countRisorse(Player1);
                    p1tie = true;
                }
                if (Player2.WinPoint >= Player1.WinPoint && Player2.WinPoint >= Player3.WinPoint && Player2.WinPoint >= Player4.WinPoint)
                {
                    p2Point = countRisorse(Player2);
                    p2tie = true;
                }
                if (Player3.WinPoint >= Player1.WinPoint && Player3.WinPoint >= Player2.WinPoint && Player3.WinPoint >= Player4.WinPoint)
                {
                    p3Point = countRisorse(Player3);
                    p3tie = true;
                }
                if (Player4.WinPoint >= Player1.WinPoint && Player4.WinPoint >= Player2.WinPoint && Player4.WinPoint >= Player3.WinPoint)
                {
                    p4Point = countRisorse(Player4);
                    p4tie = true;
                }

                if (p1tie)
                {
                    if (p1Point > p2Point && p1Point > p3Point && p1Point > p4Point)
                    {
                        WinScreen.SetActive(true);
                        p1ws.SetActive(true);
                        win = true;
                    }
                }
                if (p2tie)
                {
                    if (p2Point > p1Point && p2Point > p3Point && p2Point > p4Point)
                    {
                        WinScreen.SetActive(true);
                        p2ws.SetActive(true);
                        win = true;
                    }
                }
                if (p3tie)
                {
                    if (p3Point > p1Point && p3Point > p2Point && p3Point > p4Point)
                    {
                        WinScreen.SetActive(true);
                        p3ws.SetActive(true);
                        win = true;
                    }
                }
                if (p4tie)
                {
                    if (p4Point > p1Point && p4Point > p2Point && p4Point > p3Point)
                    {
                        WinScreen.SetActive(true);
                        p4ws.SetActive(true);
                        win = true;
                    }
                }
                if ((p1tie || p2tie || p3tie || p4tie) && !win)
                {
                    if (p1Point == p2Point || p1Point == p3Point || p1Point == p4Point || p2Point == p3Point || p2Point == p4Point || p3Point == p4Point)
                    {
                        WinScreen.SetActive(true);
                        Draw.SetActive(true);
                    }
                }
                

                
            }

            if (!win)
                Gpm.CurrentState = GamePlayManager.State.Movement;
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
        int i = _player.Credit;
        return i;
    }


}

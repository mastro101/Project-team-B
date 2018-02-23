 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : PlayerStatistiche{

    public Grid grid;
    public DetectObject detectObject;
    public GamePlayManager Gpm;

    public  TextMeshP Tmp;

    public LogManager Lg;


    public int XPos;    //Posizione X del Player sulla casella
    public int ZPos;    //Posizione Z del Player sulla casella


    int XPos_old;
    int ZPos_old;

    int DistanceMove=1; // Di quanto il giocatore si Muove

    public float _Yoffset;


    void Start()
    {
        SetPositionPlayer();
        transform.position = grid.GetCenterPosition(); //Setto la posizione del player
        transform.position += new Vector3(0f, _Yoffset, 0f);   //Fix posizione Y del player
        CheckMissions = new int[4];
    }


    void Update()
    {
        //MainMove();   //Movimento del PLayer tramite WASD
        
        if (Name == Gpm.Name)
        {
            Tmp.SetMission(Mission.ToString());
            Tmp.SetLife(Life.ToString());
            Tmp.SetCredits(Credit.ToString());
            Tmp.SetName(Gpm.Name);

            if (Gpm.CurrentState == GamePlayManager.State.Mission)
            {
                if (CheckMission && Mission != 0)
                    Gpm.CurrentState = GamePlayManager.State.Movement;
                if (!CheckMission)
                {
                    // Assegnata missione casuale diversa da quella di un altro giocatore
                    Debug.Log("M");
                    do
                    {
                        Mission = Random.Range(1, 10);
                    }
                    while (Mission == CheckMissions[0] || Mission == CheckMissions[1] || Mission == CheckMissions[2] || Mission == CheckMissions[3]);

                    switch (Name)
                    {
                        case "Green":
                            CheckMissions[0] = Mission;
                            Debug.Log(CheckMissions[0]);
                            break;
                        case "Blue":
                            CheckMissions[1] = Mission;
                            Debug.Log(CheckMissions[1]);
                            break;
                        case "Red":
                            CheckMissions[2] = Mission;
                            Debug.Log(CheckMissions[2]);
                            break;
                        case "Yellow":
                            CheckMissions[3] = Mission;
                            Debug.Log(CheckMissions[3]);
                            break;
                    }


                    Lg.SetTextLog("Missione assegnata a: " + Name, true);
                    CheckMission = true;
                    Gpm.CurrentState = GamePlayManager.State.End;
                }
                

            }
            else if (Gpm.CurrentState == GamePlayManager.State.Movement)
            {
                MainMove2();    //Movimento del Player tramite Click
                MainMove3();
            }
            else if (Gpm.CurrentState == GamePlayManager.State.Event)
            {

                Lg.SetTextLog(Name + ": Carta evento pescata", true);

                //Controllo in che tipo di casella mi trovo
                if (grid.FindCell(XPos, ZPos).GetNameTile() != "" && grid.FindCell(XPos, ZPos).GetNameTile() != "Enemy")
                {
                    Debug.Log(Name + " si trova nella città: " + grid.FindCell(XPos, ZPos).GetNameTile());
                    Lg.SetTextLog(Name + " si trova nella città: " + grid.FindCell(XPos, ZPos).GetNameTile(), true);
                }
                else if (grid.FindCell(XPos, ZPos).GetNameTile() == "Enemy")
                {
                    Lg.SetTextLog(Name + " si trova in una casella Nemico",true);
                }

                // Fase successiva
                Gpm.CurrentState = GamePlayManager.State.End;
            }


            // Morte
            if (Input.GetKeyDown(KeyCode.A))
            {
                Life--;

                Lg.SetTextLog(Name+" ha perso vita",true);
                
                
                
            }
            if (Life <= 0)
            {
                transform.position = grid.GetCenterPosition();
                transform.position += new Vector3(0f, _Yoffset, 0f);
                XPos = 6;
                ZPos = 6;
                Life = 5;
                Gpm.CurrentState = GamePlayManager.State.Event;
                Lg.SetTextLog(Name + " è morto ed è tornato al centro", true);

            }

        }
        //playerStatistiche.SetDistace(Name, DistanceMove);   //Setto il movimento del player // Da rivedere in futuro
    }

    //Movimento del Player
    void Move()
    {
        if (grid.IsValidPosition(XPos, ZPos))
        {
            Vector3 globalPosition = grid.GetWorldPosition(XPos, ZPos);
            globalPosition += new Vector3(0f, _Yoffset, 0f); ;
            transform.DOMove(globalPosition, 0.6f).SetEase(Ease.Linear);
            //grid.FindCell(XPos, ZPos).SetValidity(false);
            detectObject.CorrectMove = false;

            // Finito il movimento passa alla fase successiva
            Gpm.CurrentState = GamePlayManager.State.Event;

            Lg.SetTextLog(Name + " si è mosso (" +XPos+"-"+ZPos+")" , true);
                
        }
        else
        {
                XPos = XPos_old;
                ZPos = ZPos_old;
        }

    }


    //Movimento tramite click destro del mouse
    void MainMove2() {
        //Debug.Log(detectObject.GetX() + " - " + detectObject.GetZ());
        int ObjectX = detectObject.GetX();
        int ObjectZ = detectObject.GetZ();

        DistanceMove = 1;
        //DistanceMove = playerStatistiche.GetDistance();
        if (detectObject.CorrectMove == true && grid.FindCell(ObjectX, ObjectZ).GetValidity()) {

            if (ObjectX == XPos && ObjectZ - 1 == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[2] != true)
            { //SU
                grid.FindCell(XPos, ZPos).SetValidity(true);

                XPos_old = XPos;
                ZPos_old = ZPos;

                ZPos += DistanceMove;
                Move();
            }
            else if (ObjectX == XPos && ObjectZ + 1 == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[0] != true)
            { //GIU

                grid.FindCell(XPos, ZPos).SetValidity(true);

                XPos_old = XPos;
                ZPos_old = ZPos;

                ZPos -= DistanceMove;
                Move();
            }
            else if (ObjectX + 1 == XPos && ObjectZ == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[1] != true)
            { //SINISTRA

                grid.FindCell(XPos, ZPos).SetValidity(true);

                XPos_old = XPos;
                ZPos_old = ZPos;

                XPos -= DistanceMove;
                Move();
            }
            else if (ObjectX - 1 == XPos && ObjectZ == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[3] != true)
            { //DESTRA

                grid.FindCell(XPos, ZPos).SetValidity(true);

                XPos_old = XPos;
                ZPos_old = ZPos;

                XPos += DistanceMove;
                Move();
            }
        } 
    }

    void MainMove3()
    {
        //Debug.Log(detectObject.GetX() + " - " + detectObject.GetZ());
        int ObjectX = detectObject.GetX();
        int ObjectZ = detectObject.GetZ();

        DistanceMove = 2;
        //DistanceMove = playerStatistiche.GetDistance();
        if (detectObject.CorrectMove == true && grid.FindCell(ObjectX, ObjectZ).GetValidity())
        {

            if (ObjectX == XPos && ObjectZ - 2 == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[2] != true && grid.FindCell(ObjectX, ObjectZ - 1).Walls[2] != true && grid.FindCell(ObjectX, ObjectZ-1).GetValidity())
            { //SU
                grid.FindCell(XPos, ZPos).SetValidity(true);

                XPos_old = XPos;
                ZPos_old = ZPos;

                ZPos += DistanceMove;
                Move();
            }
            else if (ObjectX == XPos && ObjectZ + 2 == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[0] != true && grid.FindCell(ObjectX, ObjectZ + 1).Walls[0] != true && grid.FindCell(ObjectX, ObjectZ + 1).GetValidity())
            { //GIU

                grid.FindCell(XPos, ZPos).SetValidity(true);

                XPos_old = XPos;
                ZPos_old = ZPos;

                ZPos -= DistanceMove;
                Move();
            }
            else if (ObjectX + 2 == XPos && ObjectZ == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[1] != true && grid.FindCell(ObjectX + 1, ObjectZ).Walls[1] != true && grid.FindCell(ObjectX +1 , ObjectZ).GetValidity())
            { //SINISTRA

                grid.FindCell(XPos, ZPos).SetValidity(true);

                XPos_old = XPos;
                ZPos_old = ZPos;

                XPos -= DistanceMove;
                Move();
            }
            else if (ObjectX - 2 == XPos && ObjectZ == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[3] != true && grid.FindCell(ObjectX - 1, ObjectZ).Walls[3] != true && grid.FindCell(ObjectX - 1, ObjectZ).GetValidity())
            { //DESTRA

                grid.FindCell(XPos, ZPos).SetValidity(true);

                XPos_old = XPos;
                ZPos_old = ZPos;

                XPos += DistanceMove;
                Move();
            }
        }
    }


    //Set Position Player
    void SetPositionPlayer() {
        XPos = grid.Width / 2;
        ZPos = grid.Height / 2;
        /*switch (Name) {
            case "Green":
                XPos = 0;
                ZPos = 0;
                break;
            case "Yellow":
                XPos = 0;
                ZPos = grid.GetHeight() - 1;
                break;
            case "Red":
                XPos = grid.GetWidth() - 1;
                ZPos = 0;
                break;
            case "Blue":
                XPos = grid.GetWidth() - 1;
                ZPos = grid.GetHeight() - 1;
                break;
        }*/
    }
}

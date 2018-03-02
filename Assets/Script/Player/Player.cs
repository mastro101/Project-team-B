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

    public UIManager UI;

    public ButtonManager BM;


    public int XPos;    //Posizione X del Player sulla casella
    public int ZPos;    //Posizione Z del Player sulla casella


    int XPos_old;
    int ZPos_old;

    int DistanceMove=1; // Di quanto il giocatore si Muove

    public float _Yoffset;

    int eventCard;

    void Start()
    {
        grid.Center().PlayerOnTile = 4;
        transform.position = grid.GetCenterPosition(); //Setto la posizione del player
        transform.position += new Vector3(0f, _Yoffset, 0f);   //Fix posizione Y del player
        CheckMissions = new int[4];
        SetPositionPlayer();
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
            Tmp.SetMosse(PossibleMove.ToString());
            Tmp.SetStamina(Stamina.ToString());
            Tmp.SetCombatPoints(CombatPoints.ToString());


            if (Gpm.CurrentState == GamePlayManager.State.Mission)
            {
                AssignMisison();
            }
            else if (Gpm.CurrentState == GamePlayManager.State.Movement && PossibleMove > 0)
            {
                MainMove2();    //Movimento del Player tramite Click
                //MainMove3();
            }
            else if (Gpm.CurrentState == GamePlayManager.State.Event)
            {
                Lg.SetTextLog(Name + ": Carta evento pescata", true);

                Event();
            }
            else if (Gpm.CurrentState == GamePlayManager.State.Object)
            {
                Gpm.CurrentState = GamePlayManager.State.End;
            }
            else if (Gpm.CurrentState == GamePlayManager.State.Combat)
            {
                Gpm.CurrentState = GamePlayManager.State.End;
            }

            if (PossibleMove == 0)
            {
                PossibleMove = 2;
                BM.EndP.SetActive(false);
            }
            else if (PossibleMove == 1)
            {
                BM.EndP.SetActive(true);
            }

            
            
            // Morte
            Morte();
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
            

            switch (grid.FindCell(XPos, ZPos).PlayerOnTile) {
                case 1:
                    globalPosition += new Vector3(-0.6f, 0, 0.6f);
                    break;
                case 2:
                    globalPosition += new Vector3(0.6f, 0, -0.6f);
                    break;
                case 3:
                    globalPosition += new Vector3(0.6f, 0, 0.6f);
                    break;
                case 4:
                    globalPosition += new Vector3(-0.6f, 0, -0.6f);
                    break;
                default:
                    break;
            }

            transform.DOMove(globalPosition, 0.6f).SetEase(Ease.Linear);

            // Il player è sulla casella
            grid.FindCell(XPos, ZPos).PlayerOnTile++;

            detectObject.CorrectMove = false;

            // Finito il movimento passa alla fase successiva
            if(PossibleMove == 0)
            {
                Gpm.CurrentState = GamePlayManager.State.Event;
                //PossibleMove = 2;
            }
                

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

            // Il player non è più sulla casella di prima
            grid.FindCell(XPos, ZPos).PlayerOnTile--;

            if (ObjectX == XPos && ObjectZ - 1 == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[2] != true)
            { //SU
                grid.FindCell(XPos, ZPos).SetValidity(true);

                XPos_old = XPos;
                ZPos_old = ZPos;

                ZPos += DistanceMove;
                PossibleMove--;
                Move();
            }
            else if (ObjectX == XPos && ObjectZ + 1 == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[0] != true)
            { //GIU

                grid.FindCell(XPos, ZPos).SetValidity(true);

                XPos_old = XPos;
                ZPos_old = ZPos;

                ZPos -= DistanceMove;
                PossibleMove--;
                Move();
            }
            else if (ObjectX + 1 == XPos && ObjectZ == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[1] != true)
            { //SINISTRA

                grid.FindCell(XPos, ZPos).SetValidity(true);

                XPos_old = XPos;
                ZPos_old = ZPos;

                XPos -= DistanceMove;
                PossibleMove--;
                Move();
            }
            else if (ObjectX - 1 == XPos && ObjectZ == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[3] != true)
            { //DESTRA

                grid.FindCell(XPos, ZPos).SetValidity(true);

                XPos_old = XPos;
                ZPos_old = ZPos;

                XPos += DistanceMove;
                PossibleMove--;
                Move();
            }
        } 



    }

    /*void MainMove3()
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
    }*/


    //Set Position Player
    void SetPositionPlayer() {
        XPos = grid.Width / 2;
        ZPos = grid.Height / 2;
        switch (Name) {
            case "Green":
                transform.position += new Vector3(-0.6f, 0, 0.6f);
                break;
            case "Blue":
                transform.position += new Vector3(0.6f, 0, -0.6f);
                break;
            case "Red":
                transform.position += new Vector3(0.6f, 0, 0.6f);
                break;
            case "Yellow":
                transform.position += new Vector3(-0.6f, 0, -0.6f);
                break;
        }
    }

    //Metodo per assegnare le missioni
    void AssignMisison() {
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

    void Event() {
        //Controllo in che tipo di casella mi trovo
        if (grid.FindCell(XPos, ZPos).GetNameTile() != "" && grid.FindCell(XPos, ZPos).GetNameTile() != "Enemy")
        {
            // se è all'interno di una città passa alla fase Object
            Debug.Log(Name + " si trova nella città: " + grid.FindCell(XPos, ZPos).GetNameTile());
            Lg.SetTextLog(Name + " si trova nella città: " + grid.FindCell(XPos, ZPos).GetNameTile(), true);

            //ActiveTurn = false;
            UI._isHealActive[0] = true;
            UI._isHealActive[1] = true;
            Gpm.CurrentState = GamePlayManager.State.Object;
        }
        else if (grid.FindCell(XPos, ZPos).GetNameTile() == "Enemy")
        {
            // se è all'interno di una Casella Nemico passa alla fase Combat
            Lg.SetTextLog(Name + " si trova in una casella Nemico", true);
            Gpm.CurrentState = GamePlayManager.State.Combat;
        }
        else //In una casella neutrale
        {
            // Viene scelto un numero randomico tra 0 e 2
            eventCard = Random.Range(0, 3);
            Debug.Log(eventCard);
        }

        switch (eventCard)
        {
            // Evento che non comporta un cambio State
            case 0:
                Gpm.CurrentState = GamePlayManager.State.End;
                Lg.SetTextLog(Name + " Ha pescato una carta evento", true);
                break;
            // Evento Oggetto
            case 1:
                Gpm.CurrentState = GamePlayManager.State.Object;
                Lg.SetTextLog(Name + " Ha pescato una carta oggetto", true);
                break;
            // Evento Nemico
            case 2:
                Gpm.CurrentState = GamePlayManager.State.Combat;
                Lg.SetTextLog(Name + " Ha pescato una carta nemico", true);
                break;
            default:
                break;
        }
    }

    void Morte() {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Life--;
            Lg.SetTextLog(Name + " ha perso vita", true);

            if (Life <= 0)
            {
                grid.FindCell(XPos, ZPos).PlayerOnTile--;
                transform.position = grid.GetCenterPosition();
                transform.position += new Vector3(0f, _Yoffset, 0f);
                SetPositionPlayer();
                XPos = 6;
                ZPos = 6;
                Life = 5;
                grid.FindCell(XPos, ZPos).PlayerOnTile++;
                Gpm.CurrentState = GamePlayManager.State.End;
                Lg.SetTextLog(Name + " è morto ed è tornato al centro", true);
                PossibleMove = 2; //rimuovi commento per eliminare il BUG [Volontario]

            }

        }   
    }

}

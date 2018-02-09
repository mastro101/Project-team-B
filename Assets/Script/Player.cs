 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour{

    public Grid grid;
    public PlayerStatistiche playerStatistiche;
    public DetectObject detectObject;
    public GamePlayManager Gpm;


    public int XPos;    //Posizione X del Player sulla casella
    public int ZPos;    //Posizione Z del Player sulla casella

    int XPos_old;
    int ZPos_old;

    int DistanceMove=1; // Di quanto il giocatore si Muove

    public string Name; //Indica il nome del Player


    void Start()
    {
        SetPositionPlayer();
        transform.position = grid.GetWorldPosition(XPos, ZPos); //Setto la posizione del player
        transform.position += new Vector3(0f, 0.55f, 0f);   //Fix posizione Y del player
        grid.FindCell(XPos, ZPos).SetValidity(false);   //Siccome il player è sopra a una casella, nessun altro giocatore potrà andarci sopra


        
    }


    void Update()
    {
        //MainMove();   //Movimento del PLayer tramite WASD
        if (Name == Gpm.Name)
        {
            Debug.Log("pirla funziona "+Gpm.Name);
            MainMove2();    //Movimento del Player tramite Doppio Click

        }
        //playerStatistiche.SetDistace(Name, DistanceMove);   //Setto il movimento del player // Da rivedere in futuro
    }

    //Movimento del Player
    void Move()
    {
        if (grid.IsValidPosition(XPos, ZPos))
        {
            Vector3 globalPosition = grid.GetWorldPosition(XPos, ZPos);
            globalPosition += new Vector3(0f, 0.55f, 0f); ;
            transform.DOMove(globalPosition, 0.6f).SetEase(Ease.Linear);
            grid.FindCell(XPos, ZPos).SetValidity(false);
            detectObject.CorrectMove = false;

            // Finito il movimento passa alla fase successiva
            Gpm.CurrentState = GamePlayManager.State.Event;
            Gpm.CurrentState = GamePlayManager.State.End;
        }
        else
        {
                XPos = XPos_old;
                ZPos = ZPos_old;
        }

    }


    //movimento tramite WASD
    void MainMove() {
        XPos_old = XPos;
        ZPos_old = ZPos;
        DistanceMove = playerStatistiche.GetDistance();
        if (Input.GetKeyDown(KeyCode.A))//Sinistra
        {
            XPos -= DistanceMove;
            Move();
        }
        else if (Input.GetKeyDown(KeyCode.D))//Destra
        {
            XPos += DistanceMove;
            Move();
        }
        else if (Input.GetKeyDown(KeyCode.W))//Su
        {
            ZPos += DistanceMove;
            Move();
        }
        else if (Input.GetKeyDown(KeyCode.S))//Giu
        {
            ZPos -= DistanceMove;
            Move();
        }
    }


    //Movimento tramite doppio click del mouse
    void MainMove2() {
        //Debug.Log(detectObject.GetX() + " - " + detectObject.GetZ());
        int ObjectX = detectObject.GetX();
        int ObjectZ = detectObject.GetZ();
        DistanceMove = playerStatistiche.GetDistance();
        if (detectObject.CorrectMove == true) {

            if (ObjectX == XPos && ObjectZ - 1 == ZPos && grid.FindCell(ObjectX, ObjectZ).GetValidity())
            { //SU
                grid.FindCell(XPos, ZPos).SetValidity(true);

                XPos_old = XPos;
                ZPos_old = ZPos;

                ZPos += DistanceMove;
                Move();
            }
            else if (ObjectX == XPos && ObjectZ + 1 == ZPos && grid.FindCell(ObjectX, ObjectZ).GetValidity())
            { //GIU

                grid.FindCell(XPos, ZPos).SetValidity(true);

                XPos_old = XPos;
                ZPos_old = ZPos;

                ZPos -= DistanceMove;
                Move();
            }
            else if (ObjectX + 1 == XPos && ObjectZ == ZPos && grid.FindCell(ObjectX, ObjectZ).GetValidity())
            { //SINISTRA

                grid.FindCell(XPos, ZPos).SetValidity(true);

                XPos_old = XPos;
                ZPos_old = ZPos;

                XPos -= DistanceMove;
                Move();
            }
            else if (ObjectX - 1 == XPos && ObjectZ == ZPos && grid.FindCell(ObjectX, ObjectZ).GetValidity())
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
        switch (Name) {
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
        }
    }
}

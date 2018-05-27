using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : PlayerStatistiche{

    public Grid grid;
    public DetectObject detectObject;
    public GamePlayManager Gpm;
    public IEnemy currentEnemy;
    public Player currentEnemyPlayer;
    public Player PlayerEnemy1, PlayerEnemy2, PlayerEnemy3;

    Camera gameCamera;
    GameObject playerPrefab;

    public GameObject PlayerPrefab;
    public  TextMeshP Tmp;
    public LogManager Lg;
    public UIManager UI;
    public ButtonManager BM;
    public CombatManager CB;
    public Mission MissionManager;

    EnemyPoolManager enemyManager;
    EventManager eventManager;

    public int Life
    {
        get { return life; }
        set
        {
            if (value > MaxLife)
                life = MaxLife;
            else
                life = value;
            Morte();
        }
    }

    public int Credit
    {
        get { return credit; }
        set
        {
            credit = value;
            if (credit < 0)
                credit = 0;
        }
    }

    public void AddMaterial(int materialType, int _material)
    {
        Materiali[materialType] += _material;
        if (Materiali[materialType] < 0)
            Materiali[materialType] = 0;
    }


    public int XPos;    //Posizione X del Player sulla casella
    public int ZPos;    //Posizione Z del Player sulla casella


    int XPos_old;
    int ZPos_old;

    int DistanceMove=1; // Di quanto il giocatore si Muove

    [HideInInspector]
    public bool jumperEvent;

    public float _Yoffset;

    int eventCard;
    public bool inCombatEnemy;
    public bool inCombatPlayer;

    void Start()
    {
        Life = 5;

        enemyManager = FindObjectOfType<EnemyPoolManager>();
        MissionManager = FindObjectOfType<Mission>();
        gameCamera = FindObjectOfType<Camera>();
        eventManager = FindObjectOfType<EventManager>();
        Materiali = new int[4];

        playerPrefab = Instantiate(PlayerPrefab);
        playerPrefab.transform.position = new Vector3(1000, 1000, 1000);

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
            switch (Mission)
            {
                case 1:
                    if (MissionManager.check[0] == false)
                        Tmp.SetMission("Vai in A");
                    else
                        Tmp.SetMission("Vai in B");
                    break;
                case 2:
                    if (MissionManager.check[1] == false)
                        Tmp.SetMission("Vai in B");
                    else
                        Tmp.SetMission("Vai in C");
                    break;
                case 3:
                    if (MissionManager.check[2] == false)
                        Tmp.SetMission("Vai in C");
                    else
                        Tmp.SetMission("Vai in D");
                    break;
                case 4:
                    if (MissionManager.check[3] == false)
                        Tmp.SetMission("Vai in D");
                    else
                        Tmp.SetMission("Vai in A");
                    break;
                default:
                    break;
            }
            //Tmp.SetMission(Mission.ToString());
            Tmp.SetLife(Life.ToString());
            Tmp.SetCredits(Credit.ToString());
            Tmp.SetName(Gpm.Name);
            Tmp.SetMosse(PossibleMove.ToString());
            Tmp.SetCombatPoints(CombatPoint.ToString());
            Tmp.SetM1(Materiali[0].ToString());
            Tmp.SetM2(Materiali[1].ToString());
            Tmp.SetM3(Materiali[2].ToString());
            Tmp.SetM4(Materiali[3].ToString());


            if (Gpm.CurrentState == GamePlayManager.State.Mission)
            {
                AssignMisison();
            }
            else if (Gpm.CurrentState == GamePlayManager.State.Movement && PossibleMove > 0)
            {
                
                MainMove2();    //Movimento del Player tramite Click
                if(PossibleMove == 2)
                    MainMove3();
                
            }
            else if (Gpm.CurrentState == GamePlayManager.State.Event)
            {
                

                Event();
            }
            else if (Gpm.CurrentState == GamePlayManager.State.Object)
            {
                if (grid.FindCell(XPos, ZPos).GetNameTile() != "" && grid.FindCell(XPos, ZPos).GetNameTile() != "Enemy")
                {
                    UI._isHealActive[0] = true;
                    UI._isHealActive[1] = true;
                }
                else {
                    Gpm.CurrentState = GamePlayManager.State.End; 
                }
            }
            else if (Gpm.CurrentState == GamePlayManager.State.Combat)
            {
                

                if (grid.FindCell(XPos, ZPos).POnTile == this)
                {
                    
                    if (!inCombatEnemy)
                    {
                        SpawnEnemy();
                        gameCamera.transform.position = new Vector3(18.1f, 36.2f, 18.5f);
                        gameCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
                        gameCamera.orthographicSize = 4;
                        playerPrefab.transform.position = new Vector3(14.73f, 37.97f, 21f);
                        inCombatEnemy = true;
                    }
                }
                else
                {
                    if (!inCombatPlayer)
                    {
                        currentEnemyPlayer = grid.FindCell(XPos, ZPos).POnTile;
                        gameCamera.transform.position = new Vector3(18.1f, 36.2f, 18.5f);
                        gameCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
                        gameCamera.orthographicSize = 4;
                        playerPrefab.transform.position = new Vector3(14.73f, 37.97f, 21f);
                        inCombatPlayer = true;
                    }
                }


                // Attacco
                if (inCombatEnemy || inCombatPlayer)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                        Attacks = 1;
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                        Attacks = 2;
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                        Attacks = 3;

                }

                if (inCombatEnemy)
                {
                    currentEnemy.CurrentPlayer = this;
                    CB.player = this;
                    if (CB.Active == false)
                        CB.OpenAndCloseInventoryCombat();

                    if (currentEnemy.Attack == 0)
                        currentEnemy.Attack = Random.Range(1, 4);

                    if (Attacks != 0 && currentEnemy.Attack != 0)
                    {
                        
                        switch (Attacks)
                        {
                            case 1:
                                switch (currentEnemy.Attack)
                                {
                                    case 1:
                                        currentEnemy.Attack = 0;
                                        Attacks = 0;
                                        break;
                                    case 2:
                                        currentEnemy.DamagePlayer(this);
                                        currentEnemy.OnAttack += OnEnemyAttack;
                                        break;
                                    case 3:
                                        currentEnemy.LoseRound();
                                        break;
                                    default:
                                        break;
                                }
                                break;

                            case 2:
                                switch (currentEnemy.Attack)
                                {
                                    case 1:
                                        currentEnemy.LoseRound();
                                        break;
                                    case 2:
                                        currentEnemy.Attack = 0;
                                        Attacks = 0;
                                        break;
                                    case 3:
                                        currentEnemy.DamagePlayer(this);
                                        currentEnemy.OnAttack += OnEnemyAttack;
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case 3:
                                switch (currentEnemy.Attack)
                                {
                                    case 1:
                                        currentEnemy.DamagePlayer(this);
                                        currentEnemy.OnAttack += OnEnemyAttack;
                                        break;
                                    case 2:
                                        currentEnemy.LoseRound();
                                        break;
                                    case 3:
                                        currentEnemy.Attack = 0;
                                        Attacks = 0;
                                        break;
                                    default:
                                        break;
                                }
                                break;


                            default:
                                break;
                        }
                    }

                }

                
                else if (inCombatPlayer)
                {
                    currentEnemyPlayer.currentEnemyPlayer = this;
                    currentEnemyPlayer.playerPrefab.transform.position = new Vector3(21f, 37.66f, 21.03f);
                    CB.player = this;
                    if (CB.Active == false)
                        CB.OpenAndCloseInventoryCombat();
                    if (Input.GetKeyDown(KeyCode.Alpha8))
                        currentEnemyPlayer.Attacks = 1;
                    else if (Input.GetKeyDown(KeyCode.Alpha9))
                        currentEnemyPlayer.Attacks = 2;
                    else if (Input.GetKeyDown(KeyCode.Alpha0))
                        currentEnemyPlayer.Attacks = 3;

                    if (Attacks != 0 && currentEnemyPlayer.Attacks != 0)
                    {
                        switch (Attacks)
                        {
                            case 1:
                                switch (currentEnemyPlayer.Attacks)
                                {
                                    case 1:
                                        currentEnemyPlayer.Attacks = 0;
                                        Attacks = 0;
                                        break;
                                    case 2:
                                        LoseRound();
                                        break;
                                    case 3:
                                        currentEnemyPlayer.LoseRound();
                                        break;
                                    default:
                                        break;
                                }
                                break;

                            case 2:
                                switch (currentEnemyPlayer.Attacks)
                                {
                                    case 1:
                                        currentEnemyPlayer.LoseRound();
                                        break;
                                    case 2:
                                        currentEnemyPlayer.Attacks = 0;
                                        Attacks = 0;
                                        break;
                                    case 3:
                                        LoseRound();
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case 3:
                                switch (currentEnemyPlayer.Attacks)
                                {
                                    case 1:
                                        LoseRound();
                                        break;
                                    case 2:
                                        currentEnemyPlayer.LoseRound();
                                        break;
                                    case 3:
                                        currentEnemyPlayer.Attacks = 0;
                                        Attacks = 0;
                                        break;
                                    default:
                                        break;
                                }
                                break;


                            default:
                                break;
                        }
                    }
                }



                if (grid.FindCell(XPos, ZPos).POnTile == this)
                {
                    if (currentEnemy.CurrentState == IEnemyState.InPool)
                    {
                        inCombatEnemy = false;
                        //CB.OpenAndCloseInventoryCombat();
                        playerPrefab.transform.position = new Vector3(1000, 1000, 1000);
                        Gpm.CurrentState = GamePlayManager.State.End;
                    }
                }
            }

            if (PossibleMove == 0)
            {                               
                //PossibleMove = 2;
                BM.EndP.SetActive(true);
            }
            else if (PossibleMove < 4)
            {
                BM.EndP.SetActive(true);
            }

            
            
            // Morte
            Morte();
        }
        //playerStatistiche.SetDistace(Name, DistanceMove);   //Setto il movimento del player // Da rivedere in futuro
    }

    public void OnEnemyAttack(IEnemy enemy)
    {
        currentEnemy.OnAttack -= OnEnemyAttack;
    }

    private void SpawnEnemy()
    {
        currentEnemy = enemyManager.GetEnemy(Random.Range(0, enemyManager.EnemyPrefabs.Length));
        currentEnemy.gameObject.transform.position = new Vector3(21f, 37.66f, 21.03f);
        currentEnemy.Spawn();
        currentEnemy.OnDestroy += OnEnemyDestroy;

    }

    public void OnEnemyDestroy(IEnemy enemy)
    {
        CB.CloseInventoryCombat();
        gameCamera.transform.position = new Vector3(40f, 35f, -7f);
        gameCamera.transform.rotation = Quaternion.Euler(47f, -45.50f, 0);
        gameCamera.orthographicSize = 21;
        if (currentEnemy.IsAlive == false)
        {
            Credit += currentEnemy.Credits;
            
        }
        currentEnemy.OnDestroy -= OnEnemyDestroy;
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
            if (grid.FindCell(XPos, ZPos).POnTile == null /*&& (grid.FindCell(XPos, ZPos).GetNameTile() == "" || grid.FindCell(XPos, ZPos).GetNameTile() == "Enemy")*/ )
                grid.FindCell(XPos, ZPos).SetPlayer(this);

            detectObject.CorrectMove = false;



            // Finito il movimento passa alla fase successiva
            /*if(PossibleMove == 0)
            {
                Gpm.CurrentState = GamePlayManager.State.Event;
                //PossibleMove = 2;
            }*/
                

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

                moveSetting();

                ZPos += DistanceMove;
                PossibleMove--;
                Move();
            }
            else if (ObjectX == XPos && ObjectZ + 1 == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[0] != true)
            { //GIU

                moveSetting();

                ZPos -= DistanceMove;
                PossibleMove--;
                Move();
            }
            else if (ObjectX + 1 == XPos && ObjectZ == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[1] != true)
            { //SINISTRA

                moveSetting();

                XPos -= DistanceMove;
                PossibleMove--;
                Move();
            }
            else if (ObjectX - 1 == XPos && ObjectZ == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[3] != true)
            { //DESTRA

                moveSetting();

                XPos += DistanceMove;
                PossibleMove--;
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

                moveSetting();

                ZPos += DistanceMove;
                PossibleMove-=2;
                Move();
            }
            else if (ObjectX == XPos && ObjectZ + 2 == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[0] != true && grid.FindCell(ObjectX, ObjectZ + 1).Walls[0] != true && grid.FindCell(ObjectX, ObjectZ + 1).GetValidity())
            { //GIU

                moveSetting();

                ZPos -= DistanceMove;
                PossibleMove -= 2;
                Move();
            }
            else if (ObjectX + 2 == XPos && ObjectZ == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[1] != true && grid.FindCell(ObjectX + 1, ObjectZ).Walls[1] != true && grid.FindCell(ObjectX +1 , ObjectZ).GetValidity())
            { //SINISTRA

                moveSetting();

                XPos -= DistanceMove;
                PossibleMove -= 2;
                Move();
            }
            else if (ObjectX - 2 == XPos && ObjectZ == ZPos && grid.FindCell(ObjectX, ObjectZ).Walls[3] != true && grid.FindCell(ObjectX - 1, ObjectZ).Walls[3] != true && grid.FindCell(ObjectX - 1, ObjectZ).GetValidity())
            { //DESTRA

                moveSetting();

                XPos += DistanceMove;
                PossibleMove -= 2;
                Move();
            }
        }
    }

    void moveSetting()
    {
        grid.FindCell(XPos, ZPos).SetValidity(true);
        grid.FindCell(XPos, ZPos).PlayerOnTile--;
        if (grid.FindCell(XPos, ZPos).POnTile == this)
            grid.FindCell(XPos, ZPos).SetPlayer(null);

        XPos_old = XPos;
        ZPos_old = ZPos;
    }


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
        {
            Gpm.CurrentState = GamePlayManager.State.Movement;
        }
        if (!CheckMission)
        {
            // Assegnata missione casuale diversa da quella di un altro giocatore
            Debug.Log("M");
            do
            {
                Mission = Random.Range(1, 5);
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

    int event1, event2;

    void Event() {
        //Controllo in che tipo di casella mi trovo

        
        if (grid.FindCell(XPos, ZPos).GetNameTile() != "" && grid.FindCell(XPos, ZPos).GetNameTile() != "Enemy")
        {
            // se è all'interno di una città passa alla fase Object
            Debug.Log(Name + " si trova nella città: " + grid.FindCell(XPos, ZPos).GetNameTile());
            Lg.SetTextLog(Name + " si trova nella città: " + grid.FindCell(XPos, ZPos).GetNameTile(), true);

            //ActiveTurn = false;
            Gpm.CurrentState = GamePlayManager.State.Object;
        }
        else if (grid.FindCell(XPos, ZPos).POnTile != this)
        {
            Lg.SetTextLog(Name + "Sta Combattendo contro " + grid.FindCell(XPos, ZPos).POnTile.Name, true);
            Gpm.CurrentState = GamePlayManager.State.Combat;
        }
        else if (grid.FindCell(XPos, ZPos).GetNameTile() == "Enemy")
        {
            // se è all'interno di una Casella Nemico passa alla fase Combat
            Lg.SetTextLog(Name + " si trova in una casella Nemico", true);
            Gpm.CurrentState = GamePlayManager.State.Combat;
        }
        else if (grid.FindCell(XPos, ZPos).GetNameTile() == "")//In una casella neutrale
        {
            // Viene scelto un numero randomico per ogni evento
            if (eventCard == 0)
            {
                do {
                    eventCard = Random.Range(1, 14);
                } while (eventCard == 0);                
                event1 = eventCard;
                EventListView(event1);
                Debug.Log(event1);
                do {
                    eventCard = Random.Range(1, 14);
                }
                while (eventCard == event1 || eventCard == 0);
                event2 = eventCard;
                EventListView(event2);
                Debug.Log(event2);
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                EventList(event1);
                if (Gpm.CurrentState != GamePlayManager.State.Combat && jumperEvent == false)
                    Gpm.CurrentState = GamePlayManager.State.End;
            }


            if (Input.GetKeyDown(KeyCode.P))
            {
                EventList(event2);
                if (Gpm.CurrentState != GamePlayManager.State.Combat && jumperEvent == false)
                    Gpm.CurrentState = GamePlayManager.State.End;
            }

            
           
        }

        
    }

    public void EventList(int _event)
    {
        switch (_event)
        {
            // Evento che non comporta un cambio State
            case 0:
                Debug.Log("Error");
                break;
            // Evento Nemico
            case 1:
                Gpm.CurrentState = GamePlayManager.State.Combat;
                Lg.SetTextLog(Name + " Ha pescato una carta nemico", true);

                break;
            // Cuore Meccanico
            case 2:
                eventManager.Heal(2, this);
                Lg.SetTextLog(Name + " Si è curato di 2", true);
                break;
            // Pioggia Acida
            case 3:
                eventManager.Heal(-1, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                Lg.SetTextLog("Gli altri giocatori hanno perso una vita", true);
                break;
            // Vincere la scommessa
            case 4:
                eventManager.TakeCredits(10, this);
                Lg.SetTextLog(Name + "Ha guadagnato 10 crediti", true);
                break;
            // Ladri
            case 5:
                eventManager.TakeCredits(-5, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                Lg.SetTextLog("Gli altri giocatori hanno perso 5 crediti", true);
                break;
            // Jumper
            case 6:
                if (jumperEvent == false)
                    PossibleMove = 2;
                jumperEvent = true;
                Gpm.CurrentState = GamePlayManager.State.Movement;
                break;
            // Terreno Fangoso
            case 7:
                PlayerEnemy1.PossibleMove = 2;
                PlayerEnemy2.PossibleMove = 2;
                PlayerEnemy3.PossibleMove = 2;
                Lg.SetTextLog("Gli altri giocatori hanno perso 2 movimenti", true);
                break;
            // Jynix doppia coppia
            case 8:
                eventManager.Jynix(this, 2, 6, 6);
                break;
            // Jynix Poker D'assi
            case 9:
                eventManager.Jynix(this, 4, 8, 10);
                break;
            // All in
            case 10:
                eventManager.Jynix(this, 1, 8, 0, true);
                break;
            // fold
            case 11:
                eventManager.Jynix(this, 1, 10, 1);
                break;
            case 12:
                eventManager.AddRandomMaterial(5, this);
                break;
            case 13:
                eventManager.RemoveRandomMaterial(2, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                break;
            default:
                Lg.SetTextLog("Evento " + eventCard + " nullo", true);
                break;
        }
    }

    public void EventListView(int _eventCard)
    {
        switch (_eventCard)
        {
            // Evento Nemico
            case 1:
                Lg.SetTextLog("Un nemico Ti attacca", true);
                break;
            // Cuore Meccanico
            case 2:
                Lg.SetTextLog(" Ti curi di 2", true);
                break;
            // Pioggia Acida
            case 3:
                Lg.SetTextLog("Gli altri giocatori perdono una vita", true);
                break;
            // Vincere la scommessa
            case 4:
                Lg.SetTextLog("Guadagni 10 crediti", true);
                break;
            // Ladri
            case 5:
                Lg.SetTextLog("Gli altri giocatori Perdono 5 crediti", true);
                break;
            // Jumper
            case 6:
                Lg.SetTextLog("Hai 2 movimenti in più", true);
                break;
            // Terreno Fangoso
            case 7:
                Lg.SetTextLog("Gli altri giocatori perdono 2 movimenti", true);
                break;
            // Jynix doppia coppia
            case 8:
                Lg.SetTextLog("Vendi 2 materiali random per forse 6 crediti", true);
                break;
            // Jynix Poker D'assi
            case 9:
                Lg.SetTextLog("Vendi 4 materiali random per forse 10 crediti", true);
                break;
            // All in
            case 10:
                Lg.SetTextLog("Vendi tutti i materiali di un tipo random per forse 1 punto vittoria", true);
                break;
            // fold
            case 11:
                Lg.SetTextLog("Vendi 1 materiale random per 2 crediti", true);
                break;
            case 12:
                Lg.SetTextLog("Ricevi 5 materiali random", true);
                break;
            case 13:
                Lg.SetTextLog("Rimuovi agli altri 2 materiali random", true);
                break;
            default:
                Lg.SetTextLog("Evento " + eventCard + " nullo", true);
                break;
        }
    }

    public void LoseRound()
    {
        if (inCombatEnemy)
        {
            currentEnemy.CombatPoint++;
            currentEnemy.Attack = 0;
        }

        if (inCombatPlayer)
        {
            currentEnemyPlayer.CombatPoint++;
            currentEnemyPlayer.Attacks = 0;
        }


        Attacks = 0;
        
        if ((currentEnemy != null && currentEnemy.CombatPoint == 1) || (currentEnemyPlayer != null && currentEnemyPlayer.CombatPoint == 1))
        {
            Gpm.CurrentState = GamePlayManager.State.End;
            CB.CloseInventoryCombat();

            gameCamera.transform.position = new Vector3(40f, 35f, -7f);
            gameCamera.transform.rotation = Quaternion.Euler(47f, -45.50f, 0);
            gameCamera.orthographicSize = 21;
            

            if (currentEnemy != null)
                currentEnemy.DestroyMe();

            if (inCombatPlayer)
            {
                currentEnemyPlayer.Attacks = 0;
                currentEnemyPlayer.CombatPoint = 0;
            }

            Life -= currentEnemy.Damage;

            if (inCombatPlayer)
            {                
                inCombatPlayer = false;
                playerPrefab.transform.position = new Vector3(1000,1000,1000);
                currentEnemyPlayer.playerPrefab.transform.position = new Vector3(1000,1000,1000);
            }
            
            CombatPoint = 0;

            //if (Life <= 0)
              //  Morte();

            
        }
    }

    public void Heal()
    {
        if (Credit >= 3)
        {
            Credit -= 3;
            Life = 5;
        }
    }

    void Morte() {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Life--;
            Lg.SetTextLog(Name + " ha perso vita", true);
        }
        if (Life <= 0)
        {
            grid.FindCell(XPos, ZPos).PlayerOnTile--;
            if (grid.FindCell(XPos, ZPos).POnTile == this)
                grid.FindCell(XPos, ZPos).SetPlayer(null);
            transform.position = grid.GetCenterPosition();
            transform.position += new Vector3(0f, _Yoffset, 0f);
            SetPositionPlayer();
            XPos = 6;
            ZPos = 6;
            Life = 5;
            grid.FindCell(XPos, ZPos).PlayerOnTile++;
            PossibleMove = 4;
            Lg.SetTextLog(Name + " è morto ed è tornato al centro", true);
            if (Name == Gpm.Name)
                Gpm.CurrentState = GamePlayManager.State.End;
                
        }

           
    }

}



public class ciccioBalilla {


}

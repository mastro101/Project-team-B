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

    public  TextMeshP Tmp;

    public LogManager Lg;

    public UIManager UI;

    public ButtonManager BM;

    public CombatManager CB;

    public Mission MissionManager;

    EnemyPoolManager enemyManager;

    public int XPos;    //Posizione X del Player sulla casella
    public int ZPos;    //Posizione Z del Player sulla casella


    int XPos_old;
    int ZPos_old;

    int DistanceMove=1; // Di quanto il giocatore si Muove

    public float _Yoffset;

    int eventCard;
    public bool inCombatEnemy;
    public bool inCombatPlayer;

    void Start()
    {
        enemyManager = FindObjectOfType<EnemyPoolManager>();
        MissionManager = FindObjectOfType<Mission>();


        grid.Center().PlayerOnTile = 4;
        transform.position = grid.GetCenterPosition(); //Setto la posizione del player
        transform.position += new Vector3(0f, _Yoffset, 0f);   //Fix posizione Y del player
        CheckMissions = new int[4];
        SetPositionPlayer();

        Stamina = 20;
        Attacks[0] = 1;
        Attacks[1] = 2;
        Attacks[2] = 3;
        Attacks[3] = 4;
        Attacks[4] = 5;
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
            Tmp.SetStamina(Stamina.ToString());
            Tmp.SetCombatPoints(CombatPoints.ToString());


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
                Lg.SetTextLog(Name + ": Carta evento pescata", true);

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
                        inCombatEnemy = true;
                    }
                }
                else
                {
                    if (!inCombatPlayer)
                    {
                        currentEnemyPlayer = grid.FindCell(XPos, ZPos).POnTile;
                        inCombatPlayer = true;
                    }
                }


                // Attacco
                if (inCombatEnemy)
                {
                    CB.player = this;
                    if (CB.Active == false)
                        CB.OpenAndCloseInventoryCombat();

                    /*if (currentEnemy.CurrentState == IEnemyState.InUse)
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            currentEnemy.TakeDamage(Attacks[Random.Range(0, 5)]);
                            currentEnemy.AttackPlayer(this);
                            currentEnemy.OnAttack += OnEnemyAttack;

                        }
                    }*/
                }
                else if (inCombatPlayer)
                {
                    CB.player = this;
                    if (CB.Active == false)
                        CB.OpenAndCloseInventoryCombat();
                }



                if (grid.FindCell(XPos, ZPos).POnTile == this)
                {
                    if (currentEnemy.CurrentState == IEnemyState.InPool)
                    {
                        inCombatEnemy = false;
                        CB.OpenAndCloseInventoryCombat();
                        Gpm.CurrentState = GamePlayManager.State.End;
                    }
                }
            }

            if (PossibleMove == 0)
            {                               
                //PossibleMove = 2;
                BM.EndP.SetActive(true);
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

    public void OnEnemyAttack(IEnemy enemy)
    {
        currentEnemy.OnAttack -= OnEnemyAttack;
    }

    private void SpawnEnemy()
    {
        currentEnemy = enemyManager.GetEnemy(Random.Range(0, enemyManager.EnemyPrefabs.Length));
        currentEnemy.gameObject.transform.position = this.transform.position;
        currentEnemy.Spawn();
        currentEnemy.OnDestroy += OnEnemyDestroy;

    }

    public void OnEnemyDestroy(IEnemy enemy)
    {
        CB.CloseInventoryCombat();
        if (currentEnemy.IsAlive == false)
        {
            Credit += currentEnemy.Credits;
            CombatPoints += currentEnemy.CombatPoints;
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
            // Viene scelto un numero randomico tra 0 e 2
            eventCard = Random.Range(0, 3);
            Debug.Log(eventCard);

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

        
    }

    public void TakeDamage(int damage)
    {
        Stamina -= damage;
        if (Stamina <= 0)
        {
            Gpm.CurrentState = GamePlayManager.State.End;
            CB.CloseInventoryCombat();

            if (currentEnemy != null)
                currentEnemy.DestroyMe();
            Life--;
            if (inCombatPlayer)
            {                
                inCombatPlayer = false;
            }
            
            if (Life <= 0)
                Morte();

            Stamina = 20;
            
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
            PossibleMove = 2;
            Lg.SetTextLog(Name + " è morto ed è tornato al centro", true);
            Stamina = 20;
            Gpm.CurrentState = GamePlayManager.State.End;
                
        }

           
    }

}

public class ciccioBalilla {


}

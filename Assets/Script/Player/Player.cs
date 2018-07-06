using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : PlayerStatistiche{

    public TheGrid grid;
    public DetectObject detectObject;
    public GamePlayManager Gpm;
    public IEnemy currentEnemy;
    public Player currentEnemyPlayer;
    public Player PlayerEnemy1, PlayerEnemy2, PlayerEnemy3;

    bool vfxPlay;
    Camera gameCamera;
    GameObject playerPrefab;
    SoundEffectManager soundEffect;
    
    public ParticleSystem vfx;
    //public Animator anim;
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
           //if (value >= Life)
           //    Lg.SetTextLog(Nickname + " heal of " + (value - Life), true);
           //else
           //    Lg.SetTextLog(Nickname + " lost " + (Life - value) + " life ", true);

            if (value > MaxLife)
                life = MaxLife;
            else if (value <= 0)
            {
                life = 0;
                Morte();
            }
            else
                life = value;                
        }
    }

    public int Credit
    {
        get { return credit; }
        set
        {
           //if (value >= Credit)
           //    Lg.SetTextLog(Nickname + " received " + (value - Credit) + " Credits", true);
           //else
           //    Lg.SetTextLog(Nickname + " lost " + (Credit - value) + " Credits", true);

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

    [HideInInspector]
    public int eventCard;

    public bool inCombatEnemy;
    public bool inCombatPlayer;

    [HideInInspector]
    public float countdown = 2f;
    [HideInInspector]
    public bool JustEmpted;
    float cameraSize;

    void Start()
    {
        Life = 5;

        soundEffect = FindObjectOfType<SoundEffectManager>();
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
        cameraSize = gameCamera.orthographicSize;
        vfx.Stop();
    }


    void Update()
    {
        //MainMove();   //Movimento del PLayer tramite WASD

        if (Name == Gpm.Name)
        {
            if (!vfxPlay)
            {
                vfx.Play();
                vfxPlay = true;
            }
            /*switch (Mission)
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
            }*/
            //Tmp.SetMission(Mission.ToString());
            Tmp.SetLife(Life.ToString());
            Tmp.SetCredits(Credit.ToString());
            Tmp.SetName(Nickname);
            Tmp.SetMosse(PossibleMove.ToString());
            Tmp.SetWinPoints(WinPoint.ToString());
            Tmp.SetM1(Materiali[0].ToString());
            Tmp.SetM2(Materiali[1].ToString());
            Tmp.SetM3(Materiali[2].ToString());
            Tmp.SetM4(Materiali[3].ToString());
            UI.PlayerTurnImage.texture = UI.PlayerImage[(int)Gpm.CurrentTurn];
            UI.PlayerNameImage.texture = UI.PlayerName[(int)Gpm.CurrentTurn];


            if (Gpm.CurrentState == GamePlayManager.State.Mission)
            {
                //AssignMisison();
            }
            else if (Gpm.CurrentState == GamePlayManager.State.Debug)
            {
                UI.Console.SetActive(true);
            }
            else if (Gpm.CurrentState == GamePlayManager.State.Movement && PossibleMove > 0)
            {
                
                MainMove2();    //Movimento del Player tramite Click
               //if(PossibleMove == 2)
               //    MainMove3();
                
            }
            else if (Gpm.CurrentState == GamePlayManager.State.Event)
            {
                
                Event();
            }
            else if (Gpm.CurrentState == GamePlayManager.State.City)
            {
                if (grid.FindCell(XPos, ZPos).GetNameTile() != "" && grid.FindCell(XPos, ZPos).GetNameTile() != "Enemy")
                {
                    UI.Console.SetActive(true);
                    UI.UICity.SetActive(true);
                    UI._isHealActive[0] = true;
                    UI._isHealActive[1] = true;
                    switch (grid.FindCell(XPos, ZPos).GetNameTile())
                    {
                        case "A":
                            UI.MaterialeToSell.texture = UI.MaterialiToSellImage[0];
                            UI.MaterialeToBuy.texture = UI.MaterialiImage[1];
                            break;
                        case "B":
                            UI.MaterialeToSell.texture = UI.MaterialiToSellImage[1];
                            UI.MaterialeToBuy.texture = UI.MaterialiImage[2];
                            break;
                        case "C":
                            UI.MaterialeToSell.texture = UI.MaterialiToSellImage[2];
                            UI.MaterialeToBuy.texture = UI.MaterialiImage[3];
                            break;
                        case "D":
                            UI.MaterialeToSell.texture = UI.MaterialiToSellImage[3];
                            UI.MaterialeToBuy.texture = UI.MaterialiImage[0];
                            break;
                        default:
                            break;
                    }
                }
                else {
                    Gpm.CurrentState = GamePlayManager.State.End; 
                }
            }
            else if (Gpm.CurrentState == GamePlayManager.State.Combat)
            {
                UI.Player1InCombat.texture = UI.PlayerImage[(int)Gpm.CurrentTurn];
                UI.Player1NameInCombat.texture = UI.PlayerName[(int)Gpm.CurrentTurn];

                if (grid.FindCell(XPos, ZPos).POnTile == this)
                {
                    
                    if (!inCombatEnemy)
                    {
                        soundEffect.PlayEffect(soundEffect.CombatTile);
                        SpawnEnemy();
                        UI.PlayerOrEnemyInCombat.texture = UI.PlayerImage[currentEnemy.ID + 4];
                        UI.PlayerOrEnemyNameInCombat.texture = UI.PlayerName[4];
                        gameCamera.transform.position = new Vector3(18.1f, 36.2f, 18.5f);
                        gameCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
                        gameCamera.orthographic = true;
                        gameCamera.orthographicSize = 2.4f;
                        playerPrefab.transform.position = new Vector3(16.3f, 35.85f, 21f);
                        inCombatEnemy = true;
                    }
                }
                else
                {
                    if (!inCombatPlayer)
                    {
                        soundEffect.PlayEffect(soundEffect.CombatTile);
                        currentEnemyPlayer = grid.FindCell(XPos, ZPos).POnTile;
                        switch (currentEnemyPlayer.Name)
                        {
                            case "Green":
                                UI.PlayerOrEnemyInCombat.texture = UI.PlayerImage[0];
                                UI.PlayerOrEnemyNameInCombat.texture = UI.PlayerName[0];
                                break;
                            case "Blue":
                                UI.PlayerOrEnemyInCombat.texture = UI.PlayerImage[1];
                                UI.PlayerOrEnemyNameInCombat.texture = UI.PlayerName[1];
                                break;
                            case "Red":
                                UI.PlayerOrEnemyInCombat.texture = UI.PlayerImage[2];
                                UI.PlayerOrEnemyNameInCombat.texture = UI.PlayerName[2];
                                break;
                            case "Yellow":
                                UI.PlayerOrEnemyInCombat.texture = UI.PlayerImage[3];
                                UI.PlayerOrEnemyNameInCombat.texture = UI.PlayerName[3];
                                break;
                        }
                        gameCamera.transform.position = new Vector3(18.1f, 36.2f, 18.5f);
                        gameCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
                        gameCamera.orthographic = true;
                        gameCamera.orthographicSize = 2.4f;
                        playerPrefab.transform.position = new Vector3(16.3f, 35.85f, 21f);
                        inCombatPlayer = true;
                    }
                }


                // Attacco
                if ((inCombatEnemy || inCombatPlayer) && Attacks == 0)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        Attacks = 1;
                        soundEffect.PlayEffect(soundEffect.Attack);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        Attacks = 2;
                        soundEffect.PlayEffect(soundEffect.Attack);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        Attacks = 3;
                        soundEffect.PlayEffect(soundEffect.Attack);
                    }
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
                        if (Gpm.CurrentCombatState != GamePlayManager.CombatState.Animation)
                        {
                            Lg.SetTextLog("Hai usato " + Attacks, true);
                            Lg.SetTextLog("Nemico usa " + currentEnemy.Attack, true);
                            Gpm.CurrentCombatState = GamePlayManager.CombatState.Animation;
                        }

                        if (Gpm.CurrentCombatState == GamePlayManager.CombatState.Animation)
                        {
                            UI.LightAttackOnPlayer(Attacks - 1);
                            UI.LightAttackOnEnemy(currentEnemy.Attack - 1);
                            countdown -= Time.deltaTime;
                            Debug.Log(countdown);
                            if (countdown <= 0)
                            {
                                countdown = 2f;
                                UI.LightAttackOff();
                                Gpm.CurrentCombatState = GamePlayManager.CombatState.Check;
                            }
                        }

                        if (Gpm.CurrentCombatState == GamePlayManager.CombatState.Check)
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

                }

                
                else if (inCombatPlayer)
                {
                    currentEnemyPlayer.currentEnemyPlayer = this;
                    currentEnemyPlayer.playerPrefab.transform.position = new Vector3(19.6f, 35.85f, 21.03f);
                    CB.player = this;
                    if (CB.Active == false)
                        CB.OpenAndCloseInventoryCombat();
                    if (currentEnemyPlayer.Attacks == 0)
                    {
                        if (Input.GetKeyDown(KeyCode.Alpha8))
                        {
                            currentEnemyPlayer.Attacks = 1;
                            soundEffect.PlayEffect(soundEffect.Attack);
                        }
                        else if (Input.GetKeyDown(KeyCode.Alpha9))
                        {
                            currentEnemyPlayer.Attacks = 2;
                            soundEffect.PlayEffect(soundEffect.Attack);
                        }
                        else if (Input.GetKeyDown(KeyCode.Alpha0))
                        {
                            currentEnemyPlayer.Attacks = 3;
                            soundEffect.PlayEffect(soundEffect.Attack);
                        }
                    }

                    if (Attacks != 0 && currentEnemyPlayer.Attacks != 0)
                    {
                        if (Gpm.CurrentCombatState != GamePlayManager.CombatState.Animation)
                        {
                            Lg.SetTextLog("Hai usato " + Attacks, true);
                            Lg.SetTextLog("Nemico usa " + currentEnemyPlayer.Attacks, true);
                            Gpm.CurrentCombatState = GamePlayManager.CombatState.Animation;
                        }

                        if (Gpm.CurrentCombatState == GamePlayManager.CombatState.Animation)
                        {
                            UI.LightAttackOnPlayer(Attacks - 1);
                            UI.LightAttackOnEnemy(currentEnemyPlayer.Attacks - 1);
                            countdown -= Time.deltaTime;
                            Debug.Log(countdown);
                            if (countdown <= 0)
                            {
                                UI.LightAttackOff();
                                Gpm.CurrentCombatState = GamePlayManager.CombatState.Check;                                
                            }
                        }

                        if (Gpm.CurrentCombatState == GamePlayManager.CombatState.Check)
                        {
                            countdown = 2f;
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
                }



                if (grid.FindCell(XPos, ZPos).POnTile == this && inCombatEnemy)
                {
                    if (currentEnemy.CurrentState == IEnemyState.InPool)
                    {
                        inCombatEnemy = false;
                        //CB.OpenAndCloseInventoryCombat();
                        playerPrefab.transform.position = new Vector3(1000, 1000, 1000);
                        Gpm.CurrentState = GamePlayManager.State.Debug;
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
            //Morte();
        }
        else
        {
            vfx.Stop();
            vfxPlay = false;
        }
        //playerStatistiche.SetDistace(Name, DistanceMove);   //Setto il movimento del player // Da rivedere in futuro
    }

    public void OnEnemyAttack(IEnemy enemy)
    {
        currentEnemy.OnAttack -= OnEnemyAttack;
    }

    private void SpawnEnemy()
    {
        int possibilityEnemy0 = Random.Range(0, 10);
        if (possibilityEnemy0 < 7)
            currentEnemy = enemyManager.GetEnemy(1);
        else
            currentEnemy = enemyManager.GetEnemy(0);
        if (currentEnemy.ID == 1)
            currentEnemy.gameObject.transform.position = new Vector3(19.5f, 36.05f, 21f);
        else
            currentEnemy.gameObject.transform.position = new Vector3(19.5f, 36.7f, 21f);
        currentEnemy.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        currentEnemy.Spawn();
        currentEnemy.OnDestroy += OnEnemyDestroy;

    }

    public void OnEnemyDestroy(IEnemy enemy)
    {
        CB.CloseInventoryCombat();
        gameCamera.transform.position = GameObject.Find("CameraPosition").transform.position;
        gameCamera.transform.rotation = GameObject.Find("CameraPosition").transform.rotation;
        gameCamera.orthographic = false;
        gameCamera.orthographicSize = cameraSize;
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
            soundEffect.PlayEffect(soundEffect.Move);
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
                

            Lg.SetTextLog(Nickname + " moved" , true);
                
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
        if (detectObject.CorrectMove == true && grid.FindCell(ObjectX, ObjectZ).GetValidity() && grid.FindCell(ObjectX, ObjectZ).PlayerOnTile < 2) {


            //anim.SetTrigger("Walk");
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


            Lg.SetTextLog("Missione assegnata a: " + Nickname, true);
            CheckMission = true;
            Gpm.CurrentState = GamePlayManager.State.End;
        }
    }

    [HideInInspector]
    public int event1, event2;

    void Event() {
        //Controllo in che tipo di casella mi trovo

        
        if (grid.FindCell(XPos, ZPos).GetNameTile() != "" && grid.FindCell(XPos, ZPos).GetNameTile() != "Enemy" && grid.FindCell(XPos, ZPos).GetNameTile() != "Credit" && grid.FindCell(XPos, ZPos).GetNameTile() != "Empty")
        {
            soundEffect.PlayEffect(soundEffect.City);
            // se è all'interno di una città passa alla fase Object
            Debug.Log(Name + " si trova nella città: " + grid.FindCell(XPos, ZPos).GetNameTile());
            Lg.SetTextLog(Nickname + " is in city: " + grid.FindCell(XPos, ZPos).GetNameTile(), true);

            //ActiveTurn = false;
            Gpm.CurrentState = GamePlayManager.State.City;
        }
        else if (grid.FindCell(XPos, ZPos).POnTile != this && (XPos != 6 && XPos != 6))
        {
            Lg.SetTextLog(Nickname + "Sta Combattendo contro " + grid.FindCell(XPos, ZPos).POnTile.Name, true);
            Gpm.CurrentState = GamePlayManager.State.Combat;
        }
        else if (grid.FindCell(XPos, ZPos).GetNameTile() == "Enemy")
        {
            // se è all'interno di una Casella Nemico passa alla fase Combat
            Lg.SetTextLog(Nickname + " si trova in una casella Nemico", true);
            neutralizeCell(XPos, ZPos);
            Gpm.CurrentState = GamePlayManager.State.Combat;
        }
        else if (grid.FindCell(XPos, ZPos).GetNameTile() == "Credit")
        {
            int i = Random.Range(2, 6);
            soundEffect.PlayEffect(soundEffect.CreditTile);
            eventManager.TakeCredits(i, this);
            Lg.SetTextLog(Nickname + " received " + i + " credits", true);
            neutralizeCell(XPos, ZPos);
            Gpm.CurrentState = GamePlayManager.State.Debug;
        }
        else if (grid.FindCell(XPos, ZPos).GetNameTile() == "" || (grid.FindCell(XPos, ZPos).GetNameTile() == "Empty" && JustEmpted))//In una casella evento
        {
            if (Gpm.CurrentEventState == GamePlayManager.EventState.NotEvent)
                Gpm.CurrentEventState = GamePlayManager.EventState.Event;

            if (Gpm.CurrentEventState == GamePlayManager.EventState.Event)
            {
                // Viene scelto un numero randomico per ogni evento
                if (eventCard == 0)
                {
                    soundEffect.PlayEffect(soundEffect.EventTile);
                    do
                    {
                        eventCard = Random.Range(4, 39);
                    } while (eventCard == 0);
                    event1 = eventCard;
                    EventListView(event1);
                    UI.EventCard1.texture = UI.EventCardImage[immagineCarte];
                    UI.SpiegazioneCarta1.text = descrizioneCarte;
                    Debug.Log(event1);
                    do
                    {
                        eventCard = Random.Range(4, 39);
                    }
                    while (eventCard == event1 || eventCard == 0);
                    event2 = eventCard;
                    EventListView(event2);
                    UI.EventCard2.texture = UI.EventCardImage[immagineCarte];
                    UI.SpiegazioneCarta2.text = descrizioneCarte;
                    Debug.Log(event2);
                    UI.UICardEvent.SetActive(true);
                }
            }


            if (Gpm.CurrentEventState == GamePlayManager.EventState.Animation)
            {
                countdown = 0;
                Debug.Log(countdown);
                if (countdown <= 0)
                {
                    countdown = 2f;
                    JustEmpted = false;
                    Gpm.CurrentEventState = GamePlayManager.EventState.NotEvent;
                    Gpm.CurrentState = GamePlayManager.State.Debug;
                }
            }


           // if (Input.GetKeyDown(KeyCode.O))
           // {
           //     EventList(event1);
           //     if (Gpm.CurrentState != GamePlayManager.State.Combat && jumperEvent == false)
           //     {
           //         neutralizeCell(XPos, ZPos);
           //         Gpm.CurrentState = GamePlayManager.State.End;
           //     }
           // }
           //
           //
           // if (Input.GetKeyDown(KeyCode.P))
           // {
           //     EventList(event2);
           //     if (Gpm.CurrentState != GamePlayManager.State.Combat && jumperEvent == false)
           //     {
           //         neutralizeCell(XPos, ZPos);
           //         Gpm.CurrentState = GamePlayManager.State.End;
           //     }
           // }

            
           
        }
        else if (grid.FindCell(XPos, ZPos).GetNameTile() == "Empty")
        {
            if (!JustEmpted)
            {
                Gpm.CurrentState = GamePlayManager.State.Debug;
            }
        }


    }

    public void EventList(int _event)
    {
        switch (_event)
        {
            // Nulla
            case 0:
                Debug.Log("Error");
                break;
            // Nulla
            case 1:
                Debug.Log("Error");

                break;
            // Nulla
            case 2:
                Debug.Log("Error");
                break;
            //  Nulla
            case 3:
                Debug.Log("Error"); ;
                break;
            // Von Canterlik Ferire
            case 4:
                if (Credit >= 3)
                {
                    Credit -= 3;
                    eventManager.Heal(-1, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                    Lg.SetTextLog("The other players lost 1 HP", true);
                }
                else
                    Lg.SetTextLog("You don't have enough credits", true);
                break;
            // Truffati
            case 5:
                int i = -Random.Range(1, 3);
                eventManager.TakeCredits(i, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                Lg.SetTextLog("The other Players Lost " + -i + " credits", true);
                break;
            // Siero di rigenerazione
            case 6:
                Life += 2;
                Lg.SetTextLog(Nickname + " recovered 2 HP", true);
                break;
            // Terreno Fangoso
            case 7:
                PlayerEnemy1.PossibleMove = 2;
                PlayerEnemy2.PossibleMove = 2;
                PlayerEnemy3.PossibleMove = 2;
                Lg.SetTextLog("The other Players Lost 2 Movements", true);
                break;
            // Jynix doppia coppia
            case 8:
                eventManager.Jynix(this, 2, 6, 6);
                break;
            // Jynix Poker D'assi
            case 9:
                eventManager.Jynix(this, 4, 8, 10);
                break;
            // Jynix fold
            case 10:
                eventManager.Jynix(this, 1, 10, 1);
                break;

                // Sterlya
            // Buon Umore
            case 11:
                if (Credit >= 5)
                {
                    eventManager.PayForMaterial(2, 0, 2, this);
                    eventManager.PayForMaterial(1, 1, 2, this);
                    eventManager.PayForMaterial(1, 2, 2, this);
                    eventManager.PayForMaterial(1, 3, 2, this);
                    Lg.SetTextLog(Nickname + " received 8 materials", true);
                }
                else
                    Lg.SetTextLog(Nickname + " doesn't have enough credits", true);
                break;
            // Cattivo umore
            case 12:
                eventManager.PayForRandomMaterial(3, 2, this);
                Lg.SetTextLog(Nickname + " spends 3 credits for 2 material", true);
                break;
            // Generosa A
            case 13:
                if (Credit == 0)
                {
                    AddMaterial(0, 2);
                    Lg.SetTextLog(Nickname + " received 2 metal", true);
                }
                else
                    Lg.SetTextLog("You have too much credits", true);
                break;
            // Generosa B
            case 14:
                if (Credit == 0)
                {
                    AddMaterial(1, 2);
                    Lg.SetTextLog(Nickname + " received 2 poison", true);
                }
                else
                    Lg.SetTextLog("You have too much credits", true);
                break;
            // Generosa C
            case 15:
                if (Credit == 0)
                {
                    AddMaterial(2, 2);
                    Lg.SetTextLog(Nickname + " received 2 oil", true);
                }
                else
                    Lg.SetTextLog("You have too much credits", true);
                break;
            // Generosa D
            case 16:
                if (Credit == 0)
                {
                    AddMaterial(3, 2);
                    Lg.SetTextLog(Nickname + " received 2 gem", true);
                }
                else
                    Lg.SetTextLog("You have too much credits", true);
                break;
            // Professionale A
            case 17:
                eventManager.PayForMaterial(2, 0, 1, this);
                Lg.SetTextLog(Nickname + " spends 2 credits for 1 Metal", true);
                break;
            // Professionale B
            case 18:
                eventManager.PayForMaterial(2, 1, 1, this);
                Lg.SetTextLog(Nickname + " spends 2 credits for 1 Poison", true);
                break;
            // Professionale C
            case 19:
                eventManager.PayForMaterial(2, 2, 1, this);
                Lg.SetTextLog(Nickname + " spends 2 credits for 1 Oil", true);
                break;
            // Professionale D
            case 20:
                eventManager.PayForMaterial(2, 3, 1, this);
                Lg.SetTextLog(Nickname + " spends 2 credits for 1 Gem", true);
                break;
                //
            
            // Pezzo di ricambio
            case 21:
                Credit += 2;
                Lg.SetTextLog(Nickname + " received 2 credits", true);
                break;
            case 22:
                eventManager.AddMaterial(0, 1, this);
                Lg.SetTextLog(Nickname + " received 1 metal", true);
                break;
            case 23:
                eventManager.AddMaterial(1, 1, this);
                Lg.SetTextLog(Nickname + " received 1 poison", true);
                break;
            case 24:
                eventManager.AddMaterial(2, 1, this);
                Lg.SetTextLog(Nickname + " received 1 oil", true);
                break;
            case 25:
                eventManager.AddMaterial(3, 1, this);
                Lg.SetTextLog(Nickname + " received 1 gem", true);
                break;
                //

                // Von Caterlick 
            // Ferire e curare 
            case 26:
                if (Credit >= 5)
                {
                    Credit -= 5;
                    eventManager.Heal(-1, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                    eventManager.Heal(1, this); 
                    Lg.SetTextLog("Other players lost 1 HP", true);
                }
                else
                    Lg.SetTextLog("You don't have enough credits", true);
                break;
            // Ferirsi e ferire
            case 27:
                    eventManager.Heal(-1, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                    Life -= 2;
                    Lg.SetTextLog(Nickname + " lost 2 HP. Other players lost 1 HP", true);
                break;
                //

                // Branco di Lingua rosa
            // Enorme
            case 28:
                eventManager.AddMaterial(0, -3, this);
                eventManager.AddMaterial(0, -3, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                Lg.SetTextLog("All players lost 3 metal", true);
                break;
            case 29:
                eventManager.AddMaterial(1, -3, this);
                eventManager.AddMaterial(1, -3, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                Lg.SetTextLog("All players lost 3 poison", true);
                break;
            case 30:
                eventManager.AddMaterial(2, -3, this);
                eventManager.AddMaterial(2, -3, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                Lg.SetTextLog("All players lost 3 oil", true);
                break;
            case 31:
                eventManager.AddMaterial(3, -3, this);
                eventManager.AddMaterial(3, -3, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                Lg.SetTextLog("All players lost 3 gem", true);
                break;
            //
            // Infestazione
            case 32:
                eventManager.AddMaterial(0, -1, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                Lg.SetTextLog("The other players lost 1 Metal", true);
                break;
            case 33:
                eventManager.AddMaterial(1, -1, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                Lg.SetTextLog("The other players lost 1 poison", true);
                break;
            case 34:
                eventManager.AddMaterial(2, -1, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                Lg.SetTextLog("The other players lost 1 oil", true);
                break;
            case 35:
                eventManager.AddMaterial(3, -1, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                Lg.SetTextLog("The other players lost 1 gem", true);
                break;
            //
            //
            // Pioggia acida
            // Debole
            case 36:
                eventManager.Heal(-1, this);
                eventManager.Heal(-1, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                Lg.SetTextLog("All players lost 1 HP", true);
                break;
            // Normale
            case 37:
                eventManager.Heal(-1, this);
                eventManager.Heal(-1, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                eventManager.RemoveRandomMaterial(1, this);
                eventManager.RemoveRandomMaterial(1, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                Lg.SetTextLog("All players lost 1 HP and 1 material", true);
                break;
            // Forte
            case 38:
                eventManager.Heal(-2, this);
                eventManager.Heal(-2, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                eventManager.RemoveRandomMaterial(2, this);
                eventManager.RemoveRandomMaterial(2, PlayerEnemy1, PlayerEnemy2, PlayerEnemy3);
                Lg.SetTextLog("All players lost 2 HP and 2 material", true);
                break;
            default:
                Lg.SetTextLog("Evento " + eventCard + " nullo", true);
                break;
        }
    }

    string descrizioneCarte;
    int immagineCarte;

    public void EventListView(int _eventCard)
    {
        switch (_eventCard)
        {
            // Nulla
            case 0:
                Debug.Log("Error");
                break;
            // Nulla
            case 1:
                Debug.Log("Error");

                break;
            // 
            case 2:
                Debug.Log("Error");
                break;
            // 
            case 3:
                Debug.Log("Error");
                break;
            // Von Canterlik Ferire
            case 4:
                immagineCarte = 5;
                descrizioneCarte = "Pay 3 credits. Each opponent loses 1 HP.";
                break;
            // Truffati
            case 5:
                immagineCarte = 8;
                descrizioneCarte = "all other player lose 1 or 2 credits.";
                break;
            // Siero di rigenerazione
            case 6:
                immagineCarte = 2;
                descrizioneCarte = "Restores 2 HP.";
                break;
            // Stanchezza
            case 7:
                immagineCarte = 8;
                descrizioneCarte = "All other player have two possible move until their next turn.";
                break;
            // Jynix doppia coppia
            case 8:
                immagineCarte = 0;
                descrizioneCarte = "Lose 2 random materials. At 60% Jyinix gives you 6 credits.";
                break;
            // Jynix Poker D'assi
            case 9:
                immagineCarte = 0;
                descrizioneCarte = "Lose 4 random materials. At 80% Jyinx gives you 10 credits.";
                break;
            // Jynix fold
            case 10:
                immagineCarte = 0;
                descrizioneCarte = "Lose 1 random material. Get 2 credits.";
                break;

            // Sterlya
            // Buon Umore
            case 11:
                immagineCarte = 1;
                descrizioneCarte = "Spend 5 credits to get 2 materials of all kinds.";
                break;
            // Cattivo umore
            case 12:
                immagineCarte = 1;
                descrizioneCarte = "Spend 3 credits to get 2 random materials.";
                break;
            // Generosa A
            case 13:
                immagineCarte = 1;
                descrizioneCarte = "If you do not have credits, you get 2 metal.";
                break;
            // Generosa B
            case 14:
                immagineCarte = 1;
                descrizioneCarte = "If you do not have credits, you get 2 poison.";
                break;
            // Generosa C
            case 15:
                immagineCarte = 1;
                descrizioneCarte = "If you do not have credits, you get 2 oil.";
                break;
            // Generosa D
            case 16:
                immagineCarte = 1;
                descrizioneCarte = "If you do not have credits, you get 2 gem.";
                break;
            // Professionale A
            case 17:
                immagineCarte = 1;
                descrizioneCarte = "Spend 2 credits to get 1 metal.";
                break;
            // Professionale B
            case 18:
                immagineCarte = 1;
                descrizioneCarte = "Spend 2 credits to get 1 poison.";
                break;
            // Professionale C
            case 19:
                immagineCarte = 1;
                descrizioneCarte = "Spend 2 credits to get 1 oil.";
                break;
            // Professionale D
            case 20:
                immagineCarte = 1;
                descrizioneCarte = "Spend 2 credits to get 1 gem.";
                break;
            //

            // Pezzo di ricambio
            case 21:
                immagineCarte = 3;
                descrizioneCarte = "Get 2 credits.";
                break;
            case 22:
                immagineCarte = 3;
                descrizioneCarte = "Get 1 metal.";
                break;
            case 23:
                immagineCarte = 3;
                descrizioneCarte = "Get 1 poison.";
                break;
            case 24:
                immagineCarte = 3;
                descrizioneCarte = "Get 1 oil.";
                break;
            case 25:
                immagineCarte = 3;
                descrizioneCarte = "Get 1 gem.";
                break;
            //

            // Von Caterlick 
            // Ferire e curare 
            case 26:
                immagineCarte = 5;
                descrizioneCarte = "Pay 5 credits. Each opponent loses 1 HP point and you gain 1 HP";
                break;
            // Ferirsi e ferire
            case 27:
                immagineCarte = 5;
                descrizioneCarte = "Lose 2 HP. Each opponent loses 1 HP";
                break;
            //

            // Branco di Lingua rosa
            // Enorme
            case 28:
                immagineCarte = 6;
                descrizioneCarte = "All players lose 3 metal.";
                break;
            case 29:
                immagineCarte = 6;
                descrizioneCarte = "All players lose 3 poison.";
                break;
            case 30:
                immagineCarte = 6;
                descrizioneCarte = "All players lose 3 oil.";
                break;
            case 31:
                immagineCarte = 6;
                descrizioneCarte = "All players lose 3 gem.";
                break;
            //
            // Infestazione
            case 32:
                immagineCarte = 6;
                descrizioneCarte = "All other players lose 1 metal.";
                break;
            case 33:
                immagineCarte = 6;
                descrizioneCarte = "All other players lose 1 poison";
                break;
            case 34:
                immagineCarte = 6;
                descrizioneCarte = "All other players lose 1 oil";
                break;
            case 35:
                immagineCarte = 6;
                descrizioneCarte = "All other players lose 1 gem";
                break;
            //
            //
            // Pioggia acida
            // Debole
            case 36:
                immagineCarte = 7;
                descrizioneCarte = "All players lose 1 HP.";
                break;
            // Normale
            case 37:
                immagineCarte = 7;
                descrizioneCarte = "All players lose 1 HP and 2 random materials";
                break;
            // Forte
            case 38:
                immagineCarte = 7;
                descrizioneCarte = "All players lose 2 HP and 2 random materials";
                break;
            default:
                Lg.SetTextLog("Evento " + eventCard + " nullo", true);
                break;
        }
    }

    public void neutralizeCell(int _x, int _z)
    {
        grid.FindCell(_x, _z).Tile.GetComponent<MeshRenderer>().material = grid.TileMaterial[3];
        grid.FindCell(_x, _z).SetNameTile("Empty");
        MissionManager.EmptyCell++;
    }

    public void LoseRound()
    {
        if (Gpm.Name != Name)
            soundEffect.PlayEffect(soundEffect.WinCombat);
        else
            soundEffect.PlayEffect(soundEffect.LoseCombat);

        if (inCombatEnemy)
        {
            currentEnemy.CombatPoint++;
            currentEnemy.Attack = 0;
        }

        if (!inCombatEnemy)
        {
            currentEnemyPlayer.CombatPoint++;
            currentEnemyPlayer.Attacks = 0;
        }


        Attacks = 0;
        
        if ((currentEnemy != null && currentEnemy.CombatPoint == 1) || (!inCombatEnemy && currentEnemyPlayer.CombatPoint == 1))
        {
            if (!inCombatEnemy)
            {
                if (Gpm.Name == Name)
                    grid.FindCell(XPos, ZPos).SetPlayer(this);
                else
                    grid.FindCell(XPos, ZPos).SetPlayer(currentEnemyPlayer);
            }


            Gpm.CurrentState = GamePlayManager.State.Debug;
            CB.CloseInventoryCombat();

            gameCamera.transform.position = GameObject.Find("CameraPosition").transform.position;
            gameCamera.transform.rotation = GameObject.Find("CameraPosition").transform.rotation;
            gameCamera.orthographic = false;
            gameCamera.orthographicSize = cameraSize;
            

            if (currentEnemy != null)
                currentEnemy.DestroyMe();

            if (!inCombatEnemy)
            {
                int m = 0;
                int i = 0;
                int c = 0;
                if (Materiali[0] + Materiali[1] + Materiali[2] + Materiali[3] > 0)
                {
                    do
                    {
                        m = Random.Range(0, 4);
                    }
                    while (Materiali[m] == 0);
                    eventManager.AddMaterial(m, -1, this);
                    eventManager.AddMaterial(m, 1, currentEnemyPlayer);
                    i = 1;
                    if (Materiali[0] + Materiali[1] + Materiali[2] + Materiali[3] > 0)
                    {
                        do
                        {
                            m = Random.Range(0, 4);
                        }
                        while (Materiali[m] == 0);
                        eventManager.AddMaterial(m, -1, this);
                        eventManager.AddMaterial(m, 1, currentEnemyPlayer);
                        i = 2;
                        if (Materiali[0] + Materiali[1] + Materiali[2] + Materiali[3] > 0)
                        {
                            do
                            {
                                m = Random.Range(0, 4);
                            }
                            while (Materiali[m] == 0);
                            eventManager.AddMaterial(m, -1, this);
                            eventManager.AddMaterial(m, 1, currentEnemyPlayer);
                            i = 3;
                        }
                    }
                }
                if (Credit > 0)
                {
                    Credit--;
                    currentEnemyPlayer.Credit++;
                    c = 1;
                    if (Credit > 0)
                    {
                        Credit--;
                        currentEnemyPlayer.Credit++;
                        c = 2;
                    }
                    }
                Lg.SetTextLog(Nickname + " lost 1 HP" + currentEnemyPlayer.Nickname + " stole " + i + " " + Lg.MaterialName(m) + " and "+ c + " credit", true);
                currentEnemyPlayer.Attacks = 0;
                currentEnemyPlayer.CombatPoint = 0;
                Life--;
            }

            if (inCombatEnemy)
            {
                int l = currentEnemy.Damage;
                Life -= l;
                Lg.SetTextLog(Nickname + " lost " + l + " HP", true);
            }



            if (!inCombatEnemy)
            {                
                inCombatPlayer = false;
                currentEnemyPlayer.inCombatPlayer = false;
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
            Lg.SetTextLog(Nickname + " ha perso vita", true);
        }
        if (Life <= 0)
        {
            soundEffect.PlayEffect(soundEffect.PlayerDeath);
            Credit = 0;
            grid.FindCell(XPos, ZPos).PlayerOnTile--;
            if (grid.FindCell(XPos, ZPos).POnTile == this && Gpm.CurrentState == GamePlayManager.State.Combat && !inCombatEnemy)
                grid.FindCell(XPos, ZPos).SetPlayer(currentEnemyPlayer);
            else if (grid.FindCell(XPos, ZPos).POnTile == this)
                grid.FindCell(XPos, ZPos).POnTile = null;
            transform.position = grid.GetCenterPosition();
            transform.position += new Vector3(0f, _Yoffset, 0f);
            SetPositionPlayer();
            XPos = 6;
            ZPos = 6;
            Life = 5;
            grid.FindCell(XPos, ZPos).PlayerOnTile++;
            grid.FindCell(XPos, ZPos).POnTile = this;
            PossibleMove = 4;
            Lg.SetTextLog(Nickname + " died and returned to the center", true);
        }          
    }
}



public class ciccioBalilla {


}

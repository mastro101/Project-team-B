﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public GameObject HealPanel;
    public bool[] _isHealActive;

    public GameObject InfoPanel;
    public bool _isInfo=false;

    public GameObject Console;

    public GameObject UICity;
    public GameObject UICardEvent;

    public Sprite[] MaterialiImage;
    public Sprite[] MaterialiToSellImage;
    public Sprite[] MaterialiToBuyImageON;
    public Sprite[] MaterialiToSellImageON;
    public Image MaterialeToBuy, MaterialeToSell;
    public Button SellButton, BuyButton;
    public SpriteState SellSpriteState = new SpriteState();
    public SpriteState BuySpriteState = new SpriteState();

    public Texture[] EventCardImage;
    public RawImage EventCard1, EventCard2;
    public TextMeshProUGUI SpiegazioneCarta1, SpiegazioneCarta2;

    public Texture[] PlayerImage;
    public RawImage PlayerTurnImage;
    public RawImage Player1InCombat, PlayerOrEnemyInCombat, Player1NameInCombat, PlayerOrEnemyNameInCombat;
    public Texture[] PlayerName;
    public RawImage PlayerNameImage;

    public Texture[] AttackImage;
    public RawImage[] AttackPlayer, AttackPlayerOrEnemy;

    public GameObject Menu;
    public GameObject Ok;

    public Scrollbar sb;
    GamePlayManager Gpm;

	// Use this for initialization
	void Start () {
        _isHealActive = new bool[2];
        _isHealActive[0] = false;
        _isHealActive[1] = false;
        Gpm = FindObjectOfType<GamePlayManager>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Gpm.CurrentState == GamePlayManager.State.Combat)
        {
            Ok.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Menu.activeSelf)
                FindObjectOfType<ButtonManager>().OpenMenu();
            else
                FindObjectOfType<ButtonManager>().ResumeGame();
        }

        sb.value = 0;

        if (_isHealActive[1]) {
            ActiveHealPanel();
            _isHealActive[1] = false;
            Debug.Log("8Aperto");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!_isInfo)
            {
                InfoPanel.SetActive(true);
                _isInfo = true;    
            }
            else
            {
                InfoPanel.SetActive(false);
                _isInfo = false;
            }
        }


    }

    void ActiveHealPanel() {
        if (_isHealActive[0])
        {
            HealPanel.SetActive(true);
            _isHealActive[0] = true;
            Debug.Log("Aperto");
        }
        else {
            HealPanel.SetActive(false);
            _isHealActive[0] = false;
        }
    }

    public void DisableHealPanel_Button() {
        _isHealActive[0] = false;
        _isHealActive[1] = true;
    }

    public void LightAttackOnPlayer(int _attack)
    {
        AttackPlayer[_attack].gameObject.SetActive(true);
    }

    public void LightAttackOnEnemy(int _attack)
    {
        AttackPlayerOrEnemy[_attack].gameObject.SetActive(true);
    }

    public void LightAttackOff()
    {
        AttackPlayer[0].gameObject.SetActive(false);
        AttackPlayer[1].gameObject.SetActive(false);
        AttackPlayer[2].gameObject.SetActive(false);
        AttackPlayerOrEnemy[0].gameObject.SetActive(false);
        AttackPlayerOrEnemy[1].gameObject.SetActive(false);
        AttackPlayerOrEnemy[2].gameObject.SetActive(false);
    }

}

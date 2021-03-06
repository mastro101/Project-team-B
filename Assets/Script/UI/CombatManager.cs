﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public GameObject InventoryPanel;
    public GameObject UIInGame;
    public bool _isPanel;

    public TextMeshProUGUI PlayerStaminaText;
    public TextMeshProUGUI EnemyStaminaText;

    public Player player, enemyPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player != null)
        {
            /*
            PlayerStaminaText.text = "Stamina:" + player.Stamina.ToString();
            if (player.inCombatEnemy)
                EnemyStaminaText.text = "Stamina:" + player.currentEnemy.Stamina.ToString();
            else if (player.inCombatPlayer)
                EnemyStaminaText.text = "Stamina:" + player.currentEnemyPlayer.Stamina.ToString();
                */
        }
    }


    public bool Active;

    public void OpenAndCloseInventoryCombat()
    {
        Active = !Active;
        if (!_isPanel)
        {
            UIInGame.SetActive(false);
            InventoryPanel.SetActive(true);
            _isPanel = true;
        }
        else
        {
            UIInGame.SetActive(true);
            InventoryPanel.SetActive(false);
            _isPanel = false;
        }
    }

    public void CloseInventoryCombat()
    {
        Active = false;
        InventoryPanel.SetActive(false);
        UIInGame.SetActive(true);
        _isPanel = false;
    }





    public void Attack()
    {
        /*
        if (player.inCombatEnemy)
        {
            player.currentEnemy.TakeDamage(player.Attacks[Random.Range(0, 5)]);
            player.currentEnemy.DamagePlayer(player);
            player.currentEnemy.OnAttack += player.OnEnemyAttack;
        }
        if (player.inCombatPlayer)
        {
            player.currentEnemyPlayer.TakeDamage(player.Attacks[Random.Range(0, 5)]);
            player.TakeDamage(player.currentEnemyPlayer.Attacks[Random.Range(0, 5)]);
        }
        */
    }

}

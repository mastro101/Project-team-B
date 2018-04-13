using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public GameObject InventoryPanel;
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
        PlayerStaminaText.text = "Stamina:" + player.Stamina.ToString();
        if (player.inCombatEnemy)
            EnemyStaminaText.text = "Stamina:" + player.currentEnemy.Stamina.ToString();
        else if (player.inCombatPlayer)
            EnemyStaminaText.text = "Stamina:" + player.currentEnemyPlayer.Stamina.ToString();
    }


    public bool Active;

    public void OpenAndCloseInventoryCombat()
    {
        Active = !Active;
        if (!_isPanel)
        {
            InventoryPanel.SetActive(true);
            _isPanel = true;
        }
        else
        {
            InventoryPanel.SetActive(false);
            _isPanel = false;
        }
    }

    public void CloseInventoryCombat()
    {
        Active = false;
        InventoryPanel.SetActive(false);
        _isPanel = false;
    }





    public void Attack()
    {
        if (player.inCombatEnemy)
        {
            player.currentEnemy.TakeDamage(player.Attacks[Random.Range(0, 5)]);
            player.currentEnemy.AttackPlayer(player);
            player.currentEnemy.OnAttack += player.OnEnemyAttack;
        }
        if (player.inCombatPlayer)
        {
            player.currentEnemyPlayer.TakeDamage(player.Attacks[Random.Range(0, 5)]);
            player.TakeDamage(player.currentEnemyPlayer.Attacks[Random.Range(0, 5)]);
        }
    }

}

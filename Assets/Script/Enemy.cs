using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int Stamina;
    public int CombatPoints;
    public int Credits;
    public int Attack = 1;
    public string PlayerToAttack;
    public GameObject PGreen, PBlue, PRed, PYellow;
    public GamePlayManager Gpm;
    GameObject CurrentPlayer;

    private void Update()
    {
        switch (PlayerToAttack)
        {
            case "Green":
                CurrentPlayer = PGreen;
                break;
            case "Blue":
                CurrentPlayer = PBlue;
                break;
            case "Red":
                CurrentPlayer = PRed;
                break;
            case "Yellow":
                CurrentPlayer = PYellow;
                break;
            default:
                break;
        }

        if (Gpm.CurrentState == GamePlayManager.State.Combat)
        {
            CurrentPlayer.GetComponent<Player>().Stamina -= Attack;
        }
    }
}

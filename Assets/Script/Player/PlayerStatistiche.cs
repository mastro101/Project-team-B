using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistiche : MonoBehaviour {

    public string Name;
    public bool CheckMission = false;
    static public int[] CheckMissions;
    public int Mission;
    public int Life = 5;
    public int Credit;
    public int PossibleMove = 2;
    public int Stamina = 20;
    public int CombatPoints = 5;
    public int CurrentAttack;
    public int Attack1 = 1, Attack2 =1, Attack3=1, Attack4=1, Attack5=1;
    public GameObject[] Equip = {null, null, null};

    private void Start()
    {
        Credit = 1;
    }

    public void Heal()
    {

        if (Credit >= 3)
        {
            Credit -= 3;
            Life = 5;
        }
    }
}

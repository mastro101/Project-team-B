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
    public int WinPoint;
    public int CurrentAttack;
    public int Attacks;
    public GameObject[] Equip = {null, null, null};

    private void Awake()
    {
       
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

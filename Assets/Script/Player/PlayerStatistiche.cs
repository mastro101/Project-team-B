﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistiche : MonoBehaviour {

    public string Name;
    public string Nickname;
    public bool CheckMission = false;
    static public int[] CheckMissions;
    public int Mission;
    public int MaxLife = 4;
    public int life;
    public int[] Materiali;

    public int WinPoint;
    public int credit;
    public int PossibleMove = 2;
    public int CombatPoint;
    public int CurrentAttack;
    public int Attacks;
}

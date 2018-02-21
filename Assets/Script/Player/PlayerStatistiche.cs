using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistiche : MonoBehaviour {

    public string Name;
    public int Mission;
    public int Life = 5;
    public int Credit;
    public GameObject[] Equip = {null, null, null};

    public bool CheckMission = false;
}

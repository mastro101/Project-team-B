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
    public GameObject[] Equip = {null, null, null};

    private void Start()
    {
        Credit = 1;
    }

    public void Heal()
    {

        if (Credit >= 3 && Name == "Red")
        {
            Credit -= 3;
            Life = 5;
        }
    }
}

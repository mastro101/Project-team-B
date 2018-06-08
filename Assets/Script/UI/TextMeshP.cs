using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshP : MonoBehaviour {

    public TextMeshProUGUI textMesh;

    public TextMeshProUGUI MissionText;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI CreditsText;
    public TextMeshProUGUI PlayerInventoryText;
    public TextMeshProUGUI MosseRimaste;
    public TextMeshProUGUI M1Text;
    public TextMeshProUGUI M2Text;
    public TextMeshProUGUI M3Text;
    public TextMeshProUGUI M4Text;
    public TextMeshProUGUI WinPointsText;


    public bool _TurnPlayer;
    public bool _Mission;
    public bool _Life;
    public bool _Credits;

    public string NamePlayer;
    public string Mission, Life, Credits, Mosse, M1, M2, M3, M4, WinPoints;
    public string PlayerInventory;
	
	// Update is called once per frame
	void Update () {
        textMesh.text=NamePlayer;
        LifeText.text = Life;
        CreditsText.text = Credits;
        PlayerInventoryText.text = PlayerInventory;
        MosseRimaste.text = Mosse;
        M1Text.text = M1;
        M2Text.text = M2;
        M3Text.text = M3;
        M4Text.text = M4;
        WinPointsText.text = WinPoints;
    }

    public void SetName(string name) {
        NamePlayer = "Player:"+name;
        PlayerInventory = name + "'s Inventory";
    }

    public void SetMission(string mission)
    {
        Mission = "Mission:" + mission;
    }

    public void SetLife(string life) {
        Life = "Life: " + life;
    }

    public void SetCredits(string _credits) {
        Credits = ": " + _credits;
    }

    public void SetMosse(string mosse)
    {
        Mosse = "MOSSE RIMASTE: " + mosse;
    }

    public void SetM1(string _m1)
    {
        M1 = ": " + _m1;
    }

    public void SetM2(string _m2)
    {
        M2 = ": " + _m2;
    }

    public void SetM3(string _m3)
    {
        M3 = ": " + _m3;
    }

    public void SetM4(string _m4)
    {
        M4 = ": " + _m4;
    }

    public void SetWinPoints(string _winPoints)
    {
        WinPoints = "Win Point: " + _winPoints;
    }
}

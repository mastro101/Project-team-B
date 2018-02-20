﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshP : MonoBehaviour {

    public TextMeshProUGUI textMesh;

    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI CreditsText;
    public TextMeshProUGUI PlayerInventoryText;

    public bool _TurnPlayer;
    public bool _Life;
    public bool _Credits;

    public string NamePlayer;
    public string Life, Credits;
    public string PlayerInventory;
	
	// Update is called once per frame
	void Update () {
        textMesh.text=NamePlayer;
        LifeText.text = Life;
        CreditsText.text = Credits;
        PlayerInventoryText.text = PlayerInventory;
    }

    public void SetName(string name) {
        NamePlayer = "Player:"+name;
        PlayerInventory = name + "'s Inventory";
    }

    public void SetLife(string life) {
        Life = "Life:" + life;
    }

    public void SetCredits(string credits) {
        Credits = "Credits:" + credits;
    }
}

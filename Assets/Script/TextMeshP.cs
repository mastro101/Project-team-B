using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshP : MonoBehaviour {

    public TextMeshProUGUI textMesh;
    public string NamePlayer;

	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
        textMesh.text=NamePlayer;
	}

    public void SetName(string name) {
        NamePlayer = "Player:"+name;
    }
}

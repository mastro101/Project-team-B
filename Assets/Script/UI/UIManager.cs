using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject HealPanel;
    public bool[] _isHealActive;

    public GameObject DrawPanel;
    public bool[] _isDraw;

	// Use this for initialization
	void Start () {
        _isHealActive = new bool[2];
        _isHealActive[0] = false;
        _isHealActive[1] = false;

        _isDraw = new bool[2];
        _isDraw[0] = false;
        _isDraw[1] = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (_isHealActive[1]) {
            ActiveHealPanel();
            _isHealActive[1] = false;
            Debug.Log("8Aperto");
        }

        if (_isDraw[1])
        {
            ActiveDrawPanel();
            _isDraw[1] = false;
            Debug.Log("8Aperto");
        }


    }

    void ActiveHealPanel() {
        if (_isHealActive[0])
        {
            HealPanel.SetActive(true);
            _isHealActive[0] = true;
            Debug.Log("Aperto");
        }
        else {
            HealPanel.SetActive(false);
            _isHealActive[0] = false;
        }
    }

    void ActiveDrawPanel()
    {
        if (_isDraw[0])
        {
            DrawPanel.SetActive(true);
            _isDraw[0] = true;
            Debug.Log("Aperto");
        }
        else
        {
            DrawPanel.SetActive(false);
            _isDraw[0] = false;
        }
    }

    public void DisableHealPanel_Button() {
        _isHealActive[0] = false;
        _isHealActive[1] = true;
    }
}

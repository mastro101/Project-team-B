using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public GameObject HealPanel;
    public bool[] _isHealActive;

    public GameObject InfoPanel;
    public bool _isInfo=false;

    public GameObject UICity;
    public GameObject UICardEvent;

    public Texture[] MaterialiImage;
    public RawImage MaterialeToBuy, MaterialeToSell;

    public Texture[] EventCardImage;
    public RawImage EventCard1, EventCard2;
    public TextMeshProUGUI SpiegazioneCarta1, SpiegazioneCarta2;

    public Scrollbar sb;

	// Use this for initialization
	void Start () {
        _isHealActive = new bool[2];
        _isHealActive[0] = false;
        _isHealActive[1] = false;

    }
	
	// Update is called once per frame
	void Update () {

        sb.value = 0;

        if (_isHealActive[1]) {
            ActiveHealPanel();
            _isHealActive[1] = false;
            Debug.Log("8Aperto");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!_isInfo)
            {
                InfoPanel.SetActive(true);
                _isInfo = true;    
            }
            else
            {
                InfoPanel.SetActive(false);
                _isInfo = false;
            }
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

    public void DisableHealPanel_Button() {
        _isHealActive[0] = false;
        _isHealActive[1] = true;
    }
}

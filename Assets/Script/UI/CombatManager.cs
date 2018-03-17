using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public GameObject InventoryPanel;
    public bool _isPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenInventoryCombat()
    {
        if (!_isPanel)
        {
            InventoryPanel.SetActive(true);
            _isPanel = true;
        }
        else
        {
            InventoryPanel.SetActive(false);
            _isPanel = false;
        }
    }

}

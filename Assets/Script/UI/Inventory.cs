using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public GameObject Panel;

    public LogManager Lg;

    bool Active = false;

    private void Start()
    {
        Panel.SetActive(false);
    }

    private void Update(){

        if (Input.GetKeyDown(KeyCode.I)) {
            if (!Active)
            {
                Panel.SetActive(true);
                Active = true;

                Lg.SetTextLog("Inventario Aperto",true);
            }
            else
            {
                Panel.SetActive(false);
                Active = false;

                Lg.SetTextLog("Inventario Chiuso", true);
            }
        }

    }


}

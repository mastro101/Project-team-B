using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public GameObject Panel;

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
            }
            else
            {
                Panel.SetActive(false);
                Active = false;
            }
        }

    }


}

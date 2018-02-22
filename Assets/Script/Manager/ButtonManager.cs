using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    public GameObject PanelConsole;
    bool ActiveConsole = false;

    public void OpenConsole() {
        if (!ActiveConsole)
        {
            PanelConsole.SetActive(true);
            ActiveConsole = true;

        }
        else
        {
            PanelConsole.SetActive(false);
            ActiveConsole = false;

        }
    }
}

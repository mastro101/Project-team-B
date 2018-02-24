using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    public GameObject PanelConsole;
    bool ActiveConsole = true;

    Vector3 ConsolPosition;

    private void Start()
    {
        ConsolPosition = PanelConsole.transform.position;
        PanelConsole.transform.position += new Vector3(300f, 0f, 0f);
    }


    public void OpenConsole() {
        if (!ActiveConsole)
        {
            //PanelConsole.SetActive(true);
            PanelConsole.transform.position += new Vector3(300f, 0f, 0f);
            ActiveConsole = true;

        }
        else
        {
            //PanelConsole.SetActive(false);
            PanelConsole.transform.position = ConsolPosition;
            ActiveConsole = false;

        }
    }
}

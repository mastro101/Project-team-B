using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject TutorialPanel;
    bool ActiveConsole = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void OpenTutorialPanel()
    {
        if (!ActiveConsole)
        {
            TutorialPanel.SetActive(true);
            ActiveConsole = true;
        }
        else
        {
            TutorialPanel.SetActive(false);
            ActiveConsole = false;
        }

    }


    public void loadlevel()
    {
       
            SceneManager.LoadScene("Scene 2 - Copia 1");
       
    }
}

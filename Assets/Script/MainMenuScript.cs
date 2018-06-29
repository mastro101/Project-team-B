using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject TutorialPanel;
    bool ActiveConsole = false;
    SoundEffectManager soundEffect;
    public GameObject CreditsPanel;

    // Use this for initialization
    void Start()
    {
        soundEffect = FindObjectOfType<SoundEffectManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitGame()
    {
        soundEffect.PlayEffect(soundEffect.Click);
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
        soundEffect.PlayEffect(soundEffect.Click);
    }


    public void OpenCreditsPanel()
    {
        soundEffect.PlayEffect(soundEffect.Click);
        CreditsPanel.SetActive(true);
    }

    public void CloseCreditsPanel()
    {
        soundEffect.PlayEffect(soundEffect.Click);
        CreditsPanel.SetActive(false);
    }

    public void loadlevel()
    {
        soundEffect.PlayEffect(soundEffect.Click);
        SceneManager.LoadScene("Scene 2 - Copia 1");
       
    }
}

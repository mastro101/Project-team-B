using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

    public Image TutorialImage;
    public Sprite[] TutorialSpriteArray;
    public Page CurrentPage;


    private void Update()
    {
        switch (CurrentPage)
        {
            case Page.One:
                TutorialImage.sprite = TutorialSpriteArray[0];
                break;
            case Page.Two:
                TutorialImage.sprite = TutorialSpriteArray[1];
                break;
            case Page.Three:
                TutorialImage.sprite = TutorialSpriteArray[2];
                break;
            default:
                break;
        }
    }

    public void TurnPageUp()
    {
        switch (CurrentPage)
        {
            case Page.One:
                CurrentPage = Page.Two;
                break;
            case Page.Two:
                CurrentPage = Page.Three;
                break;
            case Page.Three:
                CurrentPage = Page.One;
                break;
            default:
                break;
        }
    }

    public void TurnPageDown()
    {
        switch (CurrentPage)
        {
            case Page.One:
                CurrentPage = Page.Three;
                break;
            case Page.Two:
                CurrentPage = Page.One;
                break;
            case Page.Three:
                CurrentPage = Page.Two;
                break;
            default:
                break;
        }
    }

    public enum Page
    {
        One,
        Two,
        Three,
    }
}

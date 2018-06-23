using UnityEngine;
using System.Collections;
using TMPro;

public class UIRank : MonoBehaviour
{
    public GameObject UIClassifica;
    public Player P1, P2, P3, P4;
    public TextMeshProUGUI[] WinP, Pv, Credit, Poison, Oil, Metal, Gem;
    bool open;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.L))
        {
            if (!open)
            {
                UIClassifica.SetActive(true);
                open = true;
            }
            else
            {
                UIClassifica.SetActive(false);
                open = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Tab) || Input.GetKeyUp(KeyCode.L))
        {
            if (open)
            {
                UIClassifica.SetActive(false);
                open = false;
            }
        }

        WinP[0].text = P1.WinPoint.ToString();
        WinP[1].text = P2.WinPoint.ToString();
        WinP[2].text = P3.WinPoint.ToString();
        WinP[3].text = P4.WinPoint.ToString();

        Pv[0].text = P1.Life.ToString();
        Pv[1].text = P2.Life.ToString();
        Pv[2].text = P3.Life.ToString();
        Pv[3].text = P4.Life.ToString();

        Credit[0].text = P1.Credit.ToString();
        Credit[1].text = P2.Credit.ToString();
        Credit[2].text = P3.Credit.ToString();
        Credit[3].text = P4.Credit.ToString();

        Metal[0].text = P1.Materiali[0].ToString();
        Metal[1].text = P2.Materiali[0].ToString();
        Metal[2].text = P3.Materiali[0].ToString();
        Metal[3].text = P4.Materiali[0].ToString();

        Poison[0].text = P1.Materiali[1].ToString();
        Poison[1].text = P2.Materiali[1].ToString();
        Poison[2].text = P3.Materiali[1].ToString();
        Poison[3].text = P4.Materiali[1].ToString();

        Oil[0].text = P1.Materiali[2].ToString();
        Oil[1].text = P2.Materiali[2].ToString();
        Oil[2].text = P3.Materiali[2].ToString();
        Oil[3].text = P4.Materiali[2].ToString();

        Gem[0].text = P1.Materiali[3].ToString();
        Gem[1].text = P2.Materiali[3].ToString();
        Gem[2].text = P3.Materiali[3].ToString();
        Gem[3].text = P4.Materiali[3].ToString();
    }
}

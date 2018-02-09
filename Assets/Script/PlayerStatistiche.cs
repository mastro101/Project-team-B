using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistiche : MonoBehaviour {

    public Player player;

    public int distanceMove = 1; //di quante caselle può muoversi

    string NamePlayer;
    int distanceRequest;

    public void SetDistace(string Name,int Distance) {
        distanceRequest = Distance;
        NamePlayer = Name;
    }

    public int GetDistance() {
        return distanceMove;
    }

    public void Update()
    {
        switch (NamePlayer) {
            case "Blue":
                distanceMove = distanceRequest;
                break;
            case "Red":
                distanceMove = distanceRequest;
                break;
            case "Yellow":
                distanceMove = distanceRequest;
                break;
            case "Green":
                distanceMove = distanceRequest;
                break;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdowntest : MonoBehaviour {

    float timeLeft = 10.0f;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Debug.Log("End");
            timeLeft = 10f;
        }
    }

}

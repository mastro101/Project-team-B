using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LogManager : MonoBehaviour {

    public TextMeshProUGUI Log;

    string LogText;

    bool _isEquals = false;

    // Update is called once per frame
    void Update () {

        if (_isEquals) {
            Log.text = Log.text + "\n" + LogText ;
            _isEquals = false;
        }
	}

    public void SetTextLog(string logText, bool isEquals) {
        LogText = logText;
        _isEquals = isEquals;
    }

    enum Material
    {
        Metal,
        Poison,
        Oil,
        Gem,
    }
}

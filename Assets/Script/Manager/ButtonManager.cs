using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    public GamePlayManager Gpm;

    public void EndPhase() {
        Gpm.CurrentState = GamePlayManager.State.Event;
        Gpm.CurrentState = GamePlayManager.State.End;
    }
}

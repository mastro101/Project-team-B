using UnityEngine;
using System.Collections;

public class GameplaySM : StateMachineBase
{
    MainMenuScript mainMenuScript;

    protected override void Start()
    {
        base.Start();
        
    }
}

public class GameplaySMContext : IStateMachineContext
{
    
}

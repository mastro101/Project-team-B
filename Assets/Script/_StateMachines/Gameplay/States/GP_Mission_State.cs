using UnityEngine;
using System.Collections;

public class GP_Mission_State : GP_Base_State
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter: Mission");
        
    }
}

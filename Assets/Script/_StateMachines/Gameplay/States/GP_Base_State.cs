using UnityEngine;
using System.Collections;

public class GP_Base_State : StateBase
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Tick()
    {
        base.Tick();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override IState Setup(IStateMachineContext _context)
    {
        return null;
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public abstract class StateMachineBase : MonoBehaviour
{
    protected Animator SM;

    protected IStateMachineContext currentContext;

    protected virtual void Awake()
    {
        SM = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        foreach (StateMachineBehaviour smB in SM.GetBehaviours<StateMachineBehaviour>())
        {
            (smB as StateBase).Setup(currentContext);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonState
{
    protected Person person;
    protected PersonStateMachine stateMachine;

    public PersonState(Person person, PersonStateMachine stateMachine)
    {
        this.person = person;
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState() {}

    public virtual void ExitState() {}

    public virtual void FrameUpdate() {}

    public virtual void FixedFrameUpdate() {}

    public virtual void GetUnstuck(){}
}

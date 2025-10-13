using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskState
{
    protected Person person;
    protected PersonStateMachine stateMachine;

    TaskState(Person person, PersonStateMachine stateMachine)
    {
        this.person = person;
        this.stateMachine = stateMachine;
    }
}

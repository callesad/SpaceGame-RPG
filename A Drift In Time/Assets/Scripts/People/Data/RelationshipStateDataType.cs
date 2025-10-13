using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationshipState
{
    protected Person person;
    protected PersonStateMachine stateMachine;

    RelationshipState(Person person, PersonStateMachine stateMachine)
    {
        this.person = person;
        this.stateMachine = stateMachine;
    }
}

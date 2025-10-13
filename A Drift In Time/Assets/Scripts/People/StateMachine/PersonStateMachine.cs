using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonStateMachine
{

    public PersonState currentState;

    public void Initialize(PersonState startingState)
    {
        currentState = startingState;
        currentState.EnterState();
    }


    public void ChangeState(PersonState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }


}

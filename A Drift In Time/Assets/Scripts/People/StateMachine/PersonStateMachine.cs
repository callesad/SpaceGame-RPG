using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonStateMachine
{

    public PersonState currentState;
    public PersonState previousState;

    public void Initialize(PersonState startingState)
    {
        currentState = startingState;
        currentState.EnterState();
    }


    public void ChangeState(PersonState newState)
    {
        previousState = currentState;
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }


}

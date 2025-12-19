using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonDialogueWithPlayerState : PersonState
{


    public PersonDialogueWithPlayerState(Person person, PersonStateMachine stateMachine) : base(person, stateMachine){}

    public override void EnterState() {

        var npc = person as NPCPerson;

        if (npc.debugLogs) Debug.Log(npc.gameObject.name + " Entered Dialogue State");

        npc.whereImGoing = null;

    }

    public override void ExitState() {

        Debug.Log("exited dialogue");
    }

    public override void FrameUpdate() {}

    public override void FixedFrameUpdate() {}

    public override void GetUnstuck(){}
}

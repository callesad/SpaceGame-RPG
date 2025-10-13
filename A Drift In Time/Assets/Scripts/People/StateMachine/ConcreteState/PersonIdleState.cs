using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonIdleState : PersonState
{

    public PersonIdleState(Person person, PersonStateMachine stateMachine) : base(person, stateMachine){}

    public override void EnterState() 
    {
        
        var npc = person as NPCPerson;

        if (npc.debugLogs) Debug.Log(npc.gameObject.name + " Entered Idol State");

        npc.whereImGoing = null;
        //npc.whereIWantToBe = null;
    }

    public override void FrameUpdate() 
    {

        var npc = person as NPCPerson;

        if(npc.walkRandomly) {
            stateMachine.ChangeState(npc.RandomlyWalking);
            return;
        }

        if(npc.whereIWantToBe!=null) {
            stateMachine.ChangeState(npc.WalkingWithPurpose);
            return;
        }

        if(npc.meander) {
            stateMachine.ChangeState(npc.Meandering);
        }


    }

    public override void FixedFrameUpdate() 
    {

    }

    public override void ExitState() 
    {

    }

    public override void GetUnstuck()
    {
        var npc = person as NPCPerson;

        if (npc.whereILive!=null) {
            npc.movePoint.transform.position = npc.whereILive.transform.position + new Vector3(0f,0.5f,0f);
            npc.transform.position = npc.whereILive.transform.position + new Vector3(0f,0.5f,0f);
            if (npc.debugLogs) Debug.Log(npc.gameObject.name + " respawned at home");
            npc.isScrewed=false;
            npc.isStuck=false;
            return;
        }
        if (npc.isScrewed) {return;}
        
        if (npc.debugLogs) Debug.Log(npc.gameObject.name + " is screwed...");
        npc.isScrewed=true;
        return;
    }
}

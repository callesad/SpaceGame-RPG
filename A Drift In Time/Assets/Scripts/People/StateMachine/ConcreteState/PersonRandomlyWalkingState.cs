using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonRandomlyWalkingState : PersonState
{

    public PersonRandomlyWalkingState(Person person, PersonStateMachine stateMachine) : base(person, stateMachine){}



    public override void EnterState()
    {

        /*//allows you to reference variables and methodes that are implimented on the NPCPerson class but not on the Person class
        var npc = person as NPCPerson; //get NPCPerson from Person
        if (npc != null) {
            GameObject newPathPoint = npc.ChooseRandomNewAdjacentPathPoint();
            if (newPathPoint!=null){
                npc.whereImGoing = newPathPoint;
            }
        }
        */
        
        var npc = person as NPCPerson;

        if (npc.debugLogs) Debug.Log(npc.gameObject.name + " Entered Randomly Walking State");

        if(npc.whereImGoing==null&&npc.whereImAt==null) {
            npc.whereImGoing=npc.whereIveBeen; //if not going anywhere and not at a path point goes back to last path point to reset logic
        }
        
    }

    public override void FrameUpdate()
    {
        //allows you to reference variables and methodes that are implimented on the NPCPerson class but not on the Person class
        var npc = person as NPCPerson; //get NPCPerson from Person

        if(npc.whereIWantToBe!=null) {
            npc.walkRandomly=false;
        }

        if(!npc.walkRandomly) {
            stateMachine.ChangeState(npc.Idle);
            return;
        }

        if (npc != null) { //if person is npc

            //if you dont got a place to go, or your at where youre trying to be, choose a new place to go
            if(npc.whereImGoing==null||npc.whereImAt==npc.whereImGoing){
                //choose new location
                GameObject newPathPoint = npc.ChooseRandomNewAdjacentPathPoint();
                if (newPathPoint!=null){
                    npc.whereImGoing = newPathPoint;
                }
            } else if(Vector3.Distance(person.transform.position,person.movePoint.transform.position) <= 0.05f){
                npc.MoveTowards(npc.whereImGoing.transform.position + new Vector3(0f,0.5f,0f));
            } 
        } //if not npc does nothing

        


    
    }

    public override void FixedFrameUpdate()
    {

    }

    public override void GetUnstuck()
    {
        var npc = person as NPCPerson; //get NPCPerson from Person

        if (npc.whereIveBeen==null||npc.whereIveBeen==npc.whereImGoing) {
            if (npc.debugLogs) Debug.Log(npc.gameObject.name + " is stuck and cannot find a way out ):");
            stateMachine.ChangeState(npc.Idle);
            return;
        }
        npc.whereImGoing=npc.whereIveBeen; //tries to get unstuck, will expand upon later
        npc.numberOfTimesNPCHasNotMoved=0;
        if (npc.debugLogs) Debug.Log(npc.gameObject.name + " is unstuck!!");
        npc.isStuck=false;
        return;
    }

    public override void ExitState()
    {

    }
}

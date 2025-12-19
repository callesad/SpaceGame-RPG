using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonWalkingWithPurposeState : PersonState
{
    
    public PersonWalkingWithPurposeState(Person person, PersonStateMachine stateMachine) : base(person, stateMachine){}

    private int index=0;
    private GameObject WIWTBclone;

    public override void EnterState()
    {
        
        var npc = person as NPCPerson;

        if (npc.debugLogs) Debug.Log(npc.gameObject.name + " Entered Walking With Purpose State");

        if (npc.whereIWantToBe!=null) {
            WIWTBclone = npc.whereIWantToBe;
        } else if (WIWTBclone != null) {
            npc.whereIWantToBe = WIWTBclone;
        }
    }

    public override void FrameUpdate()
    {
        var npc = person as NPCPerson; //get NPCPerson from Person

        if(npc.walkRandomly) {
            stateMachine.ChangeState(npc.RandomlyWalking);
            return;
        }

        if (npc != null) { //if person is npc, failsafe

            if (npc.whereIWantToBe==null) { //failsafe
                //change state
                stateMachine.ChangeState(npc.Idle);
                return;
            }   //if there is a target location
                //if you dont got a place to go, or your at where youre trying to be, choose a new place to go

            if (npc.whereIWantToBe!=WIWTBclone) {
                //re-enter state to reset logic if destination changes mid exicution
                Reset();
                return;
            }

            if (npc.whereImAt==npc.whereIWantToBe) {//if you are where you want to be just exit state, your job is done
                npc.path.Clear();
                index=0;
                //change state
                stateMachine.ChangeState(npc.Idle);
                return;
            } 
            
            if(npc.path.Count==0) { //if there is no path, make one

                List<GameObject> x;

                if (npc.whereImAt==null) {//if not somewhere use where youve been
                    if (npc.whereIveBeen==null){/*change state*/return;} //if no where youve been change to state to find closest point
                    x = npc.FindPathToDestination(npc.whereIveBeen, npc.whereIWantToBe);
                } else {
                    x = npc.FindPathToDestination(npc.whereImAt, npc.whereIWantToBe);
                    if (x==null){if (npc.debugLogs) Debug.Log("No Path To destination"); return;}
                }
                npc.path = x;
                return;
            }

            if(npc.whereImGoing==null||npc.whereImAt==npc.whereImGoing){
                //choose new location
                GameObject newPathPoint = npc.path[index];
                index+=1;
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

    public override void ExitState()
    {
        var npc = person as NPCPerson;
        npc.path.Clear();
        index=0;
        npc.whereIWantToBe=null;
    }

    void Reset()
    {
        var npc = person as NPCPerson;
        npc.path.Clear();
        index=0;
        npc.whereImGoing = null;

        EnterState();
    }

    public override void GetUnstuck()
    {
        var npc = person as NPCPerson; //get NPCPerson from Person

        index-=1;

        if (npc.whereIveBeen==null||index<0) {
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


}

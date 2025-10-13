using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMeanderingState : PersonState
{
    
    public PersonMeanderingState(Person person, PersonStateMachine stateMachine) : base(person, stateMachine){}

    Vector3 destination;
    float timeReachedDestination;
    bool waiting=false;
    int walkDistance;

    public override void EnterState() 
    {
        var npc = person as NPCPerson;

        if (npc.debugLogs) Debug.Log(npc.gameObject.name + " Entered Meandering State");

        if (npc.whatRoomImIn==null){
            if (npc.debugLogs) Debug.Log(npc.gameObject.name + " is not in a room");
            stateMachine.ChangeState(npc.Idle);
            return;
        }

        destination = npc.movePoint.transform.position;
        //timeReachedDestination = Time.time+npc.meanderingWaitTime;

        Debug.Log($"Checking walkDistance: npc={npc.meanderingWalkDistance}, room={npc.whatRoomImIn.maxWalkAroundSpace}");
        
        
        if(npc.meanderingWalkDistance>npc.whatRoomImIn.maxWalkAroundSpace) {
            walkDistance=npc.whatRoomImIn.maxWalkAroundSpace;
        } else {
            walkDistance=npc.meanderingWalkDistance;
        }
    }

    public override void ExitState() {}

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

        if(!npc.meander) {
            stateMachine.ChangeState(npc.Idle);
        }

         //meandering logic sudo code:

        /*
         1) take three steps in random direction
            a)choose a random direction
            b)move movepoint 
            c)check if is in room
            b)use move towards 
        */

        //a)
        
        

        if(waiting){
            if(Time.time>=timeReachedDestination+npc.meanderingWaitTime){waiting=false;}
            return;
        }


        int retries = 0;
        while (destination == npc.movePoint.transform.position && retries < 10) {
            destination = npc.ChooseRandomSpotInRoom(npc.whatRoomImIn,walkDistance);
            retries++;
        }

        if(Vector3.Distance(person.transform.position,person.movePoint.transform.position) <= 0.05f){
            npc.MoveTowards(destination);
        } 
        if(Vector3.Distance(npc.movePoint.transform.position,destination)<0.05) {
            timeReachedDestination=Time.time;
            Debug.Log(timeReachedDestination);
            waiting=true;
        }
        
        




        /*
         2) wait some amount of time
            a)make a variable so lag time can be adjusted fluidly
        */

        /*
         3) repeat process
         */

    }

    public override void FixedFrameUpdate() {}

    public override void GetUnstuck(){}
}

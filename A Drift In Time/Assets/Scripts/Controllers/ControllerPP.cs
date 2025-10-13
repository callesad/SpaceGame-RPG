using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPP: Controller
{
    PlayerPerson player;
    MovePoint movePoint;
    int x;
    int y;

    public ControllerPP(PlayerPerson player, MovePoint movePoint)
    {
        this.player = player;
        this.movePoint = movePoint;
    }

    public void GetMovement()
    {
        //float sprintBoost = 1f;
        //float y = Input.GetAxis("Vertical");
        //float x = Input.GetAxis("Horizontal");
        /*x = 0;
        y = 0;
        if (Input.GetKey(KeyCode.W)) y = 1;
        if (Input.GetKey(KeyCode.S)) y = -1; 
        if (Input.GetKey(KeyCode.D)) x = 1;
        if (Input.GetKey(KeyCode.A)) x = -1;
        Vector3 direction = new Vector3(x, y, 0f).normalized;

        if (Input.GetKey(KeyCode.LeftShift)) sprintBoost = 1.7f;

        player.Move2D(direction,sprintBoost);*/

        //sprinting
        if(Input.GetKey(KeyCode.LeftShift)) {player.sprintBoost = 1.7f;} else {player.sprintBoost = 1f;}

        if (Vector3.Distance(player.transform.position,movePoint.transform.position) <= 0.05f) 
        {
            //movepoint moves
            movePoint.previousLocation = movePoint.transform.position;
            Vector3 destination;

            if (Mathf.Abs(Input.GetAxisRaw("Vertical"))==1f)
            {
                destination = movePoint.transform.position + new Vector3(0f,Input.GetAxisRaw("Vertical"),0f);
                if(!MovePoint.ReserveSpot(destination)){
                    movePoint.transform.position = movePoint.previousLocation;
                    return;
                }
                movePoint.transform.position = destination;
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Horizontal"))==1f)
            {
                destination = movePoint.transform.position + new Vector3(Input.GetAxisRaw("Horizontal"),0f,0f);
                if(!MovePoint.ReserveSpot(destination)){
                    movePoint.transform.position = movePoint.previousLocation;
                    return;
                }
                movePoint.transform.position = destination;
            }
            

            if(!movePoint.CanMoveThere()) {movePoint.transform.position = movePoint.previousLocation;}
        }
        
    }

    //public void GetAction()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //        player.UseWeapon(player.weapon);
    //}
}
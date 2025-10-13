using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerson : Person
{

    private ControllerPP controller;
    //player states

    protected override void Awake()
    {
        base.Awake();
        controller = new ControllerPP(this,this.movePoint);
    } 

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        controller.GetMovement();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerson : Person
{

    private ControllerPP controller;
    public InteractPoint interactPoint;
    //player states

    protected override void Awake()
    {
        base.Awake();
        //finding interact point
        interactPoint = GetComponentInChildren<InteractPoint>();
        controller = new ControllerPP(this,this.movePoint,interactPoint);
    } 

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        controller.GetMovement();
    }

    protected override void Update()
    {
        base.Update();
        controller.GetAction();
    }
}

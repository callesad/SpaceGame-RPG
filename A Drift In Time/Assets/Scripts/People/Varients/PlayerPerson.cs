using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerson : Person
{
        //Variables
//Declaring player tools
    private Controller controller;
    public InteractPoint interactPoint;
//----------------------

        //Functions
//monobehaviour functions
    #region subscribing to events
    void OnEnable()
    {
        if (EventManager.Instance != null) {
            EventManager.Instance.OnEnterDialogue+=OnEnterDialogue;
            EventManager.Instance.OnExitDialogue+=OnExitDialogue;
        }
    }

    void OnDisable()
    {
        if (EventManager.Instance != null) {
            EventManager.Instance.OnEnterDialogue-=OnEnterDialogue;
            EventManager.Instance.OnExitDialogue-=OnExitDialogue;
        }
    }
    #endregion

    #region initializing functions
    protected override void Awake()
    {
        base.Awake();
        //finding interact point
        interactPoint = GetComponentInChildren<InteractPoint>(); //getting reference to interact point from children
        controller = new ControllerPP(this,this.movePoint,interactPoint); //creating a controller
    } 
    #endregion

    #region update functions
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        controller?.GetMovement();
    }

    protected override void Update()
    {
        base.Update();
        controller?.GetAction();
    }
    #endregion
//-------------------------

//public class implimentations------------
    #region OnEnterDialogue/OnExitDialogue
    void OnEnterDialogue()
    {
      
        controller = null;
    }

    void OnExitDialogue()
    {
        controller = new ControllerPP(this,this.movePoint,interactPoint); //changes the controller back to a player controller
    }
    #endregion
//--------------------------------------
}

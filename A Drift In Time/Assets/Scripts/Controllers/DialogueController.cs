using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : Controller
{
    DialogueUI UI;

    //buttons


    public DialogueController(DialogueUI UI)
    {
        this.UI = UI;
    }

    public override void GetMovement()
    {
        
    }

    public override void GetAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.ContinueOrExitStory();
        }
    }


}

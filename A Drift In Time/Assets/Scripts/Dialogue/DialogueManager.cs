using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public GameObject DialogueUI;

    private bool dialogueActive = false;

    

    #region subscribing to events

    void OnEnable()
    {
        if (EventManager.Instance != null) {
            EventManager.Instance.OnStartDialogue+=OnStartDialogue;
            EventManager.Instance.OnEndDialogue+=OnEndDialogue;
            EventManager.Instance.OnEnterDialogue+=OnEnterDialogue;
            EventManager.Instance.OnExitDialogue+=OnExitDialogue;
        }
    }
    

    void OnDisable()
    {
        if (EventManager.Instance != null) {
            EventManager.Instance.OnStartDialogue-=OnStartDialogue;
            EventManager.Instance.OnEndDialogue-=OnEndDialogue;
            EventManager.Instance.OnEnterDialogue-=OnEnterDialogue;
            EventManager.Instance.OnExitDialogue-=OnExitDialogue;
        }
    }

    #endregion

    

    #region public class implimentations

    void OnEnterDialogue()
    {
        if (dialogueActive) {return;}//prevents event from overflowing
        dialogueActive = true;
        DialogueUI?.SetActive(true); //turns on ui
    }

    void OnStartDialogue(string knotName)
    {
        Debug.Log("Started dialogue at knot "+"'"+knotName+"'");
    }

    void OnEndDialogue()
    {
        
    }

    void OnExitDialogue()
    {
        dialogueActive = false;
        DialogueUI?.SetActive(false); //turns off ui
    }
    #endregion
}

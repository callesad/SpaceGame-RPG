using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Ink.Runtime;

public class EventManager : MonoBehaviour
{


    //creates a reference to itslef that other scripts can access
    public static EventManager Instance;

    #region dialogue events

    public event Action OnEnterDialogue;

    public void EnterDialogue()
    {
        FreezeGame();

        OnEnterDialogue?.Invoke();
    }


    public event Action OnExitDialogue;

    public void ExitDialogue()
    {

        UnfreezeGame();

        OnExitDialogue?.Invoke();
    }


    public event Action<string> OnStartDialogue;

    public void StartDialogue(string knotName)
    {

        OnStartDialogue?.Invoke(knotName);
    }


    public event Action<int> OnUpdateDialogueChoice;

    public void UpdateDialogueChoice(int index)
    {
        OnUpdateDialogueChoice?.Invoke(index);
    }


    public event Action<string, List<Choice>> OnDisplayDialogue;

    public void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
    {
        OnDisplayDialogue?.Invoke(dialogueLine, dialogueChoices);
    }

    public event Action OnEndDialogue;

    public void EndDialogue()
    {
        OnEndDialogue?.Invoke();
    }
    #endregion

    #region quest events

    public event Action<string> OnStartQuest;

    public void StartQuest(string questID)
    {
        Debug.Log("quest started: " + questID);
        OnStartQuest?.Invoke(questID);
    }

    public event Action<string> OnAdvanceQuest;

    public void AdvanceQuest(string questID)
    {
        OnAdvanceQuest?.Invoke(questID);
    }

    public event Action<string> OnFinishQuest;

    public void FinishQuest(string questID)
    {
        OnFinishQuest?.Invoke(questID);
    }

    #endregion

    #region time events

    public event Action<int, int> OnUpdateClock;

    public void UpdateClock(int hour, int minute)
    {
        OnUpdateClock?.Invoke(hour, minute);
    }

    #endregion
    
    #region game function events

    public event Action OnFreezeGame;
    public bool gameFrozen = false;

    public void FreezeGame()
    {
        gameFrozen = true;
        Debug.Log("game frozen");

        //Invoking subscribers
        OnFreezeGame?.Invoke();
    }

    public event Action OnUnfreezeGame;

    public void UnfreezeGame()
    {
        gameFrozen = false;
        Debug.Log("game unfrozen");

        //Invoke subscribers
        OnUnfreezeGame?.Invoke();
    }

    #endregion

    void Awake()
    {
        //sets current to this class
        Instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

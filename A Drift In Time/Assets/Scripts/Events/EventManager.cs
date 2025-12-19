using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{


    //creates a reference to itslef that other scripts can access
    public static EventManager Instance;

    #region instantiates events

    public event Action OnEnterDialogue;

    public void EnterDialogue()
    {
        OnEnterDialogue?.Invoke();
    }


    public event Action OnExitDialogue;

    public void ExitDialogue()
    {
        OnExitDialogue?.Invoke();
    }


    public event Action<string> OnStartDialogue;

    public void StartDialogue(string knotName)
    {
        OnStartDialogue?.Invoke(knotName);
    }


    public event Action OnUpdateDialogueChoice;

    public void UpdateDialogueChoice()
    {
        OnUpdateDialogueChoice?.Invoke();
    }


    public event Action OnDisplayDialogue;

    public void DisplayDialogue()
    {
        OnDisplayDialogue?.Invoke();
    }

    public event Action OnEndDialogue;

    public void EndDialogue()
    {
        OnEndDialogue?.Invoke();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{

    public GameObject DialogueUI;

    public static DialogueManager Instance;

    private bool dialogueActive = false;
    public int DialogueChoiceIndex = 0;

    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJson; 

    [Header("Dialogue UI")]
    [SerializeField] private DialogueUI UI;

    private Story story;

    private int currentChoiceIndex = -1;

    private InkExternalFunctions inkExternalFunctions;


    #region subscribing to events

    void OnEnable()
    {
        if (EventManager.Instance != null) {
            EventManager.Instance.OnStartDialogue+=OnStartDialogue;
            EventManager.Instance.OnEndDialogue+=OnEndDialogue;
            EventManager.Instance.OnEnterDialogue+=OnEnterDialogue;
            EventManager.Instance.OnExitDialogue+=OnExitDialogue;
            EventManager.Instance.OnUpdateDialogueChoice+=OnUpdateDialogueChoice;
            
        }
    }
    

    void OnDisable()
    {
        if (EventManager.Instance != null) {
            EventManager.Instance.OnStartDialogue-=OnStartDialogue;
            EventManager.Instance.OnEndDialogue-=OnEndDialogue;
            EventManager.Instance.OnEnterDialogue-=OnEnterDialogue;
            EventManager.Instance.OnExitDialogue-=OnExitDialogue;
            EventManager.Instance.OnUpdateDialogueChoice-=OnUpdateDialogueChoice;
            
        }
    }

    #endregion

    void Awake()
    {
        Instance = this;
        if (inkJson!=null) {
            story = new Story(inkJson.text);
        } else {
            Debug.Log("inkJson file is null");
        }
        inkExternalFunctions = new InkExternalFunctions();
        inkExternalFunctions?.Bind(story);
    }

    void OnDestroy()
    {
        inkExternalFunctions?.UnBind(story);
    }

    #region public class implimentations

    void OnEnterDialogue()
    {
        if (dialogueActive) {return;}//prevents event from overflowing
        dialogueActive = true;
        DialogueUI?.SetActive(true); //turns on ui
    }

    void OnStartDialogue(string knotName)
    {
        
        knotName = knotName?.Trim();

        if (string.IsNullOrEmpty(knotName)) {
            Debug.Log("knotName was empty string when entering dialogue");
            EventManager.Instance.EndDialogue();
            return;
        }

        var result = story.ContentAtPath(new Ink.Runtime.Path(knotName));

        if (result.obj == null || result.approximate)
        {
            Debug.Log($"Ink knot/stitch '{knotName}' does not exist.");
            EventManager.Instance.EndDialogue();
            return;
        }

        Debug.Log("Started dialogue at knot "+"'"+knotName+"'");

        story.ChoosePathString(knotName); //jumps to the desired knot
        ContinueOrExitStory();//calls function to check if dialogue shold continue or terminate    
    }

    void OnUpdateDialogueChoice(int choiceIndex)
    {
        this.currentChoiceIndex = choiceIndex;
    }

    void OnEndDialogue()
    {
        story.ResetState();
        EventManager.Instance.ExitDialogue();
    }

    void OnExitDialogue()
    {
        dialogueActive = false;
        DialogueUI?.SetActive(false); //turns off ui
    }
    #endregion

    #region local functions

    public void ContinueOrExitStory()
    {

        if (story.currentChoices.Count>0)
        {
            if (currentChoiceIndex<0||currentChoiceIndex>4) 
            {
                Debug.Log("Invalid choice index");
                return;
            }
            story.ChooseChoiceIndex(currentChoiceIndex);
        }
        
        if (story.canContinue)
        {
            string dialogueLine = story.Continue();

            if(dialogueLine=="")
            {
                EventManager.Instance.EndDialogue();
                return;
            }

            EventManager.Instance.DisplayDialogue(dialogueLine, story.currentChoices);
        }
        else
        {
            EventManager.Instance.EndDialogue();
        }
    }

    #endregion
}

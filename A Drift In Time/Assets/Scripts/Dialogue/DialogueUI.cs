using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{

    [Header("components")]
    [SerializeField] public GameObject UIButtonsGO;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private DialogueChoiceButton[] choiceButtons;
    [SerializeField] private List<DialogueChoiceButton> activeChoiceButtons = new List<DialogueChoiceButton>();
    

    void Awake()
    {
        ResetDialoguePannel();
    }

    #region subscribing to events

    void OnEnable()
    {
        EventManager.Instance.OnDisplayDialogue+=OnDisplayDialogue;
        EventManager.Instance.OnEndDialogue+=OnEndDialogue;
    }

    void OnDisable()
    {
        EventManager.Instance.OnDisplayDialogue-=OnDisplayDialogue;
        EventManager.Instance.OnEndDialogue-=OnEndDialogue;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.ContinueOrExitStory();
        }
    }

    #region public class implimentations

    void OnDisplayDialogue(string dialogueLine, List<Choice> dialogueChoices) 
    {
        //clears choice buttons
        activeChoiceButtons.Clear();
        //displays text
        dialogueText.text = dialogueLine;

        //checks for propper amount of choices
        if (dialogueChoices.Count > choiceButtons.Length)
        {
            Debug.LogError("not enough UI buttons for choices");
        }

        //deactivates all choice buttons by default
        foreach (DialogueChoiceButton choiceButton in choiceButtons)
        {
            choiceButton.gameObject.SetActive(false);
        }
        int choiceButtonIndex = dialogueChoices.Count-1;
        
        //displays choices
        for (int InkChoiceIndex = choiceButtonIndex ; InkChoiceIndex >= 0 ; InkChoiceIndex--)
        {
            Choice dialogueChoice = dialogueChoices[InkChoiceIndex];
            DialogueChoiceButton choiceButton = choiceButtons[choiceButtonIndex];

            choiceButton.gameObject.SetActive(true);
            choiceButton.SetChoiceIndex(InkChoiceIndex);
            choiceButton.SetChoiceText(dialogueChoice.text);

            //autoselects first choice
            if(InkChoiceIndex==0)
            {
                choiceButton.SelectButton();
            }

            activeChoiceButtons.Add(choiceButton);

            choiceButtonIndex--;
        }

        //AI written code for wrapping scrolling using unity built in UI features
        for (int i = 0; i < activeChoiceButtons.Count; i++)
        {
            DialogueChoiceButton current = activeChoiceButtons[i];

            DialogueChoiceButton down =
                (i == 0)
                ? activeChoiceButtons[activeChoiceButtons.Count - 1]
                : activeChoiceButtons[i - 1];

            DialogueChoiceButton up =
                (i == activeChoiceButtons.Count - 1)
                ? activeChoiceButtons[0]
                : activeChoiceButtons[i + 1];

            current.SetNavigation(
                up.GetComponent<Button>(),
                down.GetComponent<Button>()
            );
        }
    }

    void OnEndDialogue()
    {
        ResetDialoguePannel();
    }

    #endregion

    #region local functions

    void ResetDialoguePannel()
    {
        dialogueText.text = "";
    }




    #endregion
}

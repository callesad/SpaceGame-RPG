using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueUI : MonoBehaviour
{

    Controller controller;

    [Header("components")]
    [SerializeField] public GameObject UIButtonsGO;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private DialogueChoiceButton[] choiceButtons;
    

    void Awake()
    {
        controller = new DialogueController(this);
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
        controller.GetAction();
    }

    #region public class implimentations

    void OnDisplayDialogue(string dialogueLine, List<Choice> dialogueChoices) 
    {
        //text
        dialogueText.text = dialogueLine;

        //choices
        if (dialogueChoices.Count > choiceButtons.Length)
        {
            Debug.LogError("not enough UI buttons for choices");
        }

        foreach (DialogueChoiceButton choiceButton in choiceButtons)
        {
            choiceButton.gameObject.SetActive(false);
        }
        int choiceButtonIndex = dialogueChoices.Count-1;
        
        for (int InkChoiceIndex = choiceButtonIndex ; InkChoiceIndex >= 0 ; InkChoiceIndex--)
        {
            Choice dialogueChoice = dialogueChoices[InkChoiceIndex];
            DialogueChoiceButton choiceButton = choiceButtons[choiceButtonIndex];

            choiceButton.gameObject.SetActive(true);
            choiceButton.SetChoiceIndex(InkChoiceIndex);
            choiceButton.SetChoiceText(dialogueChoice.text);

            choiceButtonIndex--;

            if (InkChoiceIndex == choiceButtonIndex) 
            {
                choiceButton.SelectButton();
                EventManager.Instance.UpdateDialogueChoice(choiceButtonIndex);
            }

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

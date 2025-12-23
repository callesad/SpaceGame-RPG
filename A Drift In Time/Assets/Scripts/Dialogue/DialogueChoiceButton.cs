using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueChoiceButton : MonoBehaviour, ISelectHandler
{

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    private int choiceIndex = -1;

    public void SetChoiceIndex(int index)
    {
        this.choiceIndex = index;
    }

    public void SetChoiceText(string ChoiceTextString)
    {
        buttonText.text = ChoiceTextString;
    }

    //so that button can be selected by scripts outside of this class
    public void SelectButton()
    {
        button.Select();
    }

    //when button is selected the updatedialoguechoice event is triggered
    public void OnSelect(BaseEventData eventDate)
    {
        EventManager.Instance.UpdateDialogueChoice(choiceIndex);
    }

    
}

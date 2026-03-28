using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions
{

    public void Bind(Story story)
    {
        story.BindExternalFunction("StartQuest", (string questID) => StartQuest(questID));
        story.BindExternalFunction("AdvanceQuest", (string questID) => AdvanceQuest(questID));
        story.BindExternalFunction("FinishQuest", (string questID) => FinishQuest(questID));
    }

    public void UnBind(Story story)
    {
        story.UnbindExternalFunction("StartQuest");
        story.UnbindExternalFunction("AdvanceQuest");
        story.UnbindExternalFunction("FinishQuest");
    }

    private void StartQuest(string questID)
    {
        EventManager.Instance.StartQuest(questID);
    }

    private void AdvanceQuest(string questID)
    {
        EventManager.Instance.AdvanceQuest(questID);
    }

    private void FinishQuest(string questID)
    {
        EventManager.Instance.FinishQuest(questID);
    }
}
